const resourceData = document.querySelector('.admin.resource__admin tbody');
const idElem = document.querySelector('#resourceId');
const resourceNameElem = document.querySelector('input[name="resourceName"]');
const resourceDescriptionElem = document.querySelector('textarea[name="resourceDescription"]');
const resourcePhotoElem = document.querySelector('input[name="resourcePhoto"]');
const resourcePhotoDisplayElem = document.querySelector('.admin.resource__admin.resource__photo');
const resourceMaxCapacityElem = document.querySelector('input[name="resourceMaxCapacity"]');
const resourceHasVCElem = document.querySelector('input[name="resourceHasVC"]');
const saveBtn = document.querySelector('#resourceSaveButton');
const clearBtn = document.querySelector('#resourceClearButton');
const previewBtn = document.querySelector('#resourcePreviewButton');

const resourcePreviewContainer = document.querySelector('.admin.modal-container');
const resourcePreviewImage = document.querySelector('#preview__image');
const resourcePreviewName = document.querySelector('#preview__name');
const resourcePreviewDescription = document.querySelector('#preview__description');
const resourcePreviewMaxCapacity = document.querySelector('#preview__max__capacity');
const resourcePreviewHasVC = document.querySelector('#preview__has__vc');

const onPreview = (evt) => {
    evt.preventDefault();
    resourcePreviewContainer.classList.add('show');
    resourcePreviewImage.src = resourcePhotoDisplayElem.src;
    resourcePreviewName.innerText = resourceNameElem.value;
    resourcePreviewDescription.innerText = resourceDescriptionElem.value;
    resourcePreviewMaxCapacity.innerText = resourceMaxCapacityElem.value;
    resourcePreviewHasVC.innerText = resourceHasVCElem.value;

    resourcePreviewContainer.addEventListener('click', (evt) => {
        resourcePreviewContainer.classList.remove('show');
    })
}


const onDelete = (evt) => {
    evt.preventDefault();
    const deleteBtn = evt.currentTarget;
    const resourceId = deleteBtn.getAttribute('data-resource-id');
    deleteExisting(parseInt(resourceId));
}
const onSelect = (evt) => {
    evt.preventDefault();
    const selectBtn = evt.currentTarget;
    const resourceId = selectBtn.getAttribute('data-resource-id');
    getDetails(parseInt(resourceId));
}
const onSave = (evt) => {
    evt.preventDefault();
    let updatePhoto = (!!resourcePhotoElem.files[0]) ? resourcePhotoElem.files[0] : resourcePhotoDisplayElem.src;
    (!!idElem.value) ? updateExisting(parseInt(idElem.value), resourceNameElem.value, resourceDescriptionElem.value, updatePhoto, resourceMaxCapacityElem.value, resourceHasVCElem.checked): addNew(resourceNameElem.value, resourceDescriptionElem.value, resourcePhotoElem.files[0], resourceMaxCapacityElem.value, resourceHasVCElem.checked);
}

const onClear = (evt) => {
    clearForm();
}

