const navSlide = () => {
  const burger = document.querySelector('.burger');
  const nav = document.querySelector('.nav-links');
  const navLinks = document.querySelectorAll('.nav-links li');

  burger.addEventListener('click', (e) => {
    e.preventDefault();
    // Toggle Nav
    nav.classList.toggle('nav-active');

    //Animate Links
    navLinks.forEach((link, index) => {
      if (link.style.animation) {
        link.style.animation = '';
      } else {
        link.style.animation = `navLinkFade 0.5s ease forwards ${
          index / 7 + 0.3
        }s`;
      }
    });
    //Bruger Animation
    burger.classList.toggle('toggle');
  });
};

navSlide();

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
//                  Special offers  Modal
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
