const mailListData = document.querySelector('.admin mail__list tbody');

const getAll = () => {
    mailListData.innerHTML = '';
    fetch('/api/MailingListApi')
        .then(res => res.json())
        .then(mailingList => {
            mailingList.forEach(mailList => {
                const mailListIdCell = document.createElement('td');
                const mailListEmailCell = document.createElement('td');
                const mailListFirstNameCell = document.createElement('td');
                const mailListLastNameCell = document.createElement('td');
                mailListIdCell.innerText = mailList.id;
                mailListEmailCell.innerText = mailList.emailId;
                mailListFirstNameCell.innerText = mailList.firstNameid;
                mailListLastNameCell.innerText = mailList.lastNameId;

                const row = document.createElement('tr');
                row.appendChild(mailListIdCell);
                row.appendChild(mailListEmailCell);
                row.appendChild(mailListFirstNameCell);
                row.appendChild(mailListLastNameCell);
                mailListData.appendChild(row);
            })
        })
        .catch(err => { });
}