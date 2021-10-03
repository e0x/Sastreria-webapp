// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(document).ready(function () {
    $('#clientTel').autocomplete({
        source: '/Clients/Search',
        minLength: 3,
        select: function (event, ui) {
            $.ajax({
                url: "/Clients/SearchByTel",
                data: { tel: ui.item.value },
                dataType: "json",
                success: function (client) {                                        
                    $('#clientName').val(client[0].name);
                    $('#clientAddress').val(client[0].address);
                    $('#clientId').val(client[0].id);                                        
                },
                error: function ( error) {
                    console.log(error);
                }
            });
        }
    })

    // init date for new order
    InitialDate();

});

function InitialDate() {
    var dateNewFormat, onlyDate, today = new Date();

    dateNewFormat = today.getFullYear() + '-';
    if (today.getMonth().length == 2) {

        dateNewFormat += (today.getMonth() + 1);
    }
    else {
        dateNewFormat += '0' + (today.getMonth() + 1);
    }

    onlyDate = today.getDate();
    if (onlyDate.toString().length == 2) {

        dateNewFormat += "-" + onlyDate;
    }
    else {
        dateNewFormat += '-0' + onlyDate;
    }

    $('#DeliveryDate').val($('#DeliveryDate').val() || dateNewFormat );
}

function RenderActions(RenderActionstring) {
    $("#OpenDialog").load(RenderActionstring);
};

function changeCollapseBtnLabel() {
    $('#collapseTable').on('show.bs.collapse', function () {
        $('#collapse-btn-switch').html('Ocultar pagos')
    });

    $('#collapseTable').on('hidden.bs.collapse', function () {
        $('#collapse-btn-switch').html('Ver pagos')
    });
}