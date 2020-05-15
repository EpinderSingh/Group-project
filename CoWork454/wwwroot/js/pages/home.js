//===============================================
//                   Alert  Modal
// ==============================================
const alertBtn = document.querySelector('.alert-btn');
const alertBg = document.querySelector('.alert-bg');
const alertClose = document.querySelector('.alert-close');

alertBtn.addEventListener('click', function () {
  alertBg.classList.add('alert-active');
});

alertClose.addEventListener('click', function () {
  alertBg.classList.remove('alert-active');
});
window.addEventListener('click', outsideClick);
function outsideClick(e) {
  if (e.target == alertBg) {
    alertBg.classList.remove('alert-active');
  }
}

//===============================================
//             Special offers  Modal
// ==============================================
const specialBtn = document.querySelector('.special-btn');
const specialBg = document.querySelector('.special-bg');
const specialClose = document.querySelector('.special-close');

specialBtn.addEventListener('click', function () {
  specialBg.classList.add('special-active');
});

specialClose.addEventListener('click', function () {
  specialBg.classList.remove('special-active');
});
window.addEventListener('click', function (e) {
  if (e.target == specialBg) {
    specialBg.classList.remove('special-active');
  }
});

// =======================================
//           To top button
// =======================================

const toTop = document.querySelector('.to-top');
window.addEventListener('scroll', () => {
  if (window.pageYOffset > 200) {
    toTop.classList.add('active');
  } else {
    toTop.classList.remove('active');
  }
});
