# Rebate Data Relationships & Calculation Summary

## 1. Key Entities and Relationships

| Entity/File                        | Key Columns/Fields                                                                 | Relationships                                                                                      |
|-------------------------------------|-----------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------|
| CountryMapping.csv                  | RebateContract, BusinessUnitName                                                  | Maps RebateContract to Business Units                                                              |
| Global_Demand.csv                   | Global Code, Product Name, Year, Global Demand De Heus in MT                      | Maps Global Code to Product and Demand per Year                                                    |
| Rebate_Contract_Quantity_Adjust.csv | RebateContract, Global Code, Business Unit, Year, Adjusting Quantity (MT)         | Adjusts contract quantity for a specific contract, code, unit, and year                            |
| Concentration_Conversion.csv        | RebateContract, Original Global Code, Conversion Multiplier, Target Global Code    | Converts product codes for rebate calculation                                                      |
| rebate_percentage.csv               | RebateContract, Valid Date, Supplier, Global Code, Volume, Price, Share, Rebate   | Defines rebate % based on total quantity, price, and share                                         |
| rebate_per_mt.csv                   | RebateContract, Valid Date, Supplier, Global Code, Volume, Rebate Per MT, Currency| Defines rebate per MT for total quantity                                                           |
| rebate_rate_payable.csv             | RebateContract, Valid Date, Supplier, Global Code, Begin/End Volume, Rebate (%)   | Defines rebate % for incremental volume ranges                                                     |
| H.csv, I.csv                       | RebateContract, Supplier, GlobalItemCode, Product, Price, Rebate, NetPrice, etc.  | Actual product sales, rebate per unit, and net price per contract                                  |

---

## 2. Relationship Table

| RebateContract Type | Related Files                        | Key Relationship Columns         | Calculation Basis                |
|---------------------|--------------------------------------|----------------------------------|----------------------------------|
| Country Mapping     | CountryMapping.csv                   | RebateContract, BusinessUnitName | Which business units are eligible for each contract |
| Global Demand       | Global_Demand.csv                    | Global Code, Year                | Used for minimum share/volume conditions           |
| Quantity Adjust     | Rebate_Contract_Quantity_Adjust.csv  | RebateContract, Global Code, Year| Adjusts purchased volume for rebate calculations   |
| Concentration Conversion | Concentration_Conversion.csv     | RebateContract, Original Global Code | Converts sales to target code for rebate eligibility |
| Rebate %            | rebate_percentage.csv                | RebateContract, Global Code, Volume, Price, Share | Rebate % for total quantity, with price/share conditions |
| Rebate per MT       | rebate_per_mt.csv                    | RebateContract, Global Code, Volume | Fixed rebate per MT for total quantity            |
| Rebate Rate Payable | rebate_rate_payable.csv              | RebateContract, Global Code, Volume Range | Rebate % for incremental volume ranges            |
| Sales Data          | H.csv, I.csv                         | RebateContract, GlobalItemCode, Product | Actual sales and rebate per unit                  |

---

## 3. Detailed Table Examples

### a. Country Mapping
| RebateContract   | BusinessUnitName         |
|------------------|-------------------------|
| B-Asia-2024      | Vietnam                 |
| B-Asia-2024      | Indonesia               |
| ...              | ...                     |

### b. Global Demand
| Global Code | Product Name      | Year | Global Demand De Heus in MT |
|-------------|-------------------|------|-----------------------------|
| 3205010     | L-Arginine        | 2024 | 2800                        |
| ...         | ...               | ...  | ...                         |

### c. Rebate Percentage
| RebateContract | Valid Date From | Supplier | Global Code | Volume purchased > MT | Annual Avg Price | Min Share | Rebate (%) |
|----------------|----------------|----------|-------------|----------------------|------------------|-----------|------------|
| A-2024         | 2024-01-01     | A        | 3202010     | 1914                 | 0                |           | 0.036713   |
| ...            | ...            | ...      | ...         | ...                  | ...              | ...       | ...        |

