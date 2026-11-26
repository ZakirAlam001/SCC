
const d = new Date();
s = d.getDate() + '-' + d.toString().substr(4, 3) + '-' + d.getFullYear();
$('#activedate').val(s);


function GetAllData(activedate,Mode) {
    const d = new Date(activedate);
    if (Mode == 'F') {
        d.setDate(d.getDate() + 1);
    } else if(Mode == 'B') {
        d.setDate(d.getDate() - 1);
    }
    s = d.getDate() + '-' + d.toString().substr(4, 3) + '-' + d.getFullYear();
    $('#activedate').val(s);
    GetData(1); GetData(2); GetData(3); GetData(4); GetData(5); GetData(6); GetData(7);
}

function GetData(Case) {
    //   var selectedIndent = $('#indentNo').val(); // Get Selected Indent Value
    debugger
    var date = $('.activedate').val();
    $.post('/GetData/GetDataList', { Case: Case, date1: date })
        .done(function (response) {
            //  alert(response);
            console.log("Server Response:", response);
            debugger
            // Parse the response as an array of objects
            //var loadData = JSON.parse(response);
            var loadData = response
            if (Case == 1) {
                DepositGrid(loadData);
            } else if (Case == 2) {
                SaleGrid(loadData)
            }
            else if (Case == 3) {
                PurchaseGrid(loadData)
            }
            else if (Case == 4) {
                ExpenseGrid(loadData)
            }

            else if (Case == 5) {
                CusPayment(loadData)
            }
            else if (Case == 6) {
                VenPayment(loadData)
            }
            else if (Case == 7) {
                LoanPayment(loadData)
            }

        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red')
            alert("Invalid Salections");
            //$('#txtAdvance').get(0).focus();
            //   $("#txtAdvance").focus(true);
        })
}

//// For Date Save

function DailyDeposit() {
    var DepositType = $('#DepositType :selected').val() || $('#DepositType').val();
    var description = $('.dddescription').val();
    var Amount = parseFloat($('.ddAmount').val());
    var Date = $('.dddate').val();

    if (!DepositType || DepositType === "Select Deposit Type" || DepositType === "") {
        alert("Please select Deposit Type!");
        $('#DepositType').focus();
        return false;
    }
    if (isNaN(Amount) || Amount <= 0) {
        alert("Please enter a valid Amount!");
        $('.ddAmount').focus();
        return false;
    }
    if (!Date) {
        alert("Please select Date!");
        $('.dddate').focus();
        return false;
    }
    if (!description || description.trim() === "") {
        alert("Please enter Description!");
        $('.dddescription').focus();
        return false;
    }

    $.post('/DailyDeposit/Create', { DepositType: DepositType, description: description, Amount: Amount, Date: Date })
        .done(function (data) {
            alert("Daily Deposit Saved Successfully!");
            GetData(1);
            $('#Searchmodel').modal('hide');
        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red');
            alert("Invalid Selection or Error saving deposit");
        });
}




function LoanPaymentCrud() {
    var LoanPersonID = parseInt($('.LPLoanPersonID :selected').val() || $('.LPLoanPersonID').val());
    var Payment_Type = $('.LPPayment_Type').val();
    var Description = $('.LPdescription').val();
    var Amount = parseFloat($('.LPAmount').val());
    var ApplyDate = $('.LPdate').val();

    if (isNaN(LoanPersonID) || LoanPersonID <= 0) {
        alert("Please select Person!");
        $('.LPLoanPersonID').focus();
        return false;
    }
    if (!Payment_Type) {
        alert("Please select Payment Type!");
        $('.LPPayment_Type').focus();
        return false;
    }
    if (isNaN(Amount) || Amount <= 0) {
        alert("Please enter a valid Amount!");
        $('.LPAmount').focus();
        return false;
    }
    if (!ApplyDate) {
        alert("Please select Date!");
        $('.LPdate').focus();
        return false;
    }
    if (!Description || Description.trim() === "") {
        alert("Please enter Description!");
        $('.LPdescription').focus();
        return false;
    }

    $.post('/Loan_Transaction/Create', { LoanPersonID: LoanPersonID, Payment_Type: Payment_Type, Description: Description, Amount: Amount, ApplyDate: ApplyDate })
        .done(function (data) {
            alert("Loan Payment Saved Successfully!");
            MASTER_RECORD_ID = data;
            $('#Searchmodel').modal('hide');
            $('#printConfirmationModal').modal('show');
            $('html, body').animate({ scrollTop: 0 }, 500);
            GetData(7);
        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red');
            alert("Invalid Selection or Error saving loan transaction");
        });
}



