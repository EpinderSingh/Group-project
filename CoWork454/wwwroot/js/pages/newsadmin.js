const newsPostData = document.querySelector('.admin.news__admin tbody');
const idElem = document.querySelector('#newsPostId');
const newsTitleElem = document.querySelector('input[name="newsTitle"]');
const newsTextElem = document.querySelector('textarea[name="newsText"]');
const newsPhotoElem = document.querySelector('input[name="newsPhoto"]');
const newsPhotoDisplayElem = document.querySelector('.admin.news__admin.news__post__photo');
const newsTagElem = document.querySelector('select[name="newsTag"]');
const saveBtn = document.querySelector('#newsPostSaveButton');
const clearBtn = document.querySelector('#newsPostClearButton');
const previewBtn = document.querySelector('#newsPostPreviewButton');

//update for preview news post method
const newsPreviewContainer = document.querySelector('.admin.modal-container');
const newsPreviewImage = document.querySelector('#preview__image');
const newsPreviewTitle = document.querySelector('#preview__title');
const newsPreviewText = document.querySelector('#preview__text');
const newsPreviewAuthor = document.querySelector('#preview__author');
const newsPreviewDate = document.querySelector('.news-date');

const onPreview = (evt) => {
    evt.preventDefault();
    newsPreviewContainer.classList.add('show');
    newsPreviewImage.src = newsPhotoDisplayElem.src;
    newsPreviewTitle.innerText = newsTitleElem.value;
    newsPreviewText.innerText = newsTextElem.value;
    newsPreviewAuthor.innerText = 'CoWork454 Admin';
    newsPreviewDate.innerText = new Date().toDateString();

    newsPreviewContainer.addEventListener('click', (evt) => {
        newsPreviewContainer.classList.remove('show');
    })
}


const onDelete = (evt) => {
    evt.preventDefault();
    const deleteBtn = evt.currentTarget;
    const newsPostId = deleteBtn.getAttribute('data-newspost-id');
    deleteExisting(parseInt(newsPostId));
}
const onSelect = (evt) => {
    evt.preventDefault();
    const selectBtn = evt.currentTarget;
    const newsPostId = selectBtn.getAttribute('data-newspost-id');
    getDetails(parseInt(newsPostId));
}
const onSave = (evt) => {
    evt.preventDefault();
    let updatePhoto = (!!newsPhotoElem.files[0]) ? newsPhotoElem.files[0] : newsPhotoDisplayElem.src;
    (!!idElem.value) ? updateExisting(parseInt(idElem.value), newsTitleElem.value, newsTextElem.value, updatePhoto, parseInt(newsTagElem.value)): addNew(newsTitleElem.value, newsTextElem.value, newsPhotoElem.files[0], parseInt(newsTagElem.value));
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
                const newsPhotoCell = document.createElement('td');
                const newsPhotoView = document.createElement('img');
                const newsTagCell = document.createElement('td');
                const action1Cell = document.createElement('td');
                const action2Cell = document.createElement('td');
                newsPostIdCell.innerText = newsPost.id;
                newsAuthorCell.innerText = `${newsPost.author.firstName} ${newsPost.author.lastName}`;

                var datePosted = new Date(Date.parse(newsPost.dateTimePosted));

                newsDateTimeCell.innerText = datePosted.toDateString();
                newsTitleCell.innerText = newsPost.newsTitle;

                newsPhotoCell.appendChild(newsPhotoView);
                newsPhotoView.src = newsPost.newsPhoto;
                newsPhotoView.width = "200";
                newsPhotoView.height = "200";

                newsTagCell.innerText = newsPost.newsTagLabel;
                const deleteBtn = document.createElement('button');
                const selectBtn = document.createElement('button');
                deleteBtn.innerText = 'Delete';
                deleteBtn.setAttribute('data-newspost-id', newsPost.id);
                deleteBtn.className = "news__btn";
                deleteBtn.addEventListener('click', onDelete);
                selectBtn.innerText = 'Select';
                selectBtn.setAttribute('data-newspost-id', newsPost.id);
                selectBtn.className = "news__btn";
                selectBtn.addEventListener('click', onSelect);
                action1Cell.appendChild(deleteBtn);
                action2Cell.appendChild(selectBtn);
                const row = document.createElement('tr');
                row.appendChild(newsPostIdCell);
                row.appendChild(newsAuthorCell);
                row.appendChild(newsDateTimeCell);
                row.appendChild(newsTitleCell);
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
            newsPhotoDisplayElem.src = newsPost.newsPhoto;
            newsTagElem.selectedIndex = newsPost.newsTag + 1;
        })
        .catch(err => { console.log(err) });
}

const addNew = (newsTitle, newsText, newsPhoto, newsTag) => {
    const data = new FormData();
    data.append('File', newsPhoto);
    data.append('NewsDateTime', Date.now());
    data.append('NewsTitle', newsTitle);
    data.append('NewsText', newsText);
    data.append('NewsTag', newsTag);
    const fetchOptions = {
        body: data,
        method: 'POST',
        headers: {
            'Accept': 'application/json',
        },
    };
    fetch('/api/NewsPostApi', fetchOptions)
        .then(res => {
            clearForm();
            getAll();
        })
        .catch(err => { console.log(err) });
}
const updateExisting = (newsPostId, newsTitle, newsText, newsPhoto, newsTag) => {
    const data = new FormData();
    data.append('Id', newsPostId);
    data.append('File', newsPhoto);
    data.append('NewsDateTime', Date.now());
    data.append('NewsTitle', newsTitle);
    data.append('NewsText', newsText);
    data.append('NewsTag', newsTag);

    const fetchOptions = {
        body: data,
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
        }
    };

    fetch(`/api/NewsPostApi/${newsPostId}`, fetchOptions)
        .then(res => {
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
    newsTextElem.value = '';
    newsPhotoDisplayElem.src = '';
    newsPhotoElem.value = '';
    newsTagElem.value = '';
}
saveBtn.addEventListener('click', onSave);
clearBtn.addEventListener('click', onClear);
previewBtn.addEventListener('click', onPreview);
getAll();