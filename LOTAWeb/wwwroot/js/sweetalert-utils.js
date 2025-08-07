// SweetAlert2 Utility Functions for LOTA Project

// Show success message
function showSuccess(message, title = 'Success') {
    Swal.fire({
        title: title,
        text: message,
        icon: 'success',
        timer: 3000,
        timerProgressBar: true,
        showConfirmButton: false,
        position: 'top-end',
        toast: true
    });
}

// Show error message
function showError(message, title = 'Error') {
    Swal.fire({
        title: title,
        text: message,
        icon: 'error',
        timer: 5000,
        timerProgressBar: true,
        showConfirmButton: true,
        position: 'top-end',
        toast: true
    });
}

// Show warning message
function showWarning(message, title = 'Warning') {
    Swal.fire({
        title: title,
        text: message,
        icon: 'warning',
        timer: 4000,
        timerProgressBar: true,
        showConfirmButton: false,
        position: 'top-end',
        toast: true
    });
}

// Show info message
function showInfo(message, title = 'Information') {
    Swal.fire({
        title: title,
        text: message,
        icon: 'info',
        timer: 3000,
        timerProgressBar: true,
        showConfirmButton: false,
        position: 'top-end',
        toast: true
    });
}

// Show confirmation dialog using SweetAlert2
function showConfirm(message, onConfirm, onCancel = null, title = 'Confirm') {
    Swal.fire({
        title: title,
        text: message,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        confirmButtonText: 'Confirm',
        cancelButtonText: 'Cancel',
        reverseButtons: true,
        allowOutsideClick: false,
        allowEscapeKey: false
    }).then((result) => {
        if (result.isConfirmed) {
            if (onConfirm) {
                onConfirm();
            }
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            if (onCancel) {
                onCancel();
            }
        }
    });
}

// Show loading message
function showLoading(message = 'Loading...') {
    return Swal.fire({
        title: message,
        allowOutsideClick: false,
        allowEscapeKey: false,
        showConfirmButton: false,
        didOpen: () => {
            Swal.showLoading();
        }
    });
}

// Clear all toasts (SweetAlert2 doesn't need this, but keeping for compatibility)
function clearAllToasts() {
    // SweetAlert2 automatically handles closing
    // This function is kept for compatibility with existing code
}

// Show AJAX error message
function showAjaxError(error, defaultMessage = 'An error occurred. Please try again.') {
    console.error('AJAX Error:', error);
    
    let message = defaultMessage;
    if (error.responseJSON && error.responseJSON.message) {
        message = error.responseJSON.message;
    } else if (error.statusText) {
        message = `Request failed: ${error.statusText}`;
    }
    
    showError(message);
}

// Show form validation errors
function showValidationErrors(errors) {
    if (Array.isArray(errors)) {
        // Show first error as main message, others as list
        const mainError = errors[0];
        const otherErrors = errors.slice(1);
        
        let html = `<div class="text-start">${mainError}`;
        if (otherErrors.length > 0) {
            html += '<ul class="mt-2 mb-0">';
            otherErrors.forEach(error => {
                html += `<li>${error}</li>`;
            });
            html += '</ul>';
        }
        html += '</div>';
        
        Swal.fire({
            title: 'Validation Errors',
            html: html,
            icon: 'error',
            confirmButtonColor: '#dc3545'
        });
    } else if (typeof errors === 'string') {
        showError(errors);
    } else {
        showError('Please check your input and try again.');
    }
}

// Show success with auto-refresh
function showSuccessAndReload(message, delay = 1500) {
    Swal.fire({
        title: 'Success',
        text: message,
        icon: 'success',
        timer: delay,
        timerProgressBar: true,
        showConfirmButton: false
    }).then(() => {
        location.reload();
    });
}

// Show success with callback
function showSuccessWithCallback(message, callback, delay = 1500) {
    Swal.fire({
        title: 'Success',
        text: message,
        icon: 'success',
        timer: delay,
        timerProgressBar: true,
        showConfirmButton: false
    }).then(() => {
        if (typeof callback === 'function') {
            callback();
        }
    });
} 