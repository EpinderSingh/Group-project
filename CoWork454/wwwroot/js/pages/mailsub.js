let mailFirstName = document.querySelector('#mailFirstName');
let mailLastName = document.querySelector('#mailLastName');
let subEmail = document.querySelector('#subEmail');
let subscriptionForm = document.querySelector('.mailingList');
subscriptionForm.addEventListener('submit', (event) => {
    alert('yo')
    event.preventDefault();
    const body = {
        firstName: mailFirstName.value,
        lastName: mailLastName.value,
        emailAddress: subEmail.value,
    }
    const sendMessage = {
        body: JSON.stringify(body),
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    }
    fetch('/api/MailingListApi', sendMessage)
        .then(res => res.json())
        .then(() => {
            alert("Thank you for your subscription");
        })
        .catch(err => {
        });

});

