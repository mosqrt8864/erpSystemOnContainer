// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var counter = 1;
function getval(sel,index)
{
    console.log(sel.value);
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        url: '/Inventory/GetPN?id='+sel.value,
        success:function(result){
            $('tr#row'+index).each(function(index, tr) { 
                $(this).find("td > input[type='text']").each (function(i,item) {
                    var propertyname = $(item).attr("name");
                    switch(propertyname){
                        case "Name":
                            $(this).val(result.name);
                            break;
                        case "Spec":
                            $(this).val(result.spec);
                            break;
                        default:
                            break;
                    }
                  });
             });
        },
        error: function(){
            alert('fail');
        }
    })
}
$(function(){
    $('#addItem').click(function (){
        $('<tr id="row'+counter+'">'+
            '<td><select onchange="getval(this,'+counter+');" class="form-select" id="PN" name="PN" type="text" /></td>'+
            '<td><input name="Name" type="text" class="form-control text-box single-line" readonly/></td>'+
            '<td><input name="Spec" type="text" class="form-control text-box single-line" readonly/></td>'+
            '<td><input name="Qty" type="text" class="form-control text-box single-line"/></td>'+
        '</tr>').appendTo("#itemTable");
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: 'GET',
            url: '/Inventory/GetPNList',
            success:function(result){
                $.each(result.items, function (index, value) {
                    if (index==0){
                        $('tr#row'+counter).each(function(index, tr) { 
                            $(this).find("td > input[type='text']").each (function(i,item) {
                                var propertyname = $(item).attr("name");
                                switch(propertyname){
                                    case "Name":
                                        $(this).val(value.name);
                                        break;
                                    case "Spec":
                                        $(this).val(value.spec);
                                        break;
                                    default:
                                        break;
                                }
                              });
                         });
                    }
                    $("[name='PN']").append($('<option/>', { 
                        value: value.id,
                        text : value.id 
                    }));
                    counter++;
                });
            },
            error: function(){
                alert('fail');
            }
        })
        return false;
    });
    $('#createBtn').click(function(){
        var id = $('#Id').val();
        var des =$('#Description').val();
        var items =[];
        $('#itemTable > tbody  > tr').each(function(index, tr) { 
            var properties = {PRID:id};
            $(this).find("td > input[type='text']").each (function(i,item) {
                var propertyname = $(item).attr("name");
                switch(propertyname){
                    case "PN":
                        properties.PNId = $(item).val();
                    case "Name":
                        properties.Name = $(item).val();
                    case "Spec":
                        properties.Spec = $(item).val();
                    case "Qty":
                        properties.Qty = $(item).val();
                }
              });
              items.push(properties);     
         });
        var purchaseRequest ={
            Id :id,
            Description:des,
            PurchaseRequestItems:items
        };
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            url: '/PurchaseRequest/CreatePurchaseRequest',
            data :JSON.stringify(purchaseRequest),
            success:function(result){
                alert("新增成功");
                window.location.href = ".";
            },
            error: function(err){
                alert(err.responseText);
            }
        })
    });
});
