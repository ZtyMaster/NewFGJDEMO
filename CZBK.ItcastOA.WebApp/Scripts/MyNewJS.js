function SetQZimage() {

    var rows = $('#tt').datagrid('getSelections');
    if (!rows || rows.length == 0) {
        //alert("请选择要修改的商品！");
        $.messager.alert("提醒", "请选择要查看的记录!", "error");
        return;
    }
    if (rows.length > 1) {
        $.messager.alert("提醒", "仅可查看一条信息！", "error");
        return;
    }

    if (rows[0].Images.length <= 0) {
        $('#tt').datagrid('clearChecked');
        $.messager.alert("提醒", "信息没有图片可以预览！", "error");
        return;
    }

    $.post("/Qiuzuinfo/SeeImage", { "id": rows[0].ID }, function (datas) {
        $('#tt').datagrid('clearChecked');
        var data = $.parseJSON(datas);
        if (data.msg == "ok") {
            var t = data.sData;
            t = t.slice(3, t.length);
            var c = t.split("---");
            var width = $(document.body).width() - $(document.body).width() / 2;
            var height = $(document.body).height() - $(document.body).height() / 5;
            var Pageimage = 0, MaxImage = c.length;
            $("#Timage").attr("src", c[Pageimage]);
            $("#Timage").attr("width", width - 30);
            $("#Timage").attr("height", height - 80);
            $("#SeeImageDIV").css("display", "block");
            $('#SeeImageDIV').dialog({
                title: "编辑用户信息",
                width: width,
                height: height,
                collapsible: true,
                resizable: true,
                modal: true,
                buttons: [
                    {
                        text: '上一页',
                        iconCls: 'icon-ok',
                        handler: function () {
                            Pageimage = Pageimage - 1 < 0 ? MaxImage : Pageimage - 1;
                            $("#Timage").attr("src", c[Pageimage]);
                        }
                    },
                    {
                        text: '下一页',
                        iconCls: 'icon-ok',
                        handler: function () {
                            Pageimage = Pageimage + 1 > MaxImage ? 0 : Pageimage + 1;
                            $("#Timage").attr("src", c[Pageimage]);
                        }
                    }, {
                        text: '关闭',
                        handler: function () {
                            $('#SeeImageDIV').dialog('close');
                        }
                    }]
            });
        } else {
            $.messager.alert("提醒", "展示数据错误!!", "error");
        }
    });
}
//查看图片
$(function () {
    try {

        $("#SeeImageDIV").css("display", "none");
        $("#DeditMoneyDIV").css("display", "none");
    }
    catch (e)
    { }
   
   
})
function num(obj) {
    obj.value = obj.value.replace(/[^\d.]/g, ""); //清除"数字"和"."以外的字符
    obj.value = obj.value.replace(/^\./g, ""); //验证第一个字符是数字
    obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个, 清除多余的
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3'); //只能输入两个小数
}
function SeeImage() {
    var rows = $('#tt').datagrid('getSelections');
    if (!rows || rows.length == 0) {
        //alert("请选择要修改的商品！");
        $.messager.alert("提醒", "请选择要查看的记录!", "error");
        return;
    }
    if (rows.length > 1) {
        $.messager.alert("提醒", "仅可查看一条信息！", "error");
        return;
    }
    if (rows[0].Image_str != "有") {
        $('#tt').datagrid('clearChecked');
        $.messager.alert("提醒", "信息没有图片可以预览！", "error");
        return;
    }
    $.post("/HrefInfo/SeeImage", { "id": rows[0].ID }, function (serverData) {
        $('#tt').datagrid('clearChecked');
        var data = $.parseJSON(serverData);
        if (data.msg == "ok") {
            var t = data.serverData;
            var c = t.split("---");
            var width = $(document.body).width() - $(document.body).width() / 2;
            var height = $(document.body).height() - $(document.body).height() / 5;
            var Pageimage = 0, MaxImage = c.length;
            $("#Timage").attr("src", c[Pageimage]);
            $("#Timage").attr("width", width - 30);
            $("#Timage").attr("height", height - 80);
            $("#SeeImageDIV").css("display", "block");
            $('#SeeImageDIV').dialog({
                title: "编辑用户信息",
                width: width,
                height: height,
                collapsible: true,
                resizable: true,
                modal: true,
                buttons: [
                    {
                        text: '上一页',
                        iconCls: 'icon-ok',
                        handler: function () {
                            Pageimage = Pageimage - 1 < 0 ? MaxImage : Pageimage - 1;
                            $("#Timage").attr("src", c[Pageimage]);
                        }
                    },
                    {
                        text: '下一页',
                        iconCls: 'icon-ok',
                        handler: function () {
                            Pageimage = Pageimage + 1 > MaxImage ? 0 : Pageimage + 1;
                            $("#Timage").attr("src", c[Pageimage]);
                        }
                    }, {
                        text: '关闭',
                        handler: function () {
                            $('#SeeImageDIV').dialog('close');
                        }
                    }]
            });
        } else {
            $.messager.alert("提醒", "展示数据错误!!", "error");
        }
    });
}

