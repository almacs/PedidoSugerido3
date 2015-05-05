function htmlTabControlDesactiva(nTabsPanels) {
    var c = 0;
    for (c = 1; c <= nTabsPanels; c++) {
        try {
            document.getElementById('panel' + c).className = 'tab';
            document.getElementById('tab' + c).className = 'tab';
        }
        catch (Error) { }
    }
}
function htmlTabControlSetActive(idTab) {
    document.getElementById('panel' + idTab).className = 'tabselect';
    document.getElementById('tab' + idTab).className = 'tabselect';
}
function TabShow(ntabs, itab) {
    htmlTabControlDesactiva(ntabs);
    htmlTabControlSetActive(itab);
}
function TabHide(itab) {
    document.getElementById('tab' + idTab).style.display = 'none';
}
function TabUnhide(itab) {
    document.getElementById('tab' + idTab).style.display = 'block';
}
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
function formatToNumber(controlId, valor) {
    var numValue = 0;
    valor = parseFloat(valor);
    if (valor.toFixed)
        numValue = valor.toFixed(2);
    var s = addCommas(numValue);
    document.getElementById(controlId).value = s;
}
var _numeric = '0123456789';
var _decimal = '0123456789.';
var _alphanumeric = 'abcdefghijklmnopqrstuvwxyz0123456789:.,-¿?¡! ' + String.fromCharCode(241);

function isNumeric(evento, value) {
    var n = evento.keyCode ? evento.keyCode : evento.which;
    if (n == 8 || n == 9 || n == 13 || n == 35 || n == 36 || n == 37 || n == 39 || n == 46) return true;
    var x = String.fromCharCode(n);
    return (_numeric.indexOf(x) >= 0)
}
function isAlphanumeric(evento, value, len) {
    var n = evento.keyCode ? evento.keyCode : evento.which;
    if (n == 8 || n == 9 || n == 13 || n == 35 || n == 36 || n == 37 || n == 46) return true;
    var x = String.fromCharCode(n);
    if (len && value.length >= len) return false;
    return (_alphanumeric.indexOf(x.toLowerCase()) >= 0)
}
function isDecimal(evento, value, decimales) {
    var n = evento.keyCode ? evento.keyCode : evento.which;
    if (n == 8 || n == 9 || n == 13 || n == 35 || n == 36 || n == 37 || n == 39 || n == 46) return true;
    var x = String.fromCharCode(n);

    if (value.indexOf('.') >= 0) {
        if (decimales && value.split('.')[1].length >= decimales) return false;
        return (_numeric.indexOf(x) >= 0)
    }
    else
        return (_decimal.indexOf(x) >= 0)
}
