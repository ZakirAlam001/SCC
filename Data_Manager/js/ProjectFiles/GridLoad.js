function DepositGrid(loadData) {
    var tbody = $('#depositlist tbody');
    tbody.empty();
    var Dtotal = 0
    if (loadData.length > 0) {
        for (var i = 0; i < loadData.length; i++) {
            var rowData = loadData[i];
            var DepositType = rowData.DepositType
            var Amount = rowData.Amount
            try {
                var date = new Date(parseFloat(rowData.Date.match(/\d+/g)));
                var Date1 = date.getDate() + '-' + parseInt(date.getMonth() + 1) + "-" + + date.getFullYear();
            }
            catch (err) {
                Date1 = "";
            }
            var description = rowData.description


            var row = '<tr class="">' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + Date1 + '</td>' +
                '<td >' + description + '</td>' +
                '<td>' + DepositType + '</td>' +
                '<td align="center">' + Amount + '</td>' +
                '</tr>';
            tbody.append(row);
            Dtotal = Dtotal + Amount;


        }
        var row = '<tr class="ffooter">' +
            '<td colspan=4> Total </td>' +
            '<td colspan=1>' + Dtotal + '</td>' +
            '</tr>';
        tbody.append(row);
    } else {
        var row = '<tr class="col">' +
            '<td colspan=5> No Data </td>' +
            '</tr>';
        tbody.append(row);
    }
}

function SaleGrid(loadData) {
    var tbody = $('#customeroder tbody');
    tbody.empty();
    var Dtotal = 0
    if (loadData.length > 0) {
        for (var i = 0; i < loadData.length; i++) {
            var rowData = loadData[i];
            
            var Amount = rowData.Amount


            var row = '<tr class="">' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + rowData.TrasactionID + '</td>' +
                '<td>' + rowData.Date + '</td>' +
                '<td >' + rowData.CustomerName + '</td>' +
                '<td>' + rowData.SaleItemName + '</td>' +
                '<td align="center">' + rowData.Price + '</td>' +
                '<td align="center">' + rowData.Wight + '</td>' +
                '<td align="center">' + Amount + '</td>' +
                '</tr>';
            tbody.append(row);
            Dtotal = Dtotal + Amount;


        }
        var row = '<tr class="ffooter">' +
            '<td colspan=7> Total </td>' +
            '<td colspan=1>' + Dtotal + '</td>' +
            '</tr>';
        tbody.append(row);
    } else {
        var row = '<tr class="col">' +
            '<td colspan=8> No Data </td>' +
            '</tr>';
        tbody.append(row);
    }
}

function PurchaseGrid(loadData) {
    var tbody = $('#vendorgrid tbody');
    tbody.empty();
    var Dtotal = 0
    if (loadData.length > 0) {
        for (var i = 0; i < loadData.length; i++) {
            var rowData = loadData[i];

            var Amount = rowData.Amount


            var row = '<tr class="">' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + rowData.TrasactionID + '</td>' +
                '<td>' + rowData.Date + '</td>' +
                '<td >' + rowData.VendorName + '</td>' +
                '<td>' + rowData.SaleItemName + '</td>' +
                '<td align="center">' + rowData.Price + '</td>' +
                '<td align="center">' + rowData.Wight + '</td>' +
                '<td align="center">' + Amount + '</td>' +
                '</tr>';
            tbody.append(row);
            Dtotal = Dtotal + Amount;


        }
        var row = '<tr class="ffooter">' +
            '<td colspan=7> Total </td>' +
            '<td colspan=1>' + Dtotal + '</td>' +
            '</tr>';
        tbody.append(row);
    } else {
        var row = '<tr class="col">' +
            '<td colspan=8> No Data </td>' +
            '</tr>';
        tbody.append(row);
    }
}

