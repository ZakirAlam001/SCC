// ==============================
// Party Search
// ==============================

const searchBox = document.getElementById("searchParty");

searchBox.addEventListener("keyup", function () {

    let value = this.value.toLowerCase();

    document.querySelectorAll(".party-card").forEach(function (card) {

        let partyName = card.querySelector("h5").innerText.toLowerCase();

        if (partyName.includes(value)) {

            card.style.display = "flex";

        } else {

            card.style.display = "none";

        }

    });

});

// ==============================
// Party Click
// ==============================

document.querySelectorAll(".party-card").forEach(function (card) {

    card.addEventListener("click", function () {

        // Remove Active Class

        document.querySelectorAll(".party-card").forEach(function (item) {

            item.classList.remove("active");

        });

        // Active Card

        this.classList.add("active");

        // Party Data

        let partyName = this.querySelector("h5").innerText;
        let mobile = this.querySelector("small").innerText;
        let balance = this.querySelector("h6").innerText;

        // Update Right Panel

        document.querySelector(".ledger-header h2").innerHTML = partyName;

        document.querySelector(".ledger-header p").innerHTML =
            "Mobile : " + mobile;

        document.querySelector(".ledger-header h3").innerHTML =
            balance;

        // Dummy Ledger

        loadLedger(partyName);

    });

});

// ==============================
// Dummy Ledger
// ==============================

function loadLedger(partyName) {

    const tbody = document.querySelector(".ledger-table tbody");

    tbody.innerHTML = "";

    const rows = [

        ["01-Jul-2026", "Sale", "2500", "-", "2500"],

        ["02-Jul-2026", "Payment", "-", "1000", "1500"],

        ["03-Jul-2026", "Sale", "5000", "-", "6500"]

    ];

    rows.forEach(function (row) {

        tbody.innerHTML += `

        <tr>

            <td>${row[0]}</td>

            <td>${row[1]}</td>

            <td>${row[2]}</td>

            <td>${row[3]}</td>

            <td>${row[4]}</td>

        </tr>

        `;

    });

    console.log("Ledger Loaded : " + partyName);

}

// ==============================
// ASP.NET MVC AJAX (Ready)
// ==============================

/*

function loadLedger(partyId){

    $.ajax({

        url:'/Party/GetLedger',

        type:'GET',

        data:{ id:partyId },

        success:function(res){

            $("#ledgerContainer").html(res);

        }

    });

}

*/

// ==============================
// Hover Effect
// ==============================

document.querySelectorAll(".party-card").forEach(function(card){

    card.addEventListener("mouseenter",function(){

        this.style.transition=".3s";

    });

});

// ==============================
// Page Load
// ==============================

window.onload = function(){

    document.querySelector(".party-card").click();

};