var MASTER_ID;
function VendorSave() {
    var vendorId = $('.OVendorID').val();
    var itemType = $('.OVItemType').val();
    var rate = parseFloat($('.OVRate').val());
    var weight = parseFloat($('.OVWeight').val());
    var totalAmount = parseFloat($('.OVTotalAmount').val());
    var date = $('.OVdate').val();
    var description = $('.OVDescription').val();

    if (!vendorId) {
        alert("Please select Vendor!");
        $('.OVendorID').focus();
        return false;
    }
    if (!itemType) {
        alert("Please select Order Type / Item!");
        $('.OVItemType').focus();
        return false;
    }
    if (isNaN(rate) || rate <= 0) {
        alert("Please enter a valid Rate!");
        $('.OVRate').focus();
        return false;
    }
    if (isNaN(weight) || weight <= 0) {
        alert("Please enter a valid Weight!");
        $('.OVWeight').focus();
        return false;
    }
    if (!date) {
        alert("Please select Date!");
        $('.OVdate').focus();
        return false;
    }
    if (!description || description.trim() === "") {
        alert("Please enter Description!");
        $('.OVDescription').focus();
        return false;
    }

    var master = [{
        "VendorId": vendorId,
        "ItemQuantity": weight,
        "Discount": 0,
        "Date": date,
        "TotalAmount": totalAmount
    }];

    var details = [{
        "PurchaseType": 1,
        "SaleTypeID": itemType,
        "Quantity": weight,
        "Unit_Amount": 0,
        "Sale_Amount": 0,
        "Description": description,
        "Total_Amount": totalAmount
    }];

    master = JSON.stringify(master);
    details = JSON.stringify(details);

    $.post('/Stock/PurchaseSave', { Details: details, Master: master })
        .done(function (data) {
            alert("Daily Vendor Order Saved Successfully!");
            $('#Searchmodel').modal('hide');
            location.reload();
            printYes();
        })
        .fail(function (data) {
            sessionStorage.setItem('tmsg', 'Save Failed');
            alert("Data Save failed!");
            console.log(data);
        })
}



//function ctotal() {
//    //alert();
//    var a = parseFloat($('.crate').val()) || 0;
//    var b = parseFloat($('.cweight').val()) || 0;
//    total1 = a * b;
//    $('.ctotal').val((total1).toFixed(2))
//}

function Total(id) {
    debugger
    var TotalAmount = 0;
    if (id == 1) {
        TotalAmount = parseFloat(parseFloat($('.ORate').val() || 0) * parseFloat($('.OWeight').val() || 0)).toFixed(2);
        $('.OTotalAmount').val(TotalAmount);
    }
    if (id == 2) {
        TotalAmount = parseFloat(parseFloat($('.OVRate').val() || 0) * parseFloat($('.OVWeight').val() || 0)).toFixed(2);
        $('.OVTotalAmount').val(TotalAmount);
    }
}


var MASTER_RECORD_ID;
function SaleOrder() {
    var EmpID = $('.OEmpID :selected').val() || $('.OEmpID').val();
    var stockId = $('.OItemType :selected').val() || $('.OItemType').val();
    var rate = parseFloat($('.ORate').val());
    var weight = parseFloat($('.OWeight').val());
    var Total_Amount = $('.OTotalAmount').val();
    var Description = $('.ODescription').val();
    var ApplyDate = $('.Odate').val();
    var Discount = 0;
    var Discount_Amount = 0;
    var invoiceid = 0;

    if (!EmpID) {
        alert("Please select Customer!");
        $('.OEmpID').focus();
        return false;
    }
    if (!stockId) {
        alert("Please select Order Type / Item!");
        $('.OItemType').focus();
        return false;
    }
    if (isNaN(rate) || rate <= 0) {
        alert("Please enter a valid Rate!");
        $('.ORate').focus();
        return false;
    }
    if (isNaN(weight) || weight <= 0) {
        alert("Please enter a valid Weight!");
        $('.OWeight').focus();
        return false;
    }
    if (!ApplyDate) {
        alert("Please select Date!");
        $('.Odate').focus();
        return false;
    }
    if (!Description || Description.trim() === "") {
        alert("Please enter Description!");
        $('.ODescription').focus();
        return false;
    }

    var products = [{
        "Stock_ID": stockId,
        "Stock_Counts": weight,
        "Item_Price": rate,
        "Net_Price": Total_Amount,
        "Discount": 0,
        "Discount_Amount": 0
    }];
    products = JSON.stringify(products);

    $.post('/SaleMaster/Createe', { Products: products, EmpID: EmpID, Total_Amount: Total_Amount, Description: Description, ApplyDate: ApplyDate, Discount: Discount, Discount_Amount: Discount_Amount, invoiceid: invoiceid })
        .done(function (data) {
            alert("Daily Customer Order Saved Successfully!");
            MASTER_RECORD_ID = data;
            $('#Searchmodel').modal('hide');
            $('#printConfirmationModal').modal('show');
            $('html, body').animate({ scrollTop: 0 }, 500);
            GetData(2);
        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red');
            alert("Invalid Selection or Error saving sale order");
        });
}
// Form Submit  Ajax  End



