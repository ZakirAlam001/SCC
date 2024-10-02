
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
    debugger;
    var DepositType = $('#DepositType :selected').val();

    var description = $('.dddescription').val();
    var Amount = parseFloat($('.ddAmount').val());
    var Date = $('.dddate').val();
    //alert(Total_Amount);
    // Form Submit  Ajax  Start   Createe       Recv_Date: Date,
    $.post('/DailyDeposit/Create', { DepositType: DepositType, description: description, Amount: Amount, Date: Date })
        .done(function (data) {
            debugger
            GetData(1);

        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red')
            alert("Invalid Salections");
            //$('#txtAdvance').get(0).focus();
            //   $("#txtAdvance").focus(true);
        })
    //.always(function () { console.log("always") });
}




function LoanPayment() {
    debugger;
    var LoanPersonID = parseInt($('.LPLoanPersonID :selected').val());
    var Payment_Type = $('.LPPayment_Type').val();
    var Description = $('.LPdescription').val();
    var Amount = parseFloat($('.LPAmount').val());
    var ApplyDate = $('.LPdate').val();
    //alert(Total_Amount);
    // Form Submit  Ajax  Start   Createe       Recv_Date: Date,
    $.post('/Loan_Transaction/Create', { LoanPersonID: LoanPersonID, Payment_Type: Payment_Type, Description: Description, Amount: Amount, ApplyDate: ApplyDate })
        .done(function (data) {
            //alert();
            //waitingDialog.show('Data Saved', { dialogSize: 'sm', progressType: 'success' }) ;
            //waitingDialog.hide();
            //setTimeout(function () {
            MASTER_RECORD_ID = data;

            $('#printConfirmationModal').modal('show');
            $('html, body').animate({ scrollTop: 0 }, 500);
            //}, 2000)

        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red')
            alert("Invalid Salections");
            //$('#txtAdvance').get(0).focus();
            //   $("#txtAdvance").focus(true);
        })
    //.always(function () { console.log("always") });
}