function ExpenseGrid(loadData) {
    var tbody = $('#expense tbody');
    tbody.empty();
    var Dtotal = 0
    if (loadData.length > 0) {
        for (var i = 0; i < loadData.length; i++) {
            var rowData = loadData[i];

            var Amount = rowData.Amount


            var row = '<tr class="">' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + rowData.TrasactionID + '</td>' +
                '<td>' + rowData.Date + '</td>' +
                '<td >' + rowData.Expensetype + '</td>' +
                '<td>' + rowData.Description + '</td>' +
                '<td align="center">' + Amount + '</td>' +
                '</tr>';
            tbody.append(row);
            Dtotal = Dtotal + Amount;


        }
        var row = '<tr class="ffooter">' +
            '<td colspan=5> Total </td>' +
            '<td colspan=1>' + Dtotal + '</td>' +
            '</tr>';
        tbody.append(row);
    } else {
        var row = '<tr class="col">' +
            '<td colspan=6> No Data </td>' +
            '</tr>';
        tbody.append(row);
    }
}

function CusPayment(loadData) {
    var tbody = $('#Cpayment tbody');
    tbody.empty();
    var Dtotal = 0
    if (loadData.length > 0) {
        for (var i = 0; i < loadData.length; i++) {
            var rowData = loadData[i];

            var Amount = rowData.Amount


            var row = '<tr class="">' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + rowData.TrasactionID + '</td>' +
                '<td>' + rowData.Date + '</td>' +
                '<td>' + rowData.CustomerName + '</td>' +
                '<td >' + rowData.Payment_Type + '</td>' +
                '<td>' + rowData.Description + '</td>' +
                '<td align="center">' + Amount + '</td>' +
                '</tr>';
            tbody.append(row);
            Dtotal = Dtotal + Amount;


        }
        var row = '<tr class="ffooter">' +
            '<td colspan=6> Total </td>' +
            '<td colspan=1>' + Dtotal + '</td>' +
            '</tr>';
        tbody.append(row);
    } else {
        var row = '<tr class="col">' +
            '<td colspan=7> No Data </td>' +
            '</tr>';
        tbody.append(row);
    }
}


function VenPayment(loadData) {
    var tbody = $('#Vpayment tbody');
    tbody.empty();
    var Dtotal = 0
    if (loadData.length > 0) {
        for (var i = 0; i < loadData.length; i++) {
            var rowData = loadData[i];

            var Amount = rowData.Amount


            var row = '<tr class="">' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + rowData.TrasactionID + '</td>' +
                '<td>' + rowData.Date + '</td>' +
                '<td>' + rowData.VendorName + '</td>' +
                '<td >' + rowData.Payment_Type + '</td>' +
                '<td>' + rowData.Description + '</td>' +
                '<td align="center">' + Amount + '</td>' +
                '</tr>';
            tbody.append(row);
            Dtotal = Dtotal + Amount;


        }
        var row = '<tr class="ffooter">' +
            '<td colspan=6> Total </td>' +
            '<td colspan=1>' + Dtotal + '</td>' +
            '</tr>';
        tbody.append(row);
    } else {
        var row = '<tr class="col">' +
            '<td colspan=7> No Data </td>' +
            '</tr>';
        tbody.append(row);
    }
}

function LoanPayment(loadData) {
    var tbody = $('#loan tbody');
    tbody.empty();
    var Drtotal = 0
    var Crtotal = 0
    if (loadData.length > 0) {
        for (var i = 0; i < loadData.length; i++) {
            var rowData = loadData[i];

            var drAmount = rowData.DR_Amount;
            var crAmount = rowData.CR_Amount;

            var row = '<tr class="">' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + rowData.TrasactionID + '</td>' +
                '<td>' + rowData.Date + '</td>' +
                '<td>' + rowData.CustomerName + '</td>' +
                '<td>' + rowData.Description + '</td>' +
                '<td align="center" style="background: #00800047">' + rowData.DR_Amount + '</td>' +
                '<td align="center" style="background: #ebcaca">' + rowData.CR_Amount + '</td>' +
                '<td  align="center">' + rowData.Amount + '</td>' +
                '</tr>';
            tbody.append(row);
            Drtotal = Drtotal + drAmount;
            Crtotal = Crtotal + crAmount;
        }
        var row = '<tr class="ffooter">' +
            '<td colspan=5> Total </td>' +
            '<td colspan=1>' + Drtotal + '</td>' +
            '<td colspan=1>' + Crtotal + '</td>' +
            '<td colspan=1> </td>' +
            '</tr>';
        tbody.append(row);
    } else {
        var row = '<tr class="col">' +
            '<td colspan=8> No Data </td>' +
            '</tr>';
        tbody.append(row);
    }
}


