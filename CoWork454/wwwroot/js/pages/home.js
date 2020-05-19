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

// =======================================
// To top button
// =======================================

const toTop = document.querySelector('.to-top');
window.addEventListener('scroll', () => {
  if (window.pageYOffset > 200) {
    toTop.classList.add('active');
  } else {
    toTop.classList.remove('active');
  }
});

// =======================================
// Pricing Packages slider
// =======================================

function check() {
    var checkBox = document.getElementById("checbox");
    var text1 = document.getElementsByClassName("text1");
    var text2 = document.getElementsByClassName("text2");

    for (var i = 0; i < text1.length; i++) {
        if (checkBox.checked == true) {
            text1[i].style.display = "block";
            text2[i].style.display = "none";
        } else if (checkBox.checked == false) {
            text1[i].style.display = "none";
            text2[i].style.display = "block";
        }
    }
}
check();

// =======================================
// Contact Us Form
// =======================================

const firstName = document.querySelector('#firstName');
const lastName = document.querySelector('#lastName');
const email = document.querySelector('#email').value;
const phoneNumber = document.querySelector('#phoneNumber');
const subject = document.querySelector('#subject');
const message = document.querySelector('#message');
const form = document.querySelector('.contactUs');
form.addEventListener('submitBtn', (event) => {
    event.preventDefault();
    const body = {
        firstname: firstName.value,
        lastname: lastName.value,
        emailaddress: email.value,
        phonenumber: phoneNumber.value,
        subjectLine: subject.value,
        messagebody: message.value,
    }
    const sendMessage = {
        body: JSON.stringify(body),
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    }
    fetch('localhost:5001/api/ContactApi', sendMessage)
        .then(res => res.json())
        .then(() => {
            alert("message sent");
        })
        .catch(err => {
        });

});