var MASTER_ID;
function VendorSave() {
    debugger
    // Master Record***
    //  VendorId = $('#VendorId').val()
    var master = [{
        "VendorId": $('.OVendorID').val(),
        "ItemQuantity": $('.OVWeight').val(),
        "Discount": 0,
        "Date": $('OVdate').val(),
        "TotalAmount": $('.OVTotalAmount').val(),
    }];

    //Detail Records***
    var details = [];

    // $('#table tbody tr').each(function (i, e) {
    //     var $row = $(e);
    // alert($row.find('.titem').text());
    var d = {

        "PurchaseType": 1,
        "SaleTypeID": $('.OVItemType').val(),
        "Quantity": $('.OVWeight').val(),
        "Unit_Amount": 0,
        "Sale_Amount": 0,
        "Description": $('.OVDescription').val(),
        "Total_Amount": $('.OVTotalAmount').val(),

    };
    details.push(d);
    //   });


    //Master and details array****
    master = JSON.stringify(master);
    details = JSON.stringify(details);
    //Send Data To Server Action***
    $.post('/Stock/PurchaseSave', { Details: details, Master: master })
        .done(function (data) {
            //MASTER_ID = data;
            //$('.items').remove();
            //sessionStorage.setItem('tmsg', 'Data Save Scussfully');
            alert("Data Saved!")
            location.reload();
            printYes();


        })
        .fail(function (data) {
            //  alert("Fail");
            sessionStorage.setItem('tmsg', 'Save Failed');
            alert("Data Saved failed!")

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
    debugger
    var EmpID = $('.OEmpID :selected').val();
    var Total_Amount = $('.OTotalAmount').val();
    var Description = $('.ODescription').val();
    var ApplyDate = $('.Odate').val();
    var Discount = 0;
    var Discount_Amount = 0;
    var invoiceid = 0;

    //alert(EmpID );
    //alert(Total_Amount);
    //alert(Description);
    //alert(ApplyDate);
    //Get Details Record Json List
    //$('.singleProduct').each(function (i, elem) {
    //    grossAmount += parseInt($(elem).find('.txtPayableAmount').val());
    //});
    // List of Item Json
    //Git checked
    var products = [];
    //   $('.singleProduct').each(function (i, elem) {
    var item = {
        "Stock_ID": $('.OItemType :selected').val(),
        "Stock_Counts": $('.OWeight').val(),
        "Item_Price": $('.ORate').val(),
        "Net_Price": $('.OTotalAmount').val(),
        "Discount": 0,
        "Discount_Amount": 0
    }
    products.push(item);
    //  });
    products = JSON.stringify(products);
    // console.log(products);
    // Form Submit  Ajax  Start   Createe                               List[]
    $.post('/SaleMaster/Createe', { Products: products, EmpID: EmpID, Total_Amount: Total_Amount, Description: Description, ApplyDate: ApplyDate, Discount: Discount, Discount_Amount: Discount_Amount, invoiceid: invoiceid })
        .done(function (data) {
            //alert();
            //waitingDialog.show('Data Saved', { dialogSize: 'sm', progressType: 'success' }) ;
            //waitingDialog.hide();
            //setTimeout(function () {
            MASTER_RECORD_ID = data;

            $('#printConfirmationModal').modal('show');
            $('html, body').animate({ scrollTop: 0 }, 500);
            //}, 2000)

        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red')
            alert("Invalid Salections");
            //$('#txtAdvance').get(0).focus();
            //   $("#txtAdvance").focus(true);
        })
    //.always(function () { console.log("always") });
}
// Form Submit  Ajax  End



function CustomerPayment() {
    alert();
    var EmpID = parseInt($('#EmpID :selected').val());
    var PayType = $('#Payment_Type').val();
    var Description = $('#Customerdescription').val();
    var Amount = parseFloat($('#Amount').val());
    var Date = $('#Recv_Date').val();
    //alert(Total_Amount);
    // Form Submit  Ajax  Start   Createe       Recv_Date: Date,
    $.post('/Payments/Create', { EmpID: EmpID, Payment_Type: PayType, Description: Description, Amount: Amount, Recv_Date: Date, invostatus: "No" })
        .done(function (data) {
            //alert();
            //waitingDialog.show('Data Saved', { dialogSize: 'sm', progressType: 'success' }) ;
            //waitingDialog.hide();
            //setTimeout(function () {
            MASTER_RECORD_ID = data;

            $('#printConfirmationModal').modal('show');
            $('html, body').animate({ scrollTop: 0 }, 500);
            //}, 2000)

        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red')
            alert("Invalid Salections");
            //$('#txtAdvance').get(0).focus();
            //   $("#txtAdvance").focus(true);
        })
    //.always(function () { console.log("always") });
}


function VendorPayment() {
    alert();
    var VendorId = parseInt($('.VPVendorID :selected').val());
    var PayType = $('.VPPType').val();
    var Description = $('.VPdescription').val();
    var Amount = parseFloat($('.VPamount').val());
    var Date = $('.VPdate').val();
    //alert(Total_Amount);
    // Form Submit  Ajax  Start   Createe       Recv_Date: Date,
    $.post('/Vendor_Payment/Create', { VendorId: VendorId, Payment_Type: PayType, Description: Description, Amount: Amount, Recv_Date: Date, invostatus: "No" })
        .done(function (data) {
            //alert();
            //waitingDialog.show('Data Saved', { dialogSize: 'sm', progressType: 'success' }) ;
            //waitingDialog.hide();
            //setTimeout(function () {
            MASTER_RECORD_ID = data;

            $('#printConfirmationModal').modal('show');
            $('html, body').animate({ scrollTop: 0 }, 500);
            //}, 2000)

        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red')
            alert("Invalid Salections");
            //$('#txtAdvance').get(0).focus();
            //   $("#txtAdvance").focus(true);
        })
    //.always(function () { console.log("always") });
}


function ExpenseAdd() {
    alert();
    // var VendorId = parseInt($('.VPVendorID :selected').val());
    var Expensetype = $('.Exexpensetype').val();
    var Comments = $('.Exdescription').val();
    var TotalAmount = parseFloat($('.ExAmount').val());
    var ExpDate = $('.Exdate').val();
    //alert(Total_Amount);
    // Form Submit  Ajax  Start   Createe       Recv_Date: Date,
    $.post('/MasterExpence/Create', { Expensetype: Expensetype, Comments: Comments, TotalAmount: TotalAmount, ExpDate: ExpDate, invostatus: "No" })
        .done(function (data) {
            //alert();
            //waitingDialog.show('Data Saved', { dialogSize: 'sm', progressType: 'success' }) ;
            //waitingDialog.hide();
            //setTimeout(function () {
            MASTER_RECORD_ID = data;

            $('#printConfirmationModal').modal('show');
            $('html, body').animate({ scrollTop: 0 }, 500);
            //}, 2000)

        })
        .fail(function (data) {
            $('.btn-base').css('background', 'red')
            alert("Invalid Salections");
            //$('#txtAdvance').get(0).focus();
            //   $("#txtAdvance").focus(true);
        })
    //.always(function () { console.log("always") });
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
