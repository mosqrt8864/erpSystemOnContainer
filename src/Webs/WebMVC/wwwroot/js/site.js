// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var counter = 1;
function getval(sel,index)
{
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        type: 'GET',
        url: '/Inventory/GetPN?id='+sel.value,
        success:function(result){
            $('tr#row'+index).each(function(index, tr) { 
                $(this).find("td > input[type='text']").each (function(i,item) {
                    var propertyname = $(item).attr("name");
                    switch(propertyname){
                        case "item.Name":
                            $(this).val(result.name);
                            break;
                        case "item.Spec":
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
function remove(sel,index){
    if (index !=0)
    {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: 'DELETE',
            url: '/PurchaseRequest/DeleteItem/'+index,
            success:function(result){
                alert(result);
            },
            error: function(err){
                alert('fail');
            }
        })
    }
    $(sel).closest("tr").remove();
}
$(function(){
    $('#addItem').click(function (){
        $('<tr id="row'+counter+'">'+
            '<td><button onclick="remove(this,0)" tpye="button" class="btn btn-danger">X</button></td>'+
            '<td><select name="item.PN"  onchange="getval(this,'+counter+');" class="form-select" id="PN'+counter+'" type="text" /></td>'+
            '<td><input name="item.Name" type="text" class="form-control text-box single-line" readonly/></td>'+
            '<td><input name="item.Spec" type="text" class="form-control text-box single-line" readonly/></td>'+
            '<td><input name="item.Qty" type="text" class="form-control text-box single-line"/></td>'+
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
                                    case "item.Name":
                                        $(this).val(value.name);
                                        break;
                                    case "item.Spec":
                                        $(this).val(value.spec);
                                        break;
                                    default:
                                        break;
                                }
                              });
                         });
                    }
                    $('#PN'+counter).append($('<option/>', { 
                        value: value.id,
                        text : value.id 
                    }));
                });
                counter++;
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
            var properties = {PRId:id};
            $(this).find("td > input[type='text']").each (function(i,item) {
                var propertyname = $(item).attr("name");
                switch(propertyname){
                    case "item.Name":
                        properties.Name = $(item).val();
                        break;
                    case "item.Spec":
                        properties.Spec = $(item).val();
                        break;
                    case "item.Qty":
                        properties.Qty = $(item).val();
                        break;
                }
              });
            $(this).find("td > select[type='text']").each (function(i,item) {
                var propertyname = $(item).attr("name");
                switch(propertyname){
                    case "item.PN":
                        properties.PNId = $(item).val();
                        break;
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
    $('select[name="selector"]').change(function(){
        var select = $(this);
        var id = this.value;
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: 'GET',
            url: '/Inventory/GetPN?id='+id,
            success:function(result){
                select.closest('tr').each(function(index, tr) { 
                    $(this).find("td > input[type='text']").each (function(i,item) {
                        var propertyname = $(item).attr("name");
                        switch(propertyname){
                            case "item.Name":
                                $(this).val(result.name);
                                break;
                            case "item.Spec":
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
    });
    $('#updateBtn').click(function(){
        var id = $('#Id').val();
        var des =$('#Description').val();
        var items =[];
        $('#itemTable > tbody  > tr').each(function(index, tr) { 
            var properties = {PRId:id};
            $(this).find("td > input[type='hidden']").each (function(i,item) {
                var propertyname = $(item).attr("name");
                switch(propertyname){
                    case "item.Id":
                        properties.Id = $(item).val();
                        break;
                    case "item.PRId":
                        properties.PRId = $(item).val();
                        break;
                }
              });
            $(this).find("td > input[type='text']").each (function(i,item) {
                var propertyname = $(item).attr("name");
                switch(propertyname){
                    case "item.Name":
                        properties.Name = $(item).val();
                        break;
                    case "item.Spec":
                        properties.Spec = $(item).val();
                        break;
                    case "item.Qty":
                        properties.Qty = $(item).val();
                        break;
                }
              });
            $(this).find("td > select[type='text']").each (function(i,item) {
                var propertyname = $(item).attr("name");
                switch(propertyname){
                    case "item.PN":
                        properties.PNId = $(item).val();
                        break;
                }
            });
              items.push(properties);     
         });
        var purchaseRequest ={
            Id :id,
            Description:des,
            PurchaseRequestItems:items
        };
        console.log(purchaseRequest);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            url: '/PurchaseRequest/Update',
            data :JSON.stringify(purchaseRequest),
            success:function(result){
                alert("修改成功");
                location.reload();
            },
            error: function(err){
                alert(err.responseText);
            }
        })
    });
});
