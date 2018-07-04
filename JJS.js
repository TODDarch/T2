function divBLUR() {
    alert('divBLUR');
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
    let newDivRowHtml = ('<div name="row-' + newRowNumber + '" onfocusout="onfoACBDrow()">' + rowHtml + '</div>');
    const re = /\[\d+\]/gm;
    newDivRowHtml = newDivRowHtml.replace(re, '[' + newRowNumber + ']');

    //REPLACE FUNCTION ARGUMENT(S)//
    newDivRowHtml = newDivRowHtml.replace('vbACIDchange(0', 'vbACIDchange(' + newRowNumber.toString());
    newDivRowHtml = newDivRowHtml.replace('vbSBIDchange(0', 'vbSBIDchange(' + newRowNumber.toString());
    $('div#ac8032-vb div[name=body]').append(newDivRowHtml);

    //CHANGE rowId and "SQNO"//
    $('div#ac8032-vb div[name=body] div[name=row-' + newRowNumber + '] input[name=rowId]').val(newRowNumber);
    //$('div#ac8032-vb div[name=body] div[name=row-' + newRowNumber + '] input.field-sqno').val(newRowNumber);

    //KEEP DATA TO WAREHOUSE//
    //$('div#data-warehouse div#acbj').append('')
    //get current num//
    //$('div#ac8032-vb div[name=body] input[name=rowId]').length
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
    $('div#ac8032-bj div[name=body]').append(newDivRowHtml);
    //CHANGE rowId//
    $('div#ac8032-bj div[name=body] div[name=row-' + newRowNumber + '] input[name=rowId]').val(newRowNumber);
    //$('div#ac8032-bj div[name=body] div[name=row-' + newRowNumber + '] input.field-sqno').val(newRowNumber);
};
function ACBDEditNewRow() {
    alert('onbluer...');
    ////var acbjArr = ;
    //$('#ac8032-bj div[name=row]')
    ////((Array)acbjArr)//.$('div#ac8032-bj').serialize()
};
