/*!
* Start Bootstrap - Grayscale v7.0.6 (https://startbootstrap.com/theme/grayscale)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-grayscale/blob/master/LICENSE)
*/
//
// Scripts
//

//window.addEventListener('DOMContentLoaded', event => {

//    // Navbar shrink function
//    var navbarShrink = function () {
//        const navbarCollapsible = document.body.querySelector('#mainNav');
//        if (!navbarCollapsible) {
//            return;
//        }
//        if (window.scrollY === 0) {
//            navbarCollapsible.classList.remove('navbar-shrink')
//        } else {
//            navbarCollapsible.classList.add('navbar-shrink')
//        }

//    };

//    // Shrink the navbar
//    navbarShrink();

//    // Shrink the navbar when page is scrolled
//    document.addEventListener('scroll', navbarShrink);

//    // Activate Bootstrap scrollspy on the main nav element
//    const mainNav = document.body.querySelector('#mainNav');
//    if (mainNav) {
//        new bootstrap.ScrollSpy(document.body, {
//            target: '#mainNav',
//            rootMargin: '0px 0px -40%',
//        });
//    }

//    // Collapse responsive navbar when toggler is visible
//    const navbarToggler = document.body.querySelector('.navbar-toggler');
//    const responsiveNavItems = [].slice.call(
//        document.querySelectorAll('#navbarResponsive .nav-link')
//    );
//    responsiveNavItems.map(function (responsiveNavItem) {
//        responsiveNavItem.addEventListener('click', () => {
//            if (window.getComputedStyle(navbarToggler).display !== 'none') {
//                navbarToggler.click();
//            }
//        });
//    });
//});

//var skip = 20;
//function loadMoreTransactions(accountId) {
//    fetch(`/Account/AccountDetails/${accountId}?handler=MoreTransactions&skip=${skip}`)
//        .then(response => {
//            console.log('Fetch response:', response);
//            if (!response.ok) {
//                throw new Error('Network response was not ok');
//            }
//            return response.json();
//        })
//        .then(data => {
//            console.log('Fetched data:', data);
//            var list = document.getElementById("transactionList");
//            if (data.length === 0) {
//                var loadMoreBtn = document.getElementById("loadMoreBtn");
//                loadMoreBtn.innerText = "No more transactions";
//                loadMoreBtn.disabled = true;
//            } else {
//                data.forEach(transaction => {
//                    var row = document.createElement("tr");
//                    row.innerHTML = `
//                        <td>${new Date(transaction.date).toLocaleDateString()}</td>
//                        <td>${transaction.type}</td>
//                        <td>${transaction.operation}</td>
//                        <td>${transaction.amount}</td>
//                        <td>${transaction.balance}</td>
//                        <td>${transaction.Symbol}</td>`;
//                    list.appendChild(row);
//                });
//                skip += 20;
//            }
//        })
//        .catch(error => console.error('Error fetching more transactions', error));
//}

//document.addEventListener('DOMContentLoaded', function () {
//    var loadMoreBtn = document.getElementById("loadMoreBtn");
//    if (loadMoreBtn) {
//        loadMoreBtn.addEventListener('click', function () {
//            var accountId = loadMoreBtn.getAttribute('data-account-id');
//            loadMoreTransactions(accountId);
//        });
//    }
//    // Handles deposit
//    console.log("Script loaded");
//    const form = document.getElementById('depositForm');
//    const submitButton = document.getElementById('submitButton')
//    form.addEventListener('submit', function (event) {
//        event.preventDefault();
//        submitButton.disabled = true;
//        makeDeposit();
//    });
//});

//// Handles deposit
////document.addEventListener('DOMContentLoaded', function () {
////    console.log("Script loaded");
////    const form = document.getElementById('depositForm');
////    const submitButton = document.getElementById('submitButton')
////    form.addEventListener('submit', function (event) {
////        event.preventDefault();
////        submitButton.disabled = true;
////        makeDeposit();
////    });
////});

//async function makeDeposit() {
//    console.log("makeDeposit called");
//    const accountId = parseInt(document.getElementById('accountId').value);
//    const amount = parseFloat(document.getElementById('amount').value);
//    const comment = document.getElementById('comment').value;

