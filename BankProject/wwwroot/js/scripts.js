
document.addEventListener('DOMContentLoaded', function () {
    setupNavbar();
    setupEventListeners();
    setupModals();
    setupForms();
});

// Navbar shrink function
function setupNavbar() {
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
}

function setupEventListeners() {
    // Load More Transactions
    var loadMoreBtn = document.getElementById("loadMoreBtn");
    if (loadMoreBtn) {
        loadMoreBtn.addEventListener('click', function () {
            var accountId = loadMoreBtn.getAttribute('data-account-id');
            console.log("Load more clicked, accountId:", accountId);
            loadMoreTransactions(accountId);
        });
    }
}

function setupModals() {
    // Handle Modals Show Events
    setupModal('depositModal', 'depositBtn');
    setupModal('withdrawModal', 'withdrawBtn');
    setupModal('transferModal', 'transferBtn');
}

function setupModal(modalId, buttonId) {
    var modal = new bootstrap.Modal(document.getElementById(modalId));
    var btn = document.getElementById(buttonId);
    if (btn) {
        btn.addEventListener('click', function () {
            modal.show();
        });
    }
    modal._element.addEventListener('hidden.bs.modal', function () {
        var backdrops = document.querySelectorAll('.modal-backdrop');
        backdrops.forEach(function (backdrop) {
            backdrop.remove();  // Forcefully removes the backdrop from the DOM
        });
    });
}

function setupForms() {
    // Handles forms
    setupForm('depositForm', submitDepositHandler);
    setupForm('withdrawForm', submitWithdrawHandler);
    setupForm('transferForm', submitTransferHandler);
}

function setupForm(formId, handler) {
    const form = document.getElementById(formId);
    if (form) {
        form.removeEventListener('submit', handler);
        form.addEventListener('submit', handler);
        console.log(`Added submit event listener to ${formId}`);
    } else {
        console.error(`${formId} not found!`);
    }
}

// Submit deposit
function submitDepositHandler(event) {
    console.log("Deposit form submit event triggered");
    event.preventDefault();
    const submitButton = document.getElementById('submitButton');
    if (!submitButton.disabled) {
        submitButton.disabled = true;
        makeDeposit();
    }
}

// Submit withdrawal
function submitWithdrawHandler(event) {
    event.preventDefault();
    const submitWithdrawButton = document.getElementById('submitWithdrawButton');
    if (!submitWithdrawButton.disabled) {
        submitWithdrawButton.disabled = true;
        makeWithdraw();
    }
}

// Submit transfer
function submitTransferHandler(event) {
    console.log("Transfer form submit event triggered");
    event.preventDefault();
    const submitButton = event.target.querySelector('button[type="submit"]');
    if (!submitButton.disabled) {
        submitButton.disabled = true;
        makeTransfer();
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

    const accountId = parseInt(document.getElementById('accountId').value);
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

        const resultDiv = document.getElementById('depositResult');
        resultDiv.innerText = result.message;
        resultDiv.style.display = 'block';
        resultDiv.className = result.success ? 'alert alert-success' : 'alert alert-danger';
    } catch (error) {
        console.error('Error:', error);
        document.getElementById('depositResult').innerText = "Failed to make a deposit due to a network error.";
    } finally {
        console.log("Re-enabling submit button");
        const submitButton = document.getElementById('submitButton');
        submitButton.disabled = false;
    }
}

// Make Withdraw
async function makeWithdraw() {
    console.log("makeWithdraw called");

    const accountId = parseInt(document.getElementById('accountId').value);
    const amount = parseFloat(document.getElementById('withdrawAmount').value);

    const data = {
        accountId: accountId,
        amount: amount,
    };

    console.log("Sending data:", JSON.stringify(data));

    try {
        const response = await fetch(`/Account/AccountDetails/${accountId}?handler=MakeWithdraw`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();

        const resultDiv = document.getElementById('withdrawResult');
        resultDiv.innerText = result.message;
        resultDiv.style.display = 'block';
        resultDiv.className = result.success ? 'alert alert-success' : 'alert alert-danger';

        if (!response.ok || !result.success) {
            throw new Error(result.message || `HTTP error! Status: ${response.status}`);
        }
    } catch (error) {
        console.error('Error:', error);
        document.getElementById('withdrawResult').innerText = error.message || "Failed to make a withdrawal due to a network error.";
    } finally {
        console.log("Re-enabling submit withdraw button");
        const submitButton = document.getElementById('submitWithdrawButton');
        submitButton.disabled = false;
    }
}

// Make Transfer
async function makeTransfer() {
    console.log("makeTransfer called");

    const fromAccountId = parseInt(document.getElementById('fromAccountId').value);
    const toAccountNumber = document.getElementById('toAccountNumber').value;
    const amount = parseFloat(document.getElementById('transferAmount').value);
    const comment = document.getElementById('transferComment').value;

    const data = {
        fromAccountId: fromAccountId,
        toAccountNumber: toAccountNumber,
        amount: amount,
        comment: comment
    };

    console.log("Sending data:", JSON.stringify(data));

    try {
        const response = await fetch(`/Account/AccountDetails/${fromAccountId}?handler=MakeTransfer`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();

        const resultDiv = document.getElementById('transferResult');
        resultDiv.innerText = result.message;
        resultDiv.style.display = 'block';
        resultDiv.className = result.success ? 'alert alert-success' : 'alert alert-danger';

        if (!response.ok || !result.success) {
            throw new Error(result.message || `HTTP error! Status: ${response.status}`);
        }
    } catch (error) {
        console.error('Error:', error);
        document.getElementById('transferResult').innerText = error.message || "Failed to complete the transfer due to a network error.";
    } finally {
        console.log("Re-enabling submit transfer button");
        const submitButton = document.querySelector('#transferForm button[type="submit"]');
        submitButton.disabled = false;
    }
}
