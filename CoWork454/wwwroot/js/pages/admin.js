const newsPostData = document.querySelector('.admin .news__admin tbody');
const idElem = document.querySelector('#newsPostId');
const newsTitleElem = document.querySelector('input[name="newsTitle"]');
const newsTextElem = document.querySelector('input[name="newsText"]');
const newsPhotoElem = document.querySelector('input[name="newsPhoto"]');
const newsTagElem = document.querySelector('input[name="newsTag"]');
const saveBtn = document.querySelector('#newsPostSaveButton');
const clearBtn = document.querySelector('#newsPostClearButton');
const onDelete = (evt) => {
    evt.preventDefault();
    const deleteBtn = evt.currentTarget;
    const newsPostId = deleteBtn.getAttribute('data-newspost-id');
    deleteExisting(newsPostId);
}
const onSelect = (evt) => {
    evt.preventDefault();
    const selectBtn = evt.currentTarget;
    const newsPostId = selectBtn.getAttribute('data-newspost-id');
    getDetails(newsPostId);
}
const onSave = (evt) => {
    evt.preventDefault();
    (!!idElem.value) ? updateExisting(parseInt(idElem.value)): addNew(newsTitleElem.value, newsTitleElem.value, newsTagElem.value);
}
const onClear = (evt) => {
    clearForm();
}

const getAll = () => {
    newsPostData.innerHTML = '';
    fetch('/api/NewsPostApi')
        .then(res => res.json())
        .then(newsPosts => {
            newsPosts.forEach(newsPost => {
                const newsPostIdCell = document.createElement('td');
                const newsAuthorCell = document.createElement('td');
                const newsDateTimeCell = document.createElement('td');
                const newsTitleCell = document.createElement('td');
                const newsTextCell = document.createElement('td');
                const newsPhotoCell = document.createElement('td');
                const newsTagCell = document.createElement('td');
                const action1Cell = document.createElement('td');
                const action2Cell = document.createElement('td');
                newsPostIdCell.innerText = newsPost.id;
                newsAuthorCell.innerText = newsPost.authorId;
                newsDateTimeCell.innerText = newsPost.dateTimePosted;
                newsTitleCell.innerText = newsPost.newsTitle;
                newsTextCell.innerText = newsPost.newsText;
                newsPhotoCell.innerText = newsPost.newsPhoto;
                newsTagCell.innerText = newsPost.newsTag;
                const deleteBtn = document.createElement('button');
                const selectBtn = document.createElement('button');
                deleteBtn.innerText = 'Delete';
                deleteBtn.setAttribute('data-newspost-id', newsPost.id);
                // deleteBtn.className;
                deleteBtn.addEventListener('click', onDelete);
                selectBtn.innerText = 'Select';
                selectBtn.setAttribute('data-newspost-id', newsPost.id);
                // selectBtn.className;
                selectBtn.addEventListener('click', onSelect);
                action1Cell.appendChild(deleteBtn);
                action2Cell.appendChild(selectBtn);
                const row = document.createElement('tr');
                row.appendChild(newsPostIdCell);
                row.appendChild(newsAuthorCell);
                row.appendChild(newsDateTimeCell);
                row.appendChild(newsTitleCell);
                row.appendChild(newsTextCell);
                row.appendChild(newsPhotoCell);
                row.appendChild(newsTagCell);
                row.appendChild(action1Cell);
                row.appendChild(action2Cell);
                newsPostData.appendChild(row);
            })
        })
        .catch(err => {});
}

const getDetails = (newsPostId) => {
    fetch(`/api/NewsPostApi/${newsPostId}`)
        .then(res => res.json())
        .then(newsPost => {
            idElem.value = newsPost.id;
            newsTitleElem.value = newsPost.newsTitle;
            newsTextElem.value = newsPost.newsText;
            newsPhotoElem.value = newsPost.newsPhoto;
            newsTagElem.value = newsPost.newsTag;
        })
        .catch(err => { Console.log(err) });
}

const addNew = (newsTitle, newsText, newsPhoto, newsTag) => {
    const body = {
        newsDateTime: Date.now(),
        newsTitle: newsTitle,
        newsText: newsText,
        newsPhoto: newsPhoto,
        newsTag: newsTag
    }
    const fetchOptions = {
        body: JSON.stringify(body),
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    };
    fetch('/api/NewsPostApi', fetchOptions)
        .then(res => {
            clearForm();
            getAll();
        })
        .catch(err => { Console.log(err) });
}
const updateExisting = (newsPostId, newsTitle, newsText, newsPhoto, newsTag) => {
    const body = {
        id: newsPostId,
        newsTitle: newsTitle,
        newsTitle: newsText,
        newsPhoto: newsPhoto,
        newsTag: newsTag
    }
    const fetchOptions = {
        body: JSON.stringify(body),
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        }
    };
    fetch(`/api/NewsPostApi/${newsPostId}`, fetchOptions)
        .then(res => res.json())
        .then(() => {
            clearForm();
            getAll();
        })
        .catch(err => {});
}
const deleteExisting = (newsPostId) => {
    const fetchOptions = {
        method: 'DELETE'
    };
    fetch(`/api/NewsPostApi/${newsPostId}`, fetchOptions)
        .then(res => res.json())
        .then(() => {
            console.log('News Post Deleted');
        })
        .catch(err => {
            getAll();
        });
}
const clearForm = () => {
    idElem.value = '';
    newsTitleElem.value = '';
    newsTagElem.value = '';
    newsPhotoElem.value = '';
    newsTagElem.value = '';
}
saveBtn.addEventListener('click', onSave);
clearBtn.addEventListener('click', onClear);
getAll();