function CustomerPayment() {
    var EmpID = parseInt($('#EmpID :selected').val() || $('#EmpID').val());
    var PayType = $('#Payment_Type').val();
    var Description = $('#Customerdescription').val();
    var Amount = parseFloat($('#Amount').val());
    var Date = $('#Recv_Date').val();

    if (isNaN(EmpID) || EmpID <= 0) {
        alert("Please select Customer!");
        $('#EmpID').focus();
        return false;
    }
    if (!PayType || PayType === "Select") {
        alert("Please select Payment Type!");
        $('#Payment_Type').focus();
        return false;
    }
    if (isNaN(Amount) || Amount <= 0) {
        alert("Please enter a valid Amount!");
        $('#Amount').focus();
        return false;
    }
    if (!Date) {
        alert("Please select Date!");
        $('#Recv_Date').focus();
        return false;
    }
    if (!Description || Description.trim() === "") {
        alert("Please enter Description!");
        $('#Customerdescription').focus();
        return false;
    }

    $.post('/Payments/Create', { EmpID: EmpID, Payment_Type: PayType, Description: Description, Amount: Amount, Recv_Date: Date, invostatus: "No" })
        .done(function (data) {
            alert("Customer Payment Saved Successfully!");
            MASTER_RECORD_ID = data;
            $('#Searchmodel').modal('hide');
            $('#printConfirmationModal').modal('show');
            $('html, body').animate({ scrollTop: 0 }, 500);
            GetData(5);
        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red');
            alert("Invalid Selection or Error saving customer payment");
        });
}


function VendorPayment() {
    var VendorId = parseInt($('.VPVendorID :selected').val() || $('.VPVendorID').val());
    var PayType = $('.VPPType').val();
    var Description = $('.VPdescription').val();
    var Amount = parseFloat($('.VPamount').val());
    var Date = $('.VPdate').val();

    if (isNaN(VendorId) || VendorId <= 0) {
        alert("Please select Vendor!");
        $('.VPVendorID').focus();
        return false;
    }
    if (!PayType || PayType === "Select") {
        alert("Please select Payment Type!");
        $('.VPPType').focus();
        return false;
    }
    if (isNaN(Amount) || Amount <= 0) {
        alert("Please enter a valid Amount!");
        $('.VPamount').focus();
        return false;
    }
    if (!Date) {
        alert("Please select Date!");
        $('.VPdate').focus();
        return false;
    }
    if (!Description || Description.trim() === "") {
        alert("Please enter Description!");
        $('.VPdescription').focus();
        return false;
    }

    $.post('/Vendor_Payment/Create', { VendorId: VendorId, Payment_Type: PayType, Description: Description, Amount: Amount, Recv_Date: Date, invostatus: "No" })
        .done(function (data) {
            alert("Vendor Payment Saved Successfully!");
            MASTER_RECORD_ID = data;
            $('#Searchmodel').modal('hide');
            $('#printConfirmationModal').modal('show');
            $('html, body').animate({ scrollTop: 0 }, 500);
            GetData(6);
        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red');
            alert("Invalid Selection or Error saving vendor payment");
        });
}


function ExpenseAdd() {
    var Expensetype = $('.Exexpensetype').val();
    var Comments = $('.Exdescription').val();
    var TotalAmount = parseFloat($('.ExAmount').val());
    var ExpDate = $('.Exdate').val();

    if (!Expensetype) {
        alert("Please select Expense Type!");
        $('.Exexpensetype').focus();
        return false;
    }
    if (isNaN(TotalAmount) || TotalAmount <= 0) {
        alert("Please enter a valid Amount!");
        $('.ExAmount').focus();
        return false;
    }
    if (!ExpDate) {
        alert("Please select Date!");
        $('.Exdate').focus();
        return false;
    }
    if (!Comments || Comments.trim() === "") {
        alert("Please enter Description / Comments!");
        $('.Exdescription').focus();
        return false;
    }

    $.post('/MasterExpence/Create', { Expensetype: Expensetype, Comments: Comments, TotalAmount: TotalAmount, ExpDate: ExpDate, invostatus: "No" })
        .done(function (data) {
            alert("Daily Expense Saved Successfully!");
            MASTER_RECORD_ID = data;
            $('#Searchmodel').modal('hide');
            $('#printConfirmationModal').modal('show');
            $('html, body').animate({ scrollTop: 0 }, 500);
            GetData(4);
        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red');
            alert("Invalid Selection or Error saving expense");
        });
}

// Form Submit  Ajax  End

$('table').dataTable({
    "bProcessing": false,
    "sAutoWidth": false,
    "bDestroy": true,
    "sPaginationType": "bootstrap", // full_numbers
    "iDisplayStart ": 10,
    "bSort": false,
    "iDisplayLength": 10,
    "bPaginate": false, //hide pagination
    "bFilter": false, //hide Search bar
    "bInfo": false, // hide showing entries
});
