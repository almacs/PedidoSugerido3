/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("PedidoSugeridoLamosa");

//Comienzan funciones para proyecto Pedido sugerido
function InsertUsuario(usr, pass, nom, apPaterno, apMaterno, mail, tipo, cliente, entrega, estatus, cia) {
    //funcion para insertar usuarios
  //  alert("usuario:" + usr + " ," + "pass:" + pass + " ," + "nomb:" + nom + " ," + "apmat:" + apMaterno + " ," + "paterno:" + apPaterno + " ," + "mail:" + mail + " ," + "tipo:" + tipo + " ," + "cliente:" + cliente + " ," + "entrega:" + entrega + " ," + "estatus:" + estatus);
    PedidoSugeridoLamosa.WebService.InsertaUsuarios(usr, pass, nom, apPaterno, apMaterno, mail, tipo, cliente, entrega,
                                                           estatus, cia, SucceededCallbackReload, FailedCallbackReload, "");
}



function GetEntrega(ComboValue, selected_value) {
    //alert("vol");
    selected_value = selected_value ? selected_value : "-1";
    PedidoSugeridoLamosa.WebService.GetEntrega(ComboValue, selected_value, createEntrega, FailedCallbackReload)
}

function createEntrega(result, eventArgs) {
    //debugger;
    window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_ddlEntrega").value = result.split("|")[0];
    window.document.getElementById("ctl00_Principal_UltraWebGrid1_ctl00_txtEntrega").value = result.split("|")[1];
    //$("#cmbEntrega").html(result);
    
}

function obtieneUSR(id_cliente) {
   // alert(id_cliente); 
    if (id_cliente)
        PedidoSugeridoLamosa.WebService.obtieneUSR(id_cliente, SucceededCallbackLogin, FailedCallbackReload);
}

function UpdateUsuario(usr, pass, nom, apPaterno, apMaterno, mail, tipo, cliente, entrega, estatus, cia) {
   // alert("callUpdate");
    PedidoSugeridoLamosa.WebService.UpdateUsuario(usr, pass, nom, apPaterno, apMaterno, mail, tipo, cliente, entrega,
                                                           estatus, cia, SucceededCallbackReload, FailedCallbackReload, "");
}

function SucceededCallbackLogin(result, eventArgs) {
    // Page element to display feedback.
    if (result == '-1') {
        alert("El usuario no existe");
        document.forms[0].reset;
    }
    else 
    {
        tipo_usuario = result.split("|")[0].split("=")[1];
        
        if (tipo_usuario == 6) //ASESOR
        {
            window.document.getElementById("trSucursal").style.display = 'none';
            window.document.getElementById("trSucursal").style.visibility = 'hidden';
        }
        else
        {
            window.document.getElementById("trSucursal").style.display = '';
            window.document.getElementById("trSucursal").style.visibility = '';
        }
        $("#cmbSucursal").html(result.split("|")[1]);
         
    }
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

//Terminan funciones para proyecto pedido sugerido

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
