<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webDialog.aspx.cs" Inherits="PedidoSugeridoLamosa.Simulacion.webDialog" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
<body style="width:100%; height:650px;">
		<iframe frameborder=no scrolling=auto name="webDialog" src='<%=source%>' style="width:100%; height:650px;">
</body>
</html>