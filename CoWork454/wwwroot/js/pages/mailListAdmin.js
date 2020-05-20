const mailListData = document.querySelector('.subdata');
const clearBtn = document.querySelector('#newsPostClearButton');

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
                mailListEmailCell.innerText = mailList.emailAddress;
                mailListFirstNameCell.innerText = mailList.firstName;
                mailListLastNameCell.innerText = mailList.lastName;

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

getAll();

const onDelete = (evt) => {
    evt.preventDefault();
    const deleteBtn = evt.currentTarget;
    const newsPostId = deleteBtn.getAttribute('data-newspost-id');
    deleteExisting(parseInt(newsPostId));
}