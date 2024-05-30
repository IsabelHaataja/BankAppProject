/*!
* Start Bootstrap - Grayscale v7.0.6 (https://startbootstrap.com/theme/grayscale)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-grayscale/blob/master/LICENSE)
*/
//
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Navbar shrink function
    var navbarShrink = function () {
        const navbarCollapsible = document.body.querySelector('#mainNav');
        if (!navbarCollapsible) {
            return;
        }
        if (window.scrollY === 0) {
            navbarCollapsible.classList.remove('navbar-shrink')
        } else {
            navbarCollapsible.classList.add('navbar-shrink')
        }

    };

    // Shrink the navbar 
    navbarShrink();

    // Shrink the navbar when page is scrolled
    document.addEventListener('scroll', navbarShrink);

    // Activate Bootstrap scrollspy on the main nav element
    const mainNav = document.body.querySelector('#mainNav');
    if (mainNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#mainNav',
            rootMargin: '0px 0px -40%',
        });
    }

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });
});

var skip = 20;
function loadMoreTransactions(accountId) {
    fetch(`/Account/AccountDetails/${accountId}?handler=MoreTransactions&skip=${skip}`)
        .then(response => {
            console.log('Fetch response:', response);
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Fetched data:', data);
            var list = document.getElementById("transactionList");
            if (data.length === 0) {
                var loadMoreBtn = document.getElementById("loadMoreBtn");
                loadMoreBtn.innerText = "No more transactions";
                loadMoreBtn.disabled = true;
            } else {
                data.forEach(transaction => {
                    var row = document.createElement("tr");
                    row.innerHTML = `
                        <td>${new Date(transaction.date).toLocaleDateString()}</td>
                        <td>${transaction.type}</td>
                        <td>${transaction.operation}</td>
                        <td>${transaction.amount}</td>
                        <td>${transaction.balance}</td>
                        <td>${transaction.Symbol}</td>`;
                    list.appendChild(row);
                });
                skip += 20;
            }
        })
        .catch(error => console.error('Error fetching more transactions', error));
}

document.addEventListener('DOMContentLoaded', function () {
    var loadMoreBtn = document.getElementById("loadMoreBtn");
    if (loadMoreBtn) {
        loadMoreBtn.addEventListener('click', function () {
            var accountId = loadMoreBtn.getAttribute('data-account-id');
            loadMoreTransactions(accountId);
        });
    }
});