function SeeMap() {
    var rows = $('#tt').datagrid('getSelections');
    if (rows.length != 1) {
        //alert("请选择要修改的商品！");
        $.messager.alert("提醒", "请选择要分配角色权限的一条记录!", "error");
        return;
    }
    var addres = rows[0].Address;
    $('#tt').datagrid('clearChecked');
    $("#SeeMapFrame").attr("src", "/hrefinfo/GetMap/?Address=" + addres);
    $("#SeeMapFrame").attr("height", "99%");
    $("#SeeMapFrame").attr("width", "100%");
    $("#SeeMap").css("display", "block");
    $('#SeeMap').dialog({
        title: "百度地图",
        width: $(window).width() / 1.5,
        height: $(window).height() / 1.5,
        collapsible: true,
        resizable: true,
        modal: true,
        buttons: [{
            text: '取消',
            handler: function () {
                $('#SeeMap').dialog('close');
            }
        }]
    });
}

//自定义查看图片
function SeeThisImage() {
    
    var rows = $('#tt').datagrid('getSelections');
    if (!rows || rows.length == 0) {
        //alert("请选择要修改的商品！");
        $.messager.alert("提醒", "请选择要查看的记录!", "error");
        return;
    }
    if (rows.length > 1) {
        $.messager.alert("提醒", "仅可查看一条信息！", "error");
        return;
    }
    if (rows[0].Image <= 0) {
        $('#tt').datagrid('clearChecked');
        $.messager.alert("提醒", "信息没有图片可以预览！", "error");
        return;
    }

    $.post("/UserSaveInfo/SeeImage", { "id": rows[0].ID }, function (Sdata) {
        $('#tt').datagrid('clearChecked');
        if (Sdata.msg == "ok") {
            var t = Sdata.sdata;
            var c = t.split(",");
            var RetId = Sdata.retId.split(",");
            var width = $(document.body).width() - $(document.body).width() / 2;
            var height = $(document.body).height() - $(document.body).height() / 5;
            var Pageimage = 0, MaxImage = c.length;

            $("#Timage").attr("src", c[Pageimage]);
            $("#Timage").attr("width", width - 20);
            $("#Timage").attr("height", height - 80);
            $("#SeeImageDIV").css("display", "block");
            $('#SeeImageDIV').dialog({
                title: "查看图片&nbsp&nbsp共" + MaxImage+"张",
                width: width,
                height: height,
                collapsible: true,
                resizable: true,
                modal: true,
                buttons: [ 
                     {
                         text: '删除',
                         iconCls: 'icon-remove',
                         handler: function () {
                             var Did = RetId[Pageimage];
                             $.post("/UserSaveInfo/DelImage", { "id": Did }, function (data) {
                                 if (data.ret == "ok") {
                                     $("#tt").datagrid("reload");
                                     $('#SeeImageDIV').dialog('close');
                                     
                                 } else if (data.ret == "no") {
                                     $.messager.alert("提醒", data.msg, "error");
                                 } else if (data.ret == "noAction") {
                                     $.messager.alert("提醒", "无权删除!!", "error");
                                 } else {
                                     $.messager.alert("提醒", "不可删除!!", "error");
                                 }
                             })
                         }
                     },
                    {
                        text: '上一页',
                        iconCls: 'icon-ok',
                        handler: function () {
                            Pageimage = Pageimage - 1 < 0 ? MaxImage : Pageimage - 1;
                            $("#Timage").attr("src", c[Pageimage]);
                        }
                    },
                    {
                        text: '下一页',
                        iconCls: 'icon-ok',
                        handler: function () {
                            Pageimage = Pageimage + 1 > MaxImage ? 0 : Pageimage + 1;
                            $("#Timage").attr("src", c[Pageimage]);
                        }
                    }, {
                        text: '关闭',
                        handler: function () {
                            $('#SeeImageDIV').dialog('close');
                        }
                    }]
            });
        } else {
            $.messager.alert("提醒", "展示数据错误!!", "error");
        }
    });
}

//修改价格
//查看出租图片
//将序列化成json格式后日期(毫秒数)转成日期格式
function Fromart_24(cellval) {
    if (cellval == null)
    {
        return "";
    }
    var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    var Hh = date.getHours();
    var Mm = date.getMinutes();
    return date.getFullYear() + "-" + month + "-" + currentDate +" "+Hh+":"+(Mm<10?("0"+Mm):Mm);
}

function Fromart_addHour(cellval) {
    var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + (date.getDate()+1) : date.getDate()+1;
    var Hh = date.getHours();
    var Mm = date.getMinutes();
    return date.getFullYear() + "-" + month + "-" + currentDate ;
}



    
