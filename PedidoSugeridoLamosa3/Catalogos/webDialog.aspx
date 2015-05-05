<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webDialog.aspx.cs" Inherits="PedidoSugeridoLamosa.Catalogos.webDialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=titulo%></title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
</head>
		<!--
	Forma de Uso:
					webDialog.asp?src=nombredestino&titulo=caption&parametros_destino
					
					El parametro 'src' es requerido, y es el visor o forma a mostrar.
					Todos los parámetros se mandarán también a la forma 'src'.
					
					El parámetro 'titulo' es el titulo a mostrar en el visor.
					
					Ej:
						webDialog.asp?src=visor.aspx&titulo=Visor de Viajes&ofna=MTY
						
						mandArá a llamar a:
						
						visor.aspx?ofna=MTY&src=visor.aspx
	-->
<body>
		<iframe frameborder=no name="closeMessage" src='closemessage.htm'>
		<iframe frameborder=no scrolling=auto name="webDialog" src='<%=source%>'>
</body>
</html>
