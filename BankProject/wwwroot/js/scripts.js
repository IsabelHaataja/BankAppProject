
    console.log("Loaded script.js");

    // Consolidate all event listeners into one DOMContentLoaded listener
    document.addEventListener('DOMContentLoaded', function () {

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


        // Handle Deposit Modal Show Event
        var depositModal = new bootstrap.Modal(document.getElementById('depositModal'));
        var depositBtn = document.getElementById("depositBtn");
        if (depositBtn) {
            depositBtn.addEventListener('click', function () {
                depositModal.show();
            });
        }
        depositModal._element.addEventListener('hidden.bs.modal', function () {
            var backdrops = document.querySelectorAll('.modal-backdrop');
            backdrops.forEach(function (backdrop) {
                backdrop.remove();  // Forcefully removes the backdrop from the DOM
            });
        });

        // Handles Deposit form
        const depositForm = document.getElementById('depositForm');
        const submitButton = document.getElementById('submitButton');
        if (depositForm) {
            // Removes existing submit event listeners to avoid duplication
            depositForm.removeEventListener('submit', submitDepositHandler);

            // Add submit event listener
            depositForm.addEventListener('submit', submitDepositHandler);
            console.log("Added submit event listener to depositForm");
        } else {
            console.error('Deposit form not found!');
        }

        // Handles witdraw Modal Show Event
        var withdrawModal = new bootstrap.Modal(document.getElementById('withdrawModal'));
        var withdrawBtn = document.getElementById("withdrawBtn");
        if (withdrawBtn) {
            withdrawBtn.addEventListener('click', function () {
                withdrawModal.show();
            });
        }

        withdrawModal._element.addEventListener('hidden.bs.modal', function () {
            var backdrops = document.querySelectorAll('.modal-backdrop');
            backdrops.forEach(function (backdrop) {
                backdrop.remove();  // Forcefully removes the backdrop from the DOM
            });
        });

        //Withdrawal Form 
        const withdrawForm = document.getElementById('withdrawForm');
        const submitWithdraw = document.getElementById('submitWithdraw')
        if (withdrawForm) {
            withdrawForm.removeEventListener('submit', submitWithdrawHandler);

            withdrawForm.addEventListener('submit', submitWithdrawHandler);
        } else {
            console.error('Withdraw form not found!');
        }

        //Handle transfer modal show
        var transferModal = new bootstrap.Modal(document.getElementById('transferModal'));
        var transferBtn = document.getElementById("transferBtn");
        if (transferBtn) {
            transferBtn.addEventListener('click', function () {
                transferModal.show();
            });
        }
        transferModal._element.addEventListener('hidden.bs.modal', function () {
            var backdrops = document.querySelectorAll('.modal-backdrop');
            backdrops.forEach(function (backdrop) {
                backdrop.remove();
            });
        });

        // Handles Transfer form
        const transferForm = document.getElementById('transferForm');
        if (transferForm) {
            transferForm.removeEventListener('submit', submitTransferHandler);

            transferForm.addEventListener('submit', submitTransferHandler);
            console.log("Added submit event listener to transferForm");
        } else {
            console.error('Transfer form not found!');
        }

    });

    //Submit deposit
    function submitDepositHandler(event) {
        console.log("Deposit form submit event triggered");
        event.preventDefault();
        const submitButton = document.getElementById('submitButton');
        if (!submitButton.disabled) {
            submitButton.disabled = true;
            makeDeposit();
        }
    }
    //submit withdrawal
    function submitWithdrawHandler(event) {
        event.preventDefault();
        const submitWithdrawButton = document.getElementById('submitWithdrawButton');

        if (!submitWithdrawButton.disabled) {
            submitWithdrawButton.disabled = true;
            makeWithdraw();
        }
    }
    //submit transfer
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
            const submitWithdrawButton = document.getElementById('submitWithdrawButton');
            submitWithdrawButton.disabled = false;
        }
    }

    // Make transfer
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

        //// Hide modal if successful
        //if (result.success) {
        //    var transferModal = bootstrap.Modal.getInstance(document.getElementById('transferModal'));
        //    transferModal.hide();
        //}
    } catch (error) {
        console.error('Error:', error);
        document.getElementById('transferResult').innerText = error.message || "Failed to complete the transfer due to a network error.";
    } finally {
        console.log("Re-enabling submit transfer button");
        const submitButton = document.querySelector('#transferForm button[type="submit"]');
        submitButton.disabled = false;
    }
}