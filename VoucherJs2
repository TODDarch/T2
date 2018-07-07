function onVbACIDfocus(row) {
    //alert('onVbACIDfocus');
    ////FUTURE WORK: onVbRowFocus(){}//
    ////console.log($('#data-house #data-house-acbj #').find('row-0]').length);
    //
    ////if ($('#data-house').find('div[name=row-0]').length == 0) {
    ////    var dataHtml = $('div[name=row-0]').html();
    ////    $('#data-house').append('<div>' + '</div>')
    ////}
    //
    ////CHECK DATA HOUSE BJ DATA EXIST NOT//
    $('#data-house input[name=currRowId]').val(row);
    var dataExist = $('#data-house #data-house-acbj').find('#row-' + row.toString()).length;
    if (dataExist == 1) {
        var data = $('#row-' + row).html();

        //for (var i = 0; i < dataJ.length; i++) {
        //    alert(dataJ[i].name);
        //    alert(dataJ[i].old);
        //}
        ////$('#ac8032-bj #id row-0]')

        //WORK TEST!!!//
        //$('div#ac8032-bj div[name=row-' + row + '] input.field-pjid').val(data);
        //$('div#ac8032-bj div[name=row-' + 0 + '] input.field-pjid').val(data);

        //FILL IN DATA//

        //const re = /\[/gm;
        //const re2 = /\]/gm;
        //const re3 = /\./gm;
        //data = data.replace(re, "");
        //data = data.replace(re2, "");
        //data = data.replace(re3, "");


        //CLEAN//
        $('#ac8032-bj div[name=body] input').val('');
        var obj = JSON.parse(data);
        ////console.log('total rowS:' + row);
        //$.each(obj, function (key, value) {
        //    //alert(key + ": " + value);
        //    //$('#ac8032-bj input.field-pjid').val(obj[key].value);
        //    //
        //    //
        //    var fieldname = key;
        //    if (key != "rowId") {
        //        $('#ac8032-bj div[name=body] input[name=' + fieldname + ']').val(value);
        //    }
        //    //$('#ac8032-bj div[name=body] div[name=row-' + row + '] input.field-pjid').val(obj[field_pjid.toString()]);
        //    //$('#ac8032-bj div[name=body] div[name=row-' + row + '] input.field-pjsq').val(obj[field_pjsq.toString()]);
        //    //$('#ac8032-bj div[name=body] div[name=row-' + row + '] input.field-damt').val(obj[field_damt.toString()]);
        //    //$('#ac8032-bj div[name=body] div[name=row-' + row + '] input.field-camt').val(obj[field_camt.toString()]);
        //    //$('#ac8032-bj div[name=body] div[name=row-' + row + '] input.field-ivam').val(obj[field_ivam.toString()]);
        //    //$('#ac8032-bj div[name=body] div[name=row-' + row + '] input.field-ivid').val(obj[field_ivid.toString()]);
        //});
        var arr = $.map(obj, function (el) { return el; })
        var recRowNum = 0;
        for (var i = 1; i <= arr.length; i++){
            ////var fieldname = arr[i].nane
            //var field_pjid = "ACBJList[" + row + "].PJID";
            //var field_pjsq = "ACBJList[" + row + "].PJSQ";
            //var field_damt = "ACBJList[" + row + "].DAMT";
            //var field_camt = "ACBJList[" + row + "].CAMT";
            //var field_ivam = "ACBJList[" + row + "].IVAM";
            //var field_ivid = "ACBJList[" + row + "].IVID";
            if (i % 6 == 1) {
                $('#ac8032-bj div[name=body] div[name=row-' + (recRowNum) + '] input.field-pjid').val(arr[i]);
                //var value = $('#data-house-acbj input[name=ACBJListJson]').val();
                //value += arr[i].toString();
                //$('#data-house-acbj input[name=ACBJListJson]').val(value);
            }
            if (i % 6 == 2) 
                $('#ac8032-bj div[name=body] div[name=row-' + (recRowNum) + '] input.field-pjsq').val(arr[i]);
            if (i % 6 == 3) 
                $('#ac8032-bj div[name=body] div[name=row-' + (recRowNum) + '] input.field-damt').val(arr[i]);
            if (i % 6 == 4) 
                $('#ac8032-bj div[name=body] div[name=row-' + (recRowNum) + '] input.field-camt').val(arr[i]);
            if (i % 6 == 5) 
                $('#ac8032-bj div[name=body] div[name=row-' + (recRowNum) + '] input.field-ivam').val(arr[i]);
            if (i % 6 == 0) {
                $('#ac8032-bj div[name=body] div[name=row-' + (recRowNum) + '] input.field-ivid').val(arr[i]);
                recRowNum = recRowNum + 1;
                //alert('next Row!!!');
            }
            //$('ac8032-bj-body input.field-pjsq').val(obj[ACBJList + i + PJSQ]);
            //$('ac8032-bj-body input.field-damt').val(obj[ACBJList + i + DAMT]);
        }
    }
    else {
        $('#ac8032-bj div[name=body] input').val('');
        //var row0 = $('#ac8032-bj div[name=body] div[name=row-0]').html();
        //$('#ac8032-bj div[name=body]').html('');
        //$('#ac8032-bj div[name=body]').append('<div name=row-0>'+row0+'</div>');
    }
}
function onfoACBDrow(e) {
    //alert($(this).find('input[name=rowId]').val());
    //alert('RR');
    //alert(e.find('input[name=rowId]').val());
    //alert(e.name);

    //$('#data-house #acbj').

    //function() {
    //alert($(this).find('input[name=rowId]').val());
    //}
    ////var rownum = $(this).children('div input').find('input[name=rowId]').val();
    ////alert(rownum);
    ////
    ////$('#dataId').val(rownum);
    //////var bjData = $('#ac8032-bj div[name=body] div[name*=row]').
    ////function() {
    //    //var rowNum = this.parent().parent().find('input[name=rowId]').val();
    //    //alert(rowNum);
    ////}
}
function addRowACBD() {
    //VERSION 2//
    var matchNumArr = $('div#ac8032-vb div[name*=row-]').last().attr('name').match(/\d/gm);
    let rowNumber = parseInt(matchNumArr[0]);
    let rowHtml = $('div#ac8032-vb div[name=body] div[name=row-0]').html();

    //FIND LAST ROW#//
    let lastRow = $('div#ac8032-vb div[name=body] div[name*=row]').last();
    let newRowNumber = parseInt(rowNumber) + parseInt(1);
    //lastRow.find('input[name=rowId]').val(newRowNumber);

    //APPEND AND REPLACE NUM//
    //WORK!! THIS LINE...//$('div#ac8032-vb div[name=body]').append('<div name="row-' + newRowNumber + '">' + rowHtml + '</div>');
    let newDivRowHtml = ('<div name="row-' + newRowNumber + '">' + rowHtml + '</div>');
    const re = /\[\d+\]/gm;
    newDivRowHtml = newDivRowHtml.replace(re, '[' + newRowNumber + ']');

    //REPLACE FUNCTION ARGUMENT(S)//
    newDivRowHtml = newDivRowHtml.replace('vbACIDchange(0', 'vbACIDchange(' + newRowNumber.toString()) ;
    newDivRowHtml = newDivRowHtml.replace('vbSBIDchange(0', 'vbSBIDchange(' + newRowNumber.toString()) ;
    newDivRowHtml = newDivRowHtml.replace('onVbACIDfocus(0', 'onVbACIDfocus(' + newRowNumber.toString());
    $('div#ac8032-vb div[name=body]').append(newDivRowHtml);

    //CHANGE rowId and "SQNO"//
    $('div#ac8032-vb div[name=body] div[name=row-' + newRowNumber + '] input[name=rowId]').val(newRowNumber);
    //$('div#ac8032-vb div[name=body] div[name=row-' + newRowNumber + '] input.field-sqno').val(newRowNumber);

    //KEEP DATA TO WAREHOUSE//
    //$('div#data-warehouse div#acbj').append('')
    //get current num//
    //$('div#ac8032-vb div[name=body] input[name=rowId]').length
    //$('#ac8032-vb div[name=body] div[name*=row]').focusout(function (this) {
    //    alert(this.value);
    //    $('div#data-house input').val(this.value);
    //});
    //onfocusout = "onfoACBDrow()"
};
function addRowACBJ() {
    var matchNumArr = $('div#ac8032-bj div[name*=row-]').last().attr('name').match(/\d/gm);
    let rowNumber = parseInt(matchNumArr[0]);
    let rowHtml = $('div#ac8032-bj div[name=body] div[name=row-0]').html();
    let lastRow = $('div#ac8032-bj div[name=body] div[name*=row]').last();
    let newRowNumber = parseInt(rowNumber) + parseInt(1);
    //APPEND AND REPLACE NUM//
    //WORK!! THIS LINE...//$('div#ac8032-bj div[name=body]').append('<div name="row-' + newRowNumber + '">' + rowHtml + '</div>');
    let newDivRowHtml = ('<div name="row-' + newRowNumber + '">' + rowHtml + '</div>');
    const re = /\[\d+\]/gm;
    newDivRowHtml = newDivRowHtml.replace(re, '[' + newRowNumber + ']');
    //REPLACE FUNCTION ARGUMENT(S)//
    newDivRowHtml = newDivRowHtml.replace('bjACIDchange(0', 'bjACIDchange(' + newRowNumber.toString());
    newDivRowHtml = newDivRowHtml.replace('bjSBIDchange(0', 'bjSBIDchange(' + newRowNumber.toString());

    //原始//
    //$('div#ac8032-bj div[name=body]').append(newDivRowHtml);
    //現在//
    $('div#ac8032-bj #ac8032-bj-body').append(newDivRowHtml);

    //CHANGE rowId//
    //$('div#ac8032-bj div[name=body] div[name=row-' + newRowNumber + '] input[name=rowId]').val(newRowNumber);
    ////$('div#ac8032-bj div[name=body] div[name=row-' + newRowNumber + '] input.field-sqno').val(newRowNumber);
};
$(document).ready(function () {
    
});
function ACBDEditNewRow() {
    alert('onbluer...');
    ////var acbjArr = ;
    //$('#ac8032-bj div[name=row]')
    ////((Array)acbjArr)//.$('div#ac8032-bj').serialize()
};
