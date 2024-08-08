import Toastify from 'toastify-js';
import 'toastify-js/src/toastify.css'; // Import Toastify CSS

export function showToast(message, duration = 3000) {
  Toastify({
    text: message,
    duration: duration,
    close: true,
    gravity: 'bottom', // Position of the toast
    position: 'right',
    backgroundColor: 'linear-gradient(to right, #00b09b, #96c93d)', // Custom background color
    stopOnFocus: true
  }).showToast();
}