//    const data = {
//        accountId: accountId,
//        amount: amount,
//        comment: comment
//    };
//    console.log(JSON.stringify(data));
//    try {
//        const response = await fetch(`/Account/AccountDetails/${accountId}?handler=MakeDeposit`, {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json',
//                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
//            },
//            body: JSON.stringify(data)
//        });

//        if (!response.ok) {
//            throw new Error(`HTTP error! Status: ${response.status}`);
//        }

//        const result = await response.json();
//        const resultDiv = document.getElementById('result');
//        resultDiv.innerText = result.message;
//        resultDiv.style.display = 'block';
//        if (result.success) {
//            resultDiv.className = 'alert alert-success';
//        } else {
//            resultDiv.className = 'alert alert-danger';
//        }
//    } catch (error) {
//        console.error('Error:', error);
//        document.getElementById('result').innerText = "Failed to make a deposit due to a network error.";
//    }
//}

    console.log("Loaded script.js");

    // Consolidate all event listeners into one DOMContentLoaded listener
    document.addEventListener('DOMContentLoaded', function () {
        console.log("Scripts loaded");

        // Navbar Shrink Function
        var navbarShrink = function () {
            const navbarCollapsible = document.body.querySelector('#mainNav');
            if (!navbarCollapsible) {
                return;
            }
            if (window.scrollY === 0) {
                navbarCollapsible.classList.remove('navbar-shrink');
            } else {
                navbarCollapsible.classList.add('navbar-shrink');
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

        // Load More Transactions
        var loadMoreBtn = document.getElementById("loadMoreBtn");
        if (loadMoreBtn) {
            loadMoreBtn.addEventListener('click', function () {
                var accountId = loadMoreBtn.getAttribute('data-account-id');
                console.log("Load more clicked, accountId:", accountId);
                loadMoreTransactions(accountId);
            });
        }

        // Handles Deposit
        const depositForm = document.getElementById('depositForm');
        const submitButton = document.getElementById('submitButton');
        if (depositForm) {
            // Remove any existing submit event listener to avoid duplication
            depositForm.removeEventListener('submit', submitDepositHandler);

            // Add the submit event listener
            depositForm.addEventListener('submit', submitDepositHandler);
            console.log("Added submit event listener to depositForm");
        } else {
            console.error('Deposit form not found!');
        }
    });

    function submitDepositHandler(event) {
        console.log("Deposit form submit event triggered");
        event.preventDefault();
        const submitButton = document.getElementById('submitButton');
        if (!submitButton.disabled) {
            submitButton.disabled = true;
            makeDeposit();
        } else {
            console.warn("Submit button is already disabled, avoiding duplicate submission.");
        }
    }

    // Load More Transactions
    var skip = 20;
    function loadMoreTransactions(accountId) {
        console.log("Loading more transactions, skip:", skip);
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

    // Make Deposit
    async function makeDeposit() {
        console.log("makeDeposit called");

        const accountId = parseInt(document.getElementById('accountId').value, 10);
        const amount = parseFloat(document.getElementById('amount').value);
        const comment = document.getElementById('comment').value;

        const data = {
            accountId: accountId,
            amount: amount,
            comment: comment
        };

        console.log("Sending data:", JSON.stringify(data));

        try {
            const response = await fetch(`/Account/AccountDetails/${accountId}?handler=MakeDeposit`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                },
                body: JSON.stringify(data)
            });

            console.log("Response received");
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }

            const result = await response.json();
            console.log("Server result:", result);

            const resultDiv = document.getElementById('result');
            resultDiv.innerText = result.message;
            resultDiv.style.display = 'block';
            if (result.success) {
                resultDiv.className = 'alert alert-success';
            } else {
                resultDiv.className = 'alert alert-danger';
            }
        } catch (error) {
            console.error('Error:', error);
            document.getElementById('result').innerText = "Failed to make a deposit due to a network error.";
        } finally {
            console.log("Re-enabling submit button");
            const submitButton = document.getElementById('submitButton');
            submitButton.disabled = false;
        }
    }