const getAll = () => {
    resourceData.innerHTML = '';
    fetch('/api/ResourceApi')
        .then(res => res.json())
        .then(resources => {
            resources.forEach(resource => {
                /*create cells*/
                const resourceIdCell = document.createElement('td');
                const resourceNameCell = document.createElement('td');
                const resourceDescriptionCell = document.createElement('td');
                const resourcePhotoCell = document.createElement('td');
                const resourcePhotoView = document.createElement('img');
                const resourceMaxCapacityCell = document.createElement('td');
                const resourceHasVCCell = document.createElement('td');
                const action1Cell = document.createElement('td');
                const action2Cell = document.createElement('td');

                /*fill cells*/
                resourceIdCell.innerText = resource.id;
                resourceNameCell.innerText = resource.resourceName;
                resourceDescriptionCell.innerHTML = resource.resourceDescription;
                resourcePhotoCell.appendChild(resourcePhotoView);
                resourcePhotoView.src = resource.resourceImage;
                resourcePhotoView.width = "200";
                resourcePhotoView.height = "200";
                resourceMaxCapacityCell.innerText = resource.resourceMaxCapacity;
                /* check this y/n from bool */
                resourceHasVCCell.innerText = resource.resourceHasVC.toString();

                /*set up buttons */
                const deleteBtn = document.createElement('button');
                const selectBtn = document.createElement('button');
                deleteBtn.innerText = 'Delete';
                deleteBtn.setAttribute('data-resource-id', resource.id);
                deleteBtn.className = "admin__btn";
                deleteBtn.addEventListener('click', onDelete);
                selectBtn.innerText = 'Select';
                selectBtn.setAttribute('data-resource-id', resource.id);
                selectBtn.className = "admin__btn";
                selectBtn.addEventListener('click', onSelect);
                action1Cell.appendChild(deleteBtn);
                action2Cell.appendChild(selectBtn);

                /*form table row*/
                const row = document.createElement('tr');
                row.appendChild(resourceIdCell);
                row.appendChild(resourceNameCell);
                row.appendChild(resourceDescriptionCell);
                row.appendChild(resourcePhotoCell);
                row.appendChild(resourceMaxCapacityCell);
                row.appendChild(resourceHasVCCell);
                row.appendChild(action1Cell);
                row.appendChild(action2Cell);
                resourceData.appendChild(row);
            })
        })
        .catch(err => {});
}

const getDetails = (resourceId) => {
    fetch(`/api/ResourceApi/${resourceId}`)
        .then(res => res.json())
        .then(resource => {
            idElem.value = resource.id;
            resourceNameElem.value = resource.resourceName;
            resourceDescriptionElem.value = resource.resourceDescription;
            resourcePhotoDisplayElem.src = resource.resourceImage;
            resourceMaxCapacityElem.value = resource.resourceMaxCapacity;
            resourceHasVCElem.checked = resource.resourceHasVC;
        })
        .catch(err => { console.log(err) });
}

const addNew = (resourceName, resourceDescription, resourceImage, resourceMaxCapacity, resourceHasVC) => {
    const data = new FormData();
    data.append('File', resourceImage);
    data.append('ResourceName', resourceName);
    data.append('ResourceDescription', resourceDescription);
    data.append('ResourceMaxCapacity', resourceMaxCapacity);
    data.append('ResourceHasVC', resourceHasVC);
    const fetchOptions = {
        body: data,
        method: 'POST',
        headers: {
            'Accept': 'application/json',
        },
    };
    fetch('/api/ResourceApi', fetchOptions)
        .then(res => {
            clearForm();
            getAll();
        })
        .catch(err => { console.log(err) });
}
const updateExisting = (resourceId, resourceName, resourceDescription, resourceImage, resourceMaxCapacity, resourceHasVC) => {
    const data = new FormData();
    data.append('Id', resourceId);
    data.append('File', resourceImage);
    data.append('ResourceName', resourceName);
    data.append('ResourceDescription', resourceDescription);
    data.append('ResourceMaxCapacity', resourceMaxCapacity);
    data.append('ResourceHasVC', resourceHasVC);

    const fetchOptions = {
        body: data,
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
        }
    };

    fetch(`/api/ResourceApi/${resourceId}`, fetchOptions)
        .then(res => {
            clearForm();
            getAll();
        })
        .catch(err => {});
}
const deleteExisting = (resourceId) => {
    const fetchOptions = {
        method: 'DELETE'
    };
    fetch(`/api/ResourceApi/${resourceId}`, fetchOptions)
        .then(res => res.json())
        .then(() => {
            console.log('Resource Deleted');
        })
        .catch(err => {
            getAll();
        });
}
const clearForm = () => {
    idElem.value = '';
    resourceNameElem.value = '';
    resourceDescriptionElem.value = '';
    resourcePhotoDisplayElem.src = '';
    resourcePhotoElem.value = '';
    resourceMaxCapacityElem.value = '';
    resourceHasVCElem.checked = false;
}
saveBtn.addEventListener('click', onSave);
clearBtn.addEventListener('click', onClear);
previewBtn.addEventListener('click', onPreview);
getAll();