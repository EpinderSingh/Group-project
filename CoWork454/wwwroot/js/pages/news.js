

fetch('/api/NewsPostApi')
  .then((res) => res.json())
  .then((blogs) => {
    blogs.forEach((blog) => {
      
      const newsTitle = document.createElement('h2');
      const newsText = document.createElement('p');
      const newsPhoto = document.createElement('img');

      newsTitle.innerText = blog.newsTitle;
      newsText.innerText = blog.newsText;
      newsPhoto.src = blog.newsPhoto;

        const newsContainer = document.querySelector('.news-post');

      newsContainer.appendChild(newsTitle);
      newsContainer.appendChild(newsText);
      newsContainer.appendChild(newsPhoto);

      //   const div = document.createElement('div');
      //   div.appendChild(newsTitle);
    });
  });