### d. Rebate Per MT
| RebateContract | Valid Date From | Supplier | Global Code | Volume purchased > MT | Rebate Per MT | Currency |
|----------------|----------------|----------|-------------|----------------------|---------------|----------|
| C-2024         | 2024-01-01     | C        | 3203010     | 6200                 | 57            | USD      |
| ...            | ...            | ...      | ...         | ...                  | ...           | ...      |

### e. Rebate Rate Payable
| RebateContract | Valid Date From | Supplier | Global Code | Begin volume range | End volume range | Rebate (%) |
|----------------|----------------|----------|-------------|--------------------|------------------|------------|
| B-Asia-2024    | 2024-01-01     | B        | 3201050     | 0                  | 500              | 0.026971   |
| ...            | ...            | ...      | ...         | ...                | ...              | ...        |

---

## 4. Formulas for Rebate Contract Types

### A. Rebate Percentage (`rebate_percentage.csv`)
**Formula:**  
```
If (Total Purchased Volume > Threshold) AND (Price/Share conditions met):
    Rebate = Total Purchased Volume × Invoice Price × Rebate %
```
**Conditions:**  
- Volume purchased must exceed threshold.
- Price or minimum share in global demand may be required.

---

### B. Rebate Per MT (`rebate_per_mt.csv`)
**Formula:**  
```
If (Total Purchased Volume > Threshold):
    Rebate = Total Purchased Volume × Rebate Per MT
```
**Conditions:**  
- Volume purchased must exceed threshold.

---

### C. Rebate Rate Payable (`rebate_rate_payable.csv`)
**Formula:**  
```
For each volume range:
    If (Total Purchased Volume > Begin Volume Range):
        Rebate for Range = (min(Total Purchased Volume, End Volume Range) - Begin Volume Range) × Invoice Price × Rebate %
Total Rebate = Sum of Rebates for all applicable ranges
```
**Conditions:**  
- Only the quantity exceeding each range is eligible for the higher rebate rate.

---

### D. Concentration Conversion (`Concentration_Conversion.csv`)
**Formula:**  
```
Converted Volume = Original Volume × Conversion Multiplier
```
**Usage:**  
- Used to aggregate sales of different concentrations to a target code for rebate eligibility.

---

### E. Quantity Adjustment (`Rebate_Contract_Quantity_Adjust.csv`)
**Formula:**  
```
Adjusted Volume = Purchased Volume + Adjusting Quantity (MT)
```
**Usage:**  
- Used before applying rebate formulas.

---

## 5. How They Work Together

1. **Sales Data** (`H.csv`, `I.csv`) is mapped to **RebateContract** and **Global Code**.
2. **CountryMapping** ensures the business unit is eligible for the contract.
3. **Concentration Conversion** is applied if needed.
4. **Quantity Adjustment** is applied.
5. **Global Demand** is referenced for share-based rebates.
6. **Rebate formulas** are applied based on contract type:
    - **Percentage**: `rebate_percentage.csv`
    - **Per MT**: `rebate_per_mt.csv`
    - **Rate Payable**: `rebate_rate_payable.csv`

---

## 6. Summary Table: Contract Type vs. Calculation

| Contract Type         | Calculation File(s)           | Formula Type         | Notes                                  |
|---------------------- |------------------------------|----------------------|----------------------------------------|
| Percentage            | rebate_percentage.csv         | % of total           | May require price/share conditions     |
| Per MT                | rebate_per_mt.csv             | Fixed per MT         | Simple threshold                       |
| Rate Payable          | rebate_rate_payable.csv       | Tiered % per range   | Incremental, only for excess quantity  |
| Concentration Convert | Concentration_Conversion.csv  | Multiplier           | For code aggregation                   |
| Quantity Adjust       | Rebate_Contract_Quantity_Adjust.csv | Additive         | For manual adjustments                 |

---

