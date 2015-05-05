var tabs;

function htmlTabControlDesactiva(nTabsPanels) {
    var c = 0;
    
    for (c = 1; c <= nTabsPanels; c++) {
        if(document.getElementById(tabs + c) != null){
            document.getElementById('panel' + c).className = 'tab';
            document.getElementById(tabs + c).className = 'tab';
        }
    }
}
function htmlTabControlSetActive(idTab) {
    var tab = '#'+tabs+idTab
    var panel = $("#panel"+idTab)
    $(panel).removeClass().addClass('tabselect');
    $(tab).removeClass().addClass('tabselect');
}
function TabShow(ntabs, itab) {
    
    htmlTabControlDesactiva(ntabs);
    htmlTabControlSetActive(itab);
    
}
function TabHide(itab) {
    document.getElementById(tabs + idTab).style.display = 'none';
}
function TabUnhide(itab) {
    document.getElementById(tabs + idTab).style.display = 'block';
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
function ShowTab(tabId,tabsCount) {
    if(tabsCount == undefined){
        tabsCount = 2;
    } 
    tabs = tabId.substring(0,tabId.indexOf('tab')+3)
    TabShow(tabsCount, tabId.substring(tabId.lastIndexOf('tab')+3));
}