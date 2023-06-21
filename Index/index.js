fetch('header.html')
    .then(response => response.text())
    .then(data => {
        const pageHeader = document.querySelector('div#pageHeader')
        pageHeader.innerHTML = data;
    }) 