/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './RebateContracts.Web/Views/**/*.cshtml',
    './RebateContracts.Web/Pages/**/*.cshtml',
    './RebateContracts.Web/Views/**/*.razor',
    './RebateContracts.Web/Views/**/*.cs',
    './RebateContracts.Web/Models/**/*.cs',
    './RebateContracts.Web/wwwroot/js/**/*.js',
  ],
  theme: {
    extend: {
      colors: {
        primary: {
          DEFAULT: '#00953B', // De Heus green
          light: '#00B44A',
          dark: '#007A2F',
        },
        secondary: {
          DEFAULT: '#0057A4', // De Heus blue
          light: '#0073D1',
          dark: '#003B6F',
        },
        accent: {
          DEFAULT: '#F6C700', // De Heus yellow
          light: '#FFE066',
          dark: '#C7A000',
        },
        neutral: {
          DEFAULT: '#F5F5F5',
          dark: '#222',
        },
        card: '#FFFFFF',
        border: '#E5E7EB',
        error: '#DC2626',
        success: '#16A34A',
      },
    },
  },
  plugins: [],
};
