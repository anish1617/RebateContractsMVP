// Simple Toaster for notifications
window.showToast = function (message, type = 'success') {
    const toast = document.createElement('div');
    toast.className = `fixed top-6 right-6 z-50 px-6 py-3 rounded shadow-lg text-white font-semibold transition bg-${type === 'success' ? 'primary' : 'error'}`;
    toast.innerText = message;
    document.body.appendChild(toast);
    setTimeout(() => {
        toast.classList.add('opacity-0');
        setTimeout(() => toast.remove(), 500);
    }, 2500);
};

// Enhanced Modal functionality with AJAX support
window.showModal = function (modalId) {
    document.getElementById(modalId)?.classList.remove('hidden');
};

window.hideModal = function (modalId) {
    document.getElementById(modalId)?.classList.add('hidden');
};

// Load modal content via AJAX
window.loadModalContent = function (url, title, modalId = 'modal-container') {
    const modal = document.getElementById(modalId);
    const modalTitle = document.getElementById('modal-title');
    const modalContent = document.getElementById('modal-content');
    
    if (!modal || !modalTitle || !modalContent) return;
    
    // Set modal title
    modalTitle.textContent = title;
    
    // Show loading indicator
    modalContent.innerHTML = '<div class="flex justify-center items-center p-8"><div class="spinner-border animate-spin h-8 w-8 border-4 rounded-full border-primary border-t-transparent"></div></div>';
    
    // Show modal
    showModal(modalId);
    
    // Fetch content
    fetch(url)
        .then(response => {
            if (!response.ok) throw new Error('Network response was not ok');
            return response.text();
        })
        .then(html => {
            modalContent.innerHTML = html;
            // Initialize any scripts needed for the modal
            const scripts = modalContent.querySelectorAll('script');
            scripts.forEach(script => {
                const newScript = document.createElement('script');
                Array.from(script.attributes).forEach(attr => {
                    newScript.setAttribute(attr.name, attr.value);
                });
                newScript.appendChild(document.createTextNode(script.innerHTML));
                script.parentNode.replaceChild(newScript, script);
            });
        })
        .catch(error => {
            modalContent.innerHTML = `<div class="text-center text-red-600 p-4">Error loading content: ${error.message}</div>`;
        });
};

// Submit modal form via AJAX
window.submitModalForm = function (formId, successCallback = null) {
    const form = document.getElementById(formId);
    if (!form) return;
    
    fetch(form.action, {
        method: form.method,
        body: new FormData(form),
        headers: {
            'X-Requested-With': 'XMLHttpRequest'
        }
    })
    .then(response => {
        if (!response.ok) {
            return response.text().then(text => {
                throw new Error(text || 'Form submission failed');
            });
        }
        return response.json();
    })
    .then(data => {
        if (data.success) {
            hideModal('modal-container');
            showToast(data.message || 'Operation completed successfully');
            if (successCallback && typeof successCallback === 'function') {
                successCallback(data);
            }
        } else {
            // Display validation errors or other messages
            console.error('Form submission returned errors:', data);
        }
    })
    .catch(error => {
        showToast(error.message || 'An error occurred while processing your request', 'error');
    });
};
