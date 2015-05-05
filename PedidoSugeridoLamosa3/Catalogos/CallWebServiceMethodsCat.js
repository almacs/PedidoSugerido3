/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("PedidoSugeridoLamosa.Catalogos");

function InsertArtCliente(art_generico, art_cliente, desc, clienteInSession) {
    //funcion para insertar usuarios
    alert("entro");
    PedidoSugeridoLamosa.WebService.InsertArtCliente(art_generico, art_cliente, desc, clienteInSession, SucceededCallbackReload, FailedCallbackReload);
}

function UpdateArtCliente(art_generico, art_cliente, clienteInSession) {
//    alert("generico" + art_generico);
//    alert("cliente" + art_cliente);
//    alert("usuarios" + clienteInSession);
    PedidoSugeridoLamosa.WebService.UpdateArtCliente(art_generico, art_cliente, clienteInSession, SucceededCallbackReload, FailedCallbackReload);
}

function SucceededCallbackReload(result, eventArgs) {
    // Page element to display feedback.
    alert(result);
    relaodGrid();
}

function FailedCallbackReload(error) {
    // Display the error. 
    alert("Error: " + error.get_message());
    relaodGrid();
}

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
