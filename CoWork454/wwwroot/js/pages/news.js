fetch('/api/NewsPostApi')
    .then(res => res.json())
    .then((data) => {
        console.log(data)
    })
    .catch(err => {
    });