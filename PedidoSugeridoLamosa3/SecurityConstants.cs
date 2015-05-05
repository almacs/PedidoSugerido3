using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections;
using System.Xml;

namespace PedidoSugeridoLamosa
{
	public class SecurityConstants : Page 
	{
		/// <summary>
		/// Obtenemos Mensaje para determinada Operacion
		/// </summary>
		/// <param name="iIndex">Devuelve el Mensaje correspondiente a este Index</param>
		/// <returns></returns>
		public string MsgText(int iIndex)
		{
			string sText = iIndex.ToString();
			using (XmlTextReader reader = new XmlTextReader(MapPath("../Administracion/xmlMensajero.xml")))
			{
				reader.MoveToContent();
				reader.ReadStartElement();
				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element && reader.Name == "ID")
					{
						if (reader.ReadString() == sText)
						{
							reader.ReadToNextSibling("Mensaje");
							sText = reader.ReadString();
							break;
						}
					}
				}
				reader.Close();
			}
			return sText;
		}

		public void BotonConfirmacion(System.Web.UI.WebControls.WebControl boton, string Mensaje)
		{
			//if (!Page.IsPostBack)
			//{
			boton.Attributes.Add("OnClick", "javascript:if(Page_ClientValidate()){return confirm('" + Mensaje + "')};");
			//}

		}


		public class Controles
		{
			private int id;
			private string nombre;
			private ArrayList ctrls = new ArrayList();

			/// <summary>
			/// Obtienes el valor Id de la Pantalla
			/// </summary>
			public int Id
			{
				get
				{ return id; }
				set
				{ id = value; }
			}

			/// <summary>
			/// Obtienes el valor Nombre de la Pantalla
			/// </summary>
			public string Nombre
			{
				get
				{ return nombre; }
				set
				{ nombre = value; }
			}

			/// <summary>
			/// Coleccion de Id's de sus respectivos Controles
			/// </summary>
			public ArrayList Cntrols
			{
				get
				{ return ctrls; }
				set
				{ ctrls = value; }
			}

		}

		#region Private Members

		DataTable Forms = new DataTable("Pantallas");
		DataTable Cntrls = new DataTable("Controles");

		ArrayList Relacion = new ArrayList();

		//*** Agregamos una Instancia por c/Pantalla
		Controles Bitacora = new Controles();
		Controles BusquedaClientes = new Controles();
		Controles BusquedaProveedores = new Controles();
		Controles CentrosEmbarcadores = new Controles();
		Controles ClienteCredito = new Controles();
		Controles ClienteDirecciones = new Controles();
		Controles Clientes = new Controles();
		Controles Conversion = new Controles();
		Controles Departamentos = new Controles();
		Controles Empleados = new Controles();
		Controles Empresa = new Controles();
		Controles Estados = new Controles();
		Controles ListPrecios = new Controles();
		Controles Monedas = new Controles();
		Controles Municipios = new Controles();
		Controles ParametrosSucursal = new Controles();
		Controles Precios = new Controles();
		Controles Productos = new Controles();
		Controles ProductosSucursal = new Controles();
		Controles Proveedores = new Controles();
		Controles Puestos = new Controles();
		Controles PuntosDeVenta = new Controles();
		Controles Sucursales = new Controles();
		Controles Unidades = new Controles();
		Controles ClasesCuenta = new Controles();
		Controles CuentasContables = new Controles();
		Controles Naturaleza = new Controles();
		Controles SubTiposCuenta = new Controles();
		Controles TiposCuenta = new Controles();
		Controles TiposPoliza = new Controles();
		Controles PreciosCE = new Controles();
		Controles DatosParticulares = new Controles();
		Controles Roles = new Controles();
		Controles PermisosControlesPantallas = new Controles();
		Controles Usuarios = new Controles();
		Controles Users = new Controles();
		Controles TiposPV = new Controles();
		Controles ConcultaPolizas = new Controles();
		Controles Polizas = new Controles();
		Controles ConsultaPolizasTipoF = new Controles();
		Controles FoliosPolizas = new Controles();
		Controles AuxiliarCuentasTipo = new Controles();
		Controles SaldosDiarios = new Controles();
		Controles CierreAnual = new Controles();
		Controles AuxiliarCuentasRango = new Controles();
		Controles PromedioSaldosDiarios = new Controles();
		Controles AnexosCatalogo = new Controles();
		Controles PromedioSaldosIF = new Controles();
		Controles BalanzaComprobacion = new Controles();
		Controles AuxiliarMayorRango = new Controles();
		Controles AuxiliarMayorTipo = new Controles();
		Controles MayorGeneral = new Controles();
		Controles BalanceGeneral = new Controles();
		Controles Mensajero = new Controles();
		Controles PolizaApertura = new Controles();
		Controles CierreMensual = new Controles();
		Controles EdoResultados = new Controles();
		Controles Transportistas = new Controles();
		Controles UnidadesPorTransportistas = new Controles();
		Controles Tanques = new Controles();
		Controles ConfiguracionDeCuentasSucursal = new Controles();
		Controles ConfiguracionPV = new Controles();
		Controles VentasCreditoConsulta = new Controles();
		Controles VentasCreditoRegistro = new Controles();
		Controles VentasContadoConsulta = new Controles();
		Controles VentasContadoRegistro = new Controles();
		Controles RecepcionGas = new Controles();
		Controles ProcesoPolizaIngreso = new Controles();
		Controles UnidadesAdmin = new Controles();
		Controles ConsumoAnticiposConsulta = new Controles();
		Controles ConsumoAnticiposRegistro = new Controles();
		Controles PorcentajesFinales = new Controles();
		Controles ObsequiosConsulta = new Controles();
		Controles ObsequiosRegistro = new Controles();
		Controles ChecquesDevueltos = new Controles();
		Controles TipoPrecios = new Controles();
		Controles GeneraReportePolizaIngreso = new Controles();
		Controles DepositosConsulta = new Controles();
		Controles DepositosRegistro = new Controles();
		Controles Autoconsumos = new Controles();
		Controles ControlDocumentos = new Controles();
		Controles TraspasosConsulta = new Controles();
		Controles TraspasosRegistro = new Controles();
		Controles TraspasosPorRecibir = new Controles();
		Controles DeudoresDiversos = new Controles();


		//*** Agregamos una Instancia por c/Pantalla        

		#endregion

		public SecurityConstants()
		{
			//===== Contruccion de la Tabla Controles =============================================
			Cntrls.Columns.Add("Id");
			Cntrls.Columns.Add("Nombre");
			Cntrls.Rows.Add(1, "Guardar");
			Cntrls.Rows.Add(2, "Cancelar");
			Cntrls.Rows.Add(3, "Buscar");
			Cntrls.Rows.Add(4, "Editar");
			Cntrls.Rows.Add(5, "Imprimir");
			Cntrls.Rows.Add(6, "Exportar");
			Cntrls.Rows.Add(7, "Nuevo");
			Cntrls.Rows.Add(8, "Borrar");
			Cntrls.Rows.Add(9, "LibreSucursal");
			Cntrls.Rows.Add(10, "Aprobar");
			Cntrls.Rows.Add(11, "LibreEjercicio");
			Cntrls.Rows.Add(12, "GuardarAnt");
			Cntrls.Rows.Add(13, "GenerarEjercicio");
			Cntrls.Rows.Add(14, "Ejecutar");
			Cntrls.Rows.Add(15, "Abrir");
			Cntrls.Rows.Add(16, "Editar Tipos Precio");
			//=====================================================================================

			//======================= Construimos la Tabla Pantallas y asignasmos Constantes ====================//
			Forms.Columns.Add("Id");
			Forms.Columns.Add("Nombre");


			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Bitacora.Id = 1, Bitacora.Nombre = "Bitacora");
			Bitacora.Cntrols.Add(3);
			Bitacora.Cntrols.Add(6);
			Relacion.Add(Bitacora);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(BusquedaClientes.Id = 2, BusquedaClientes.Nombre = "BusquedaClientes");
			BusquedaClientes.Cntrols.Add(3);
			BusquedaClientes.Cntrols.Add(7);
			Relacion.Add(BusquedaClientes);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(BusquedaProveedores.Id = 3, BusquedaProveedores.Nombre = "BusquedaProveedores");
			BusquedaProveedores.Cntrols.Add(3);
			BusquedaProveedores.Cntrols.Add(6);
			BusquedaProveedores.Cntrols.Add(1);
			Relacion.Add(BusquedaProveedores);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(CentrosEmbarcadores.Id = 4, CentrosEmbarcadores.Nombre = "CentrosEmbarcadores");
			CentrosEmbarcadores.Cntrols.Add(1);
			CentrosEmbarcadores.Cntrols.Add(2);
			CentrosEmbarcadores.Cntrols.Add(4);
			CentrosEmbarcadores.Cntrols.Add(6);
			Relacion.Add(CentrosEmbarcadores);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ClienteCredito.Id = 5, ClienteCredito.Nombre = "ClienteCredito");
			ClienteCredito.Cntrols.Add(1);
			ClienteCredito.Cntrols.Add(2);
			ClienteCredito.Cntrols.Add(4);
			ClienteCredito.Cntrols.Add(6);
			Relacion.Add(ClienteCredito);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ClienteDirecciones.Id = 6, ClienteDirecciones.Nombre = "ClienteDirecciones");
			ClienteDirecciones.Cntrols.Add(1);
			ClienteDirecciones.Cntrols.Add(2);
			ClienteDirecciones.Cntrols.Add(4);
			ClienteDirecciones.Cntrols.Add(6);
			Relacion.Add(ClienteDirecciones);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Clientes.Id = 7, Clientes.Nombre = "Clientes");
			Clientes.Cntrols.Add(1);
			Clientes.Cntrols.Add(2);
			Clientes.Cntrols.Add(6);
			Relacion.Add(Clientes);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Conversion.Id = 8, Conversion.Nombre = "Conversion");
			Conversion.Cntrols.Add(1);
			Conversion.Cntrols.Add(2);
			Conversion.Cntrols.Add(4);
			Conversion.Cntrols.Add(6);
			Relacion.Add(Conversion);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Departamentos.Id = 9, Departamentos.Nombre = "Departamentos");
			Departamentos.Cntrols.Add(1);
			Departamentos.Cntrols.Add(2);
			Departamentos.Cntrols.Add(4);
			Departamentos.Cntrols.Add(6);
			Relacion.Add(Departamentos);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Empleados.Id = 10, Empleados.Nombre = "Empleados");
			Empleados.Cntrols.Add(1);
			Empleados.Cntrols.Add(2);
			Empleados.Cntrols.Add(4);
			Empleados.Cntrols.Add(6);
			Relacion.Add(Empleados);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Empresa.Id = 11, Empresa.Nombre = "Empresa");
			Empresa.Cntrols.Add(1);
			Empresa.Cntrols.Add(2);
			Relacion.Add(Empresa);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Estados.Id = 12, Estados.Nombre = "Estados");
			Estados.Cntrols.Add(1);
			Estados.Cntrols.Add(2);
			Estados.Cntrols.Add(4);
			Estados.Cntrols.Add(6);
			Relacion.Add(Estados);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ListPrecios.Id = 13, ListPrecios.Nombre = "ListPrecios");
			ListPrecios.Cntrols.Add(1);
			ListPrecios.Cntrols.Add(2);
			Relacion.Add(ListPrecios);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Monedas.Id = 14, Monedas.Nombre = "Monedas");
			Monedas.Cntrols.Add(1);
			Monedas.Cntrols.Add(2);
			Monedas.Cntrols.Add(4);
			Monedas.Cntrols.Add(6);
			Relacion.Add(Monedas);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Municipios.Id = 15, Municipios.Nombre = "Municipios");
			Municipios.Cntrols.Add(1);
			Municipios.Cntrols.Add(2);
			Municipios.Cntrols.Add(4);
			Municipios.Cntrols.Add(6);
			Relacion.Add(Municipios);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ParametrosSucursal.Id = 16, ParametrosSucursal.Nombre = "ParametrosSucursal");
			ParametrosSucursal.Cntrols.Add(1);
			ParametrosSucursal.Cntrols.Add(2);
			ParametrosSucursal.Cntrols.Add(4);
			ParametrosSucursal.Cntrols.Add(6);
			Relacion.Add(ParametrosSucursal);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Precios.Id = 17, Precios.Nombre = "Precios");
			Precios.Cntrols.Add(6);
			Precios.Cntrols.Add(7);
			Relacion.Add(Precios);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Productos.Id = 18, Productos.Nombre = "Productos");
			Productos.Cntrols.Add(1);
			Productos.Cntrols.Add(6);
			Productos.Cntrols.Add(4);
			Relacion.Add(Productos);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ProductosSucursal.Id = 19, ProductosSucursal.Nombre = "ProductosSucursal");
			ProductosSucursal.Cntrols.Add(1);
			ProductosSucursal.Cntrols.Add(2);
			ProductosSucursal.Cntrols.Add(4);
			Relacion.Add(ProductosSucursal);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Proveedores.Id = 20, Proveedores.Nombre = "Proveedores");
			Proveedores.Cntrols.Add(1);
			Proveedores.Cntrols.Add(2);
			Proveedores.Cntrols.Add(6);
			Relacion.Add(Proveedores);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Puestos.Id = 21, Puestos.Nombre = "Puestos");
			Puestos.Cntrols.Add(1);
			Puestos.Cntrols.Add(2);
			Puestos.Cntrols.Add(4);
			Puestos.Cntrols.Add(6);
			Relacion.Add(Puestos);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(PuntosDeVenta.Id = 22, PuntosDeVenta.Nombre = "PuntosDeVenta");
			PuntosDeVenta.Cntrols.Add(1);
			PuntosDeVenta.Cntrols.Add(6);
			PuntosDeVenta.Cntrols.Add(4);
			PuntosDeVenta.Cntrols.Add(9);
			Relacion.Add(PuntosDeVenta);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Sucursales.Id = 23, Sucursales.Nombre = "Sucursales");
			Sucursales.Cntrols.Add(1);
			Sucursales.Cntrols.Add(2);
			Sucursales.Cntrols.Add(4);
			Sucursales.Cntrols.Add(6);
			Relacion.Add(Sucursales);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Unidades.Id = 24, Unidades.Nombre = "Unidades");
			Unidades.Cntrols.Add(1);
			Unidades.Cntrols.Add(2);
			Unidades.Cntrols.Add(4);
			Unidades.Cntrols.Add(6);
			Relacion.Add(Unidades);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ClasesCuenta.Id = 25, ClasesCuenta.Nombre = "ClasesCuenta");
			ClasesCuenta.Cntrols.Add(1);
			ClasesCuenta.Cntrols.Add(2);
			ClasesCuenta.Cntrols.Add(4);
			ClasesCuenta.Cntrols.Add(6);
			Relacion.Add(ClasesCuenta);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(CuentasContables.Id = 26, CuentasContables.Nombre = "CuentasContables");
			CuentasContables.Cntrols.Add(1);
			CuentasContables.Cntrols.Add(2);
			CuentasContables.Cntrols.Add(3);
			CuentasContables.Cntrols.Add(4);
			CuentasContables.Cntrols.Add(6);
			CuentasContables.Cntrols.Add(8);
			Relacion.Add(CuentasContables);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Naturaleza.Id = 27, Naturaleza.Nombre = "Naturaleza");
			Naturaleza.Cntrols.Add(1);
			Naturaleza.Cntrols.Add(2);
			Naturaleza.Cntrols.Add(4);
			Naturaleza.Cntrols.Add(6);
			Naturaleza.Cntrols.Add(8);
			Relacion.Add(Naturaleza);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(SubTiposCuenta.Id = 28, SubTiposCuenta.Nombre = "SubTiposCuenta");
			SubTiposCuenta.Cntrols.Add(1);
			SubTiposCuenta.Cntrols.Add(2);
			SubTiposCuenta.Cntrols.Add(4);
			SubTiposCuenta.Cntrols.Add(6);
			SubTiposCuenta.Cntrols.Add(8);
			Relacion.Add(SubTiposCuenta);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(TiposCuenta.Id = 29, TiposCuenta.Nombre = "TiposCuenta");
			TiposCuenta.Cntrols.Add(1);
			TiposCuenta.Cntrols.Add(2);
			TiposCuenta.Cntrols.Add(4);
			TiposCuenta.Cntrols.Add(6);
			TiposCuenta.Cntrols.Add(8);
			Relacion.Add(TiposCuenta);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(TiposPoliza.Id = 30, TiposPoliza.Nombre = "TiposPoliza");
			TiposPoliza.Cntrols.Add(1);
			TiposPoliza.Cntrols.Add(2);
			TiposPoliza.Cntrols.Add(4);
			TiposPoliza.Cntrols.Add(6);
			TiposPoliza.Cntrols.Add(8);
			Relacion.Add(TiposPoliza);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(PreciosCE.Id = 31, PreciosCE.Nombre = "PreciosCE");
			PreciosCE.Cntrols.Add(1);
			PreciosCE.Cntrols.Add(3);
			PreciosCE.Cntrols.Add(4);
			PreciosCE.Cntrols.Add(6);
			PreciosCE.Cntrols.Add(8);
			Relacion.Add(PreciosCE);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(DatosParticulares.Id = 32, DatosParticulares.Nombre = "DatosParticulares");
			DatosParticulares.Cntrols.Add(1);
			DatosParticulares.Cntrols.Add(2);
			DatosParticulares.Cntrols.Add(4);
			Relacion.Add(DatosParticulares);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Roles.Id = 33, Roles.Nombre = "Roles");
			Roles.Cntrols.Add(1);
			Roles.Cntrols.Add(2);
			Roles.Cntrols.Add(4);
			Roles.Cntrols.Add(6);
			Relacion.Add(Roles);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(PermisosControlesPantallas.Id = 34, PermisosControlesPantallas.Nombre = "PermisosControlesPantallas");
			PermisosControlesPantallas.Cntrols.Add(1);
			PermisosControlesPantallas.Cntrols.Add(2);
			Relacion.Add(PermisosControlesPantallas);


			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Usuarios.Id = 36, Usuarios.Nombre = "Usuarios");
			Usuarios.Cntrols.Add(1);
			Usuarios.Cntrols.Add(2);
			Usuarios.Cntrols.Add(4);
			Usuarios.Cntrols.Add(6);
			Relacion.Add(Usuarios);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Users.Id = 120, Users.Nombre = "Users");
			Users.Cntrols.Add(1);
			Users.Cntrols.Add(2);
			Users.Cntrols.Add(4);
			Users.Cntrols.Add(6);
			Relacion.Add(Users);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(TiposPV.Id = 37, TiposPV.Nombre = "TiposPV");
			TiposPV.Cntrols.Add(1);
			TiposPV.Cntrols.Add(2);
			TiposPV.Cntrols.Add(4);
			TiposPV.Cntrols.Add(6);
			Relacion.Add(TiposPV);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ConcultaPolizas.Id = 38, ConcultaPolizas.Nombre = "ConcultaPolizas");
			ConcultaPolizas.Cntrols.Add(3);
			ConcultaPolizas.Cntrols.Add(9);
			ConcultaPolizas.Cntrols.Add(11);
			Relacion.Add(ConcultaPolizas);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Polizas.Id = 39, Polizas.Nombre = "Polizas");
			Polizas.Cntrols.Add(1);
			Polizas.Cntrols.Add(2);
			Polizas.Cntrols.Add(3);
			Polizas.Cntrols.Add(4);
			Polizas.Cntrols.Add(6);
			Polizas.Cntrols.Add(8);
			Polizas.Cntrols.Add(9);
			Polizas.Cntrols.Add(10);
			Polizas.Cntrols.Add(11);
			Polizas.Cntrols.Add(12);
			Relacion.Add(Polizas);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ConsultaPolizasTipoF.Id = 40, ConsultaPolizasTipoF.Nombre = "ConsultaPolizasTipoF");
			ConsultaPolizasTipoF.Cntrols.Add(3);
			ConsultaPolizasTipoF.Cntrols.Add(9);
			ConsultaPolizasTipoF.Cntrols.Add(11);
			Relacion.Add(ConsultaPolizasTipoF);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(FoliosPolizas.Id = 41, FoliosPolizas.Nombre = "FoliosPolizas");
			FoliosPolizas.Cntrols.Add(1);
			FoliosPolizas.Cntrols.Add(2);
			FoliosPolizas.Cntrols.Add(8);
			FoliosPolizas.Cntrols.Add(9);
			FoliosPolizas.Cntrols.Add(13);
			Relacion.Add(FoliosPolizas);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(AuxiliarCuentasTipo.Id = 42, AuxiliarCuentasTipo.Nombre = "AuxiliarCuentasTipo");
			AuxiliarCuentasTipo.Cntrols.Add(2);
			AuxiliarCuentasTipo.Cntrols.Add(3);
			AuxiliarCuentasTipo.Cntrols.Add(9);
			AuxiliarCuentasTipo.Cntrols.Add(11);
			Relacion.Add(AuxiliarCuentasTipo);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(SaldosDiarios.Id = 43, SaldosDiarios.Nombre = "SaldosDiarios");
			SaldosDiarios.Cntrols.Add(3);
			Relacion.Add(SaldosDiarios);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(CierreAnual.Id = 44, CierreAnual.Nombre = "Cierre Anual");
			CierreAnual.Cntrols.Add(14);
			CierreAnual.Cntrols.Add(15);
			Relacion.Add(CierreAnual);


			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(AuxiliarCuentasRango.Id = 45, AuxiliarCuentasRango.Nombre = "AuxiliarCuentasRango");
			AuxiliarCuentasRango.Cntrols.Add(2);
			AuxiliarCuentasRango.Cntrols.Add(3);
			AuxiliarCuentasRango.Cntrols.Add(9);
			AuxiliarCuentasRango.Cntrols.Add(11);
			Relacion.Add(AuxiliarCuentasRango);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(PromedioSaldosDiarios.Id = 46, PromedioSaldosDiarios.Nombre = "Promedio Saldos Diarios");
			PromedioSaldosDiarios.Cntrols.Add(2);
			PromedioSaldosDiarios.Cntrols.Add(3);
			Relacion.Add(PromedioSaldosDiarios);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(AnexosCatalogo.Id = 47, AnexosCatalogo.Nombre = "Anexos Catalogo");
			AnexosCatalogo.Cntrols.Add(2);
			AnexosCatalogo.Cntrols.Add(3);
			AnexosCatalogo.Cntrols.Add(6);
			Relacion.Add(AnexosCatalogo);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(PromedioSaldosIF.Id = 48, PromedioSaldosIF.Nombre = "Promedio Saldos IF");
			PromedioSaldosIF.Cntrols.Add(2);
			PromedioSaldosIF.Cntrols.Add(3);
			PromedioSaldosIF.Cntrols.Add(6);
			Relacion.Add(PromedioSaldosIF);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(BalanzaComprobacion.Id = 49, BalanzaComprobacion.Nombre = "Balanza de Comprobacion");
			BalanzaComprobacion.Cntrols.Add(3);
			BalanzaComprobacion.Cntrols.Add(6);
			BalanzaComprobacion.Cntrols.Add(9);
			Relacion.Add(BalanzaComprobacion);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(AuxiliarMayorTipo.Id = 50, AuxiliarMayorTipo.Nombre = "AuxiliarMayorTipo");
			AuxiliarMayorTipo.Cntrols.Add(2);
			AuxiliarMayorTipo.Cntrols.Add(3);
			AuxiliarMayorTipo.Cntrols.Add(9);
			AuxiliarMayorTipo.Cntrols.Add(11);
			Relacion.Add(AuxiliarMayorTipo);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(AuxiliarMayorRango.Id = 51, AuxiliarMayorRango.Nombre = "AuxiliarMayorRango");
			AuxiliarMayorRango.Cntrols.Add(2);
			AuxiliarMayorRango.Cntrols.Add(3);
			AuxiliarMayorRango.Cntrols.Add(9);
			AuxiliarMayorRango.Cntrols.Add(11);
			Relacion.Add(AuxiliarMayorRango);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(MayorGeneral.Id = 52, MayorGeneral.Nombre = "MayorGeneral");
			MayorGeneral.Cntrols.Add(9);
			Relacion.Add(MayorGeneral);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(BalanceGeneral.Id = 53, BalanceGeneral.Nombre = "BalanceGeneral");
			BalanceGeneral.Cntrols.Add(2);
			BalanceGeneral.Cntrols.Add(9);
			BalanceGeneral.Cntrols.Add(11);
			Relacion.Add(BalanceGeneral);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Mensajero.Id = 54, Mensajero.Nombre = "Mensajero");
			Mensajero.Cntrols.Add(1);
			Mensajero.Cntrols.Add(2);
			Relacion.Add(Mensajero);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(PolizaApertura.Id = 55, PolizaApertura.Nombre = "PolizaApertura");
			PolizaApertura.Cntrols.Add(6);
			PolizaApertura.Cntrols.Add(8);
			PolizaApertura.Cntrols.Add(11);
			PolizaApertura.Cntrols.Add(14);
			Relacion.Add(PolizaApertura);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(CierreMensual.Id = 56, CierreMensual.Nombre = "CierreMensual");
			CierreMensual.Cntrols.Add(3);
			CierreMensual.Cntrols.Add(4);
			CierreMensual.Cntrols.Add(11);
			CierreMensual.Cntrols.Add(14);
			CierreMensual.Cntrols.Add(15);
			Relacion.Add(CierreMensual);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(EdoResultados.Id = 57, EdoResultados.Nombre = "Estado de Resultados");
			EdoResultados.Cntrols.Add(2);
			EdoResultados.Cntrols.Add(6);
			Relacion.Add(EdoResultados);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Transportistas.Id = 58, Transportistas.Nombre = "Transportistas");
			//Transportistas.Cntrols.Add(1);
			//Transportistas.Cntrols.Add(2);
			Transportistas.Cntrols.Add(3);
			Transportistas.Cntrols.Add(4);
			Transportistas.Cntrols.Add(6);
			Transportistas.Cntrols.Add(8);
			Relacion.Add(Transportistas);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(UnidadesPorTransportistas.Id = 59, UnidadesPorTransportistas.Nombre = "Unidades Por Transportistas");
			UnidadesPorTransportistas.Cntrols.Add(1);
			UnidadesPorTransportistas.Cntrols.Add(2);
			UnidadesPorTransportistas.Cntrols.Add(4);
			UnidadesPorTransportistas.Cntrols.Add(6);
			UnidadesPorTransportistas.Cntrols.Add(8);
			Relacion.Add(UnidadesPorTransportistas);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Tanques.Id = 60, Tanques.Nombre = "Tanques");
			Tanques.Cntrols.Add(3);
			Tanques.Cntrols.Add(4);
			Tanques.Cntrols.Add(6);
			Relacion.Add(Tanques);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(UnidadesAdmin.Id = 61, UnidadesAdmin.Nombre = "UnidadesAdmin");

			UnidadesAdmin.Cntrols.Add(4);
			UnidadesAdmin.Cntrols.Add(6);
			UnidadesAdmin.Cntrols.Add(9);
			Relacion.Add(UnidadesAdmin);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ConfiguracionDeCuentasSucursal.Id = 64, ConfiguracionDeCuentasSucursal.Nombre = "Configuracion De Cuentas Sucursal");
			ConfiguracionDeCuentasSucursal.Cntrols.Add(3);
			ConfiguracionDeCuentasSucursal.Cntrols.Add(4);
			ConfiguracionDeCuentasSucursal.Cntrols.Add(6);
			Relacion.Add(ConfiguracionDeCuentasSucursal);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ConfiguracionPV.Id = 65, ConfiguracionPV.Nombre = "Configuracion De Unidades Administrativas/Puntos Venta");
			ConfiguracionPV.Cntrols.Add(1);
			ConfiguracionPV.Cntrols.Add(3);
			ConfiguracionPV.Cntrols.Add(6);
			Relacion.Add(ConfiguracionPV);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(VentasCreditoConsulta.Id = 66, VentasCreditoConsulta.Nombre = "Ventas Crédito Consulta");
			VentasCreditoConsulta.Cntrols.Add(3);
			VentasCreditoConsulta.Cntrols.Add(6);
			Relacion.Add(VentasCreditoConsulta);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(VentasCreditoRegistro.Id = 67, VentasCreditoRegistro.Nombre = "Ventas Crédito Registro");
			VentasCreditoRegistro.Cntrols.Add(3);
			VentasCreditoRegistro.Cntrols.Add(4);
			VentasCreditoRegistro.Cntrols.Add(6);
			VentasCreditoRegistro.Cntrols.Add(7);
			VentasCreditoRegistro.Cntrols.Add(8);
			VentasCreditoRegistro.Cntrols.Add(16);
			Relacion.Add(VentasCreditoRegistro);


			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(VentasContadoConsulta.Id = 68, VentasContadoConsulta.Nombre = "Ventas Contado Consulta");
			VentasContadoConsulta.Cntrols.Add(3);
			VentasContadoConsulta.Cntrols.Add(6);
			Relacion.Add(VentasContadoConsulta);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(VentasContadoRegistro.Id = 69, VentasContadoRegistro.Nombre = "Ventas Contado Registro");
			VentasContadoRegistro.Cntrols.Add(3);
			VentasContadoRegistro.Cntrols.Add(4);
			VentasContadoRegistro.Cntrols.Add(6);
			VentasContadoRegistro.Cntrols.Add(7);
			VentasContadoRegistro.Cntrols.Add(8);
			VentasContadoRegistro.Cntrols.Add(16);
			Relacion.Add(VentasContadoRegistro);
			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(RecepcionGas.Id = 78, RecepcionGas.Nombre = "Recepción de gas");
			RecepcionGas.Cntrols.Add(1);
			RecepcionGas.Cntrols.Add(3);
			RecepcionGas.Cntrols.Add(4);
			RecepcionGas.Cntrols.Add(6);
			RecepcionGas.Cntrols.Add(8);
			RecepcionGas.Cntrols.Add(9);
			Relacion.Add(RecepcionGas);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ProcesoPolizaIngreso.Id = 79, ProcesoPolizaIngreso.Nombre = "Procesar poliza de ingreso");
			ProcesoPolizaIngreso.Cntrols.Add(1);
			ProcesoPolizaIngreso.Cntrols.Add(3);
			ProcesoPolizaIngreso.Cntrols.Add(6);
			ProcesoPolizaIngreso.Cntrols.Add(9);
			Relacion.Add(ProcesoPolizaIngreso);
			//----------------------------------------------------------------------------------------------------

			Forms.Rows.Add(ConsumoAnticiposConsulta.Id = 70, ConsumoAnticiposConsulta.Nombre = "Consumo de Anticipos Consulta");
			ConsumoAnticiposConsulta.Cntrols.Add(9);
			ConsumoAnticiposConsulta.Cntrols.Add(6);
			Relacion.Add(ConsumoAnticiposConsulta);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ConsumoAnticiposRegistro.Id = 71, ConsumoAnticiposRegistro.Nombre = "Consumo de Anticipos Registro");
			ConsumoAnticiposRegistro.Cntrols.Add(4);
			ConsumoAnticiposRegistro.Cntrols.Add(6);
			ConsumoAnticiposRegistro.Cntrols.Add(7);
			ConsumoAnticiposRegistro.Cntrols.Add(8);
			ConsumoAnticiposRegistro.Cntrols.Add(9);
			Relacion.Add(ConsumoAnticiposRegistro);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(PorcentajesFinales.Id = 80, PorcentajesFinales.Nombre = "Porcentajes Finales");
			PorcentajesFinales.Cntrols.Add(4);
			PorcentajesFinales.Cntrols.Add(6);
			PorcentajesFinales.Cntrols.Add(9);
			Relacion.Add(PorcentajesFinales);
			//----------------------------------------------------------------------------------------------------

			Forms.Rows.Add(ObsequiosConsulta.Id = 72, ObsequiosConsulta.Nombre = "Obsequios Consulta");
			ObsequiosConsulta.Cntrols.Add(6);
			ObsequiosConsulta.Cntrols.Add(9);
			Relacion.Add(ObsequiosConsulta);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ObsequiosRegistro.Id = 73, ObsequiosRegistro.Nombre = "Obsequios Registro");
			ObsequiosRegistro.Cntrols.Add(4);
			ObsequiosRegistro.Cntrols.Add(6);
			ObsequiosRegistro.Cntrols.Add(7);
			ObsequiosRegistro.Cntrols.Add(8);
			ObsequiosRegistro.Cntrols.Add(9);
			Relacion.Add(ObsequiosRegistro);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(Autoconsumos.Id = 74, Autoconsumos.Nombre = "Autoconsumos de gas");
			Autoconsumos.Cntrols.Add(1);
			Autoconsumos.Cntrols.Add(3);
			Autoconsumos.Cntrols.Add(4);
			Autoconsumos.Cntrols.Add(6);
			Autoconsumos.Cntrols.Add(9);
			Relacion.Add(Autoconsumos);
			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ChecquesDevueltos.Id = 77, ChecquesDevueltos.Nombre = "Cheques Devueltos");
			ChecquesDevueltos.Cntrols.Add(4);
			ChecquesDevueltos.Cntrols.Add(6);
			ChecquesDevueltos.Cntrols.Add(7);
			ChecquesDevueltos.Cntrols.Add(8);
			ChecquesDevueltos.Cntrols.Add(9);
			Relacion.Add(ChecquesDevueltos);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(TipoPrecios.Id = 81, TipoPrecios.Nombre = "Tipo Precios");
			TipoPrecios.Cntrols.Add(1);
			TipoPrecios.Cntrols.Add(6);
			Relacion.Add(TipoPrecios);
			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(GeneraReportePolizaIngreso.Id = 82, GeneraReportePolizaIngreso.Nombre = "Generar reporte sustento de poliza");
			GeneraReportePolizaIngreso.Cntrols.Add(1);
			GeneraReportePolizaIngreso.Cntrols.Add(3);
			GeneraReportePolizaIngreso.Cntrols.Add(5);
			GeneraReportePolizaIngreso.Cntrols.Add(6);
			GeneraReportePolizaIngreso.Cntrols.Add(9);
			Relacion.Add(GeneraReportePolizaIngreso);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(DepositosConsulta.Id = 83, DepositosConsulta.Nombre = "Depósitos Consulta");
			DepositosConsulta.Cntrols.Add(6);
			DepositosConsulta.Cntrols.Add(9);
			Relacion.Add(DepositosConsulta);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(DepositosRegistro.Id = 84, DepositosRegistro.Nombre = "Depósitos Registro");
			DepositosRegistro.Cntrols.Add(4);
			DepositosRegistro.Cntrols.Add(6);
			DepositosRegistro.Cntrols.Add(7);
			DepositosRegistro.Cntrols.Add(8);
			DepositosRegistro.Cntrols.Add(9);
			Relacion.Add(DepositosRegistro);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(ControlDocumentos.Id = 89, ControlDocumentos.Nombre = "Control de Documentos");
			ControlDocumentos.Cntrols.Add(1);
			ControlDocumentos.Cntrols.Add(4);
			ControlDocumentos.Cntrols.Add(6);
			Relacion.Add(ControlDocumentos);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(TraspasosConsulta.Id = 90, TraspasosConsulta.Nombre = "Traspasos Consulta");
			TraspasosConsulta.Cntrols.Add(6);
			Relacion.Add(TraspasosConsulta);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(TraspasosRegistro.Id = 91, TraspasosRegistro.Nombre = "Traspasos Registro");
			TraspasosRegistro.Cntrols.Add(4);
			TraspasosRegistro.Cntrols.Add(6);
			TraspasosRegistro.Cntrols.Add(7);
			TraspasosRegistro.Cntrols.Add(8);
			TraspasosRegistro.Cntrols.Add(9);
			Relacion.Add(TraspasosRegistro);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(TraspasosPorRecibir.Id = 92, TraspasosPorRecibir.Nombre = "Traspasos por recibir");
			TraspasosPorRecibir.Cntrols.Add(4);
			TraspasosPorRecibir.Cntrols.Add(6);
			TraspasosPorRecibir.Cntrols.Add(9);
			Relacion.Add(TraspasosPorRecibir);

			//----------------------------------------------------------------------------------------------------
			Forms.Rows.Add(DeudoresDiversos.Id = 93, DeudoresDiversos.Nombre = "Deudores diversos");
			DeudoresDiversos.Cntrols.Add(1);
			DeudoresDiversos.Cntrols.Add(3);
			DeudoresDiversos.Cntrols.Add(4);
			DeudoresDiversos.Cntrols.Add(6);
			DeudoresDiversos.Cntrols.Add(9);
			Relacion.Add(DeudoresDiversos);

		}


		#region Public Members

		public DataTable dtPantallas
		{
			get { return Forms; }
		}

		public DataTable dtControles
		{
			get { return Cntrls; }
		}

		public ArrayList collRelacion
		{
			get { return Relacion; }
		}

		//******** Agregamos Propiedad por Pantalla
		public Controles pBitacora
		{
			get { return Bitacora; }
		}

		public Controles pBusquedaClientes
		{
			get { return BusquedaClientes; }
		}

		public Controles pBusquedaProveedores
		{
			get { return BusquedaProveedores; }
		}

		public Controles pCentrosEmbarcadores
		{
			get { return CentrosEmbarcadores; }
		}

		public Controles pClienteCredito
		{
			get { return ClienteCredito; }
		}

		public Controles pClienteDirecciones
		{
			get { return ClienteDirecciones; }
		}

		public Controles pClientes
		{
			get { return Clientes; }
		}

		public Controles pConversion
		{
			get { return Conversion; }
		}

		public Controles pDepartamentos
		{
			get { return Departamentos; }
		}

		public Controles pEmpleados
		{
			get { return Empleados; }
		}

		public Controles pEmpresa
		{
			get { return Empresa; }
		}

		public Controles pEstados
		{
			get { return Estados; }
		}

		public Controles pListPrecios
		{
			get { return ListPrecios; }
		}

		public Controles pMonedas
		{
			get { return Monedas; }
		}

		public Controles pMunicipios
		{
			get { return Municipios; }
		}

		public Controles pParametrosSucursal
		{
			get { return ParametrosSucursal; }
		}

		public Controles pPrecios
		{
			get { return Precios; }
		}

		public Controles pProductos
		{
			get { return Productos; }
		}

		public Controles pProductosSucursal
		{
			get { return ProductosSucursal; }
		}

		public Controles pProveedores
		{
			get { return Proveedores; }
		}

		public Controles pPuestos
		{
			get { return Puestos; }
		}

		public Controles pPuntosDeVenta
		{
			get { return PuntosDeVenta; }
		}

		public Controles pSucursales
		{
			get { return Sucursales; }
		}

		public Controles pUnidades
		{
			get { return Unidades; }
		}


		public Controles pClasesCuenta
		{
			get { return ClasesCuenta; }
		}

		public Controles pCuentasContables
		{
			get { return CuentasContables; }
		}

		public Controles pNaturaleza
		{
			get { return Naturaleza; }
		}

		public Controles pSubTiposCuenta
		{
			get { return SubTiposCuenta; }
		}

		public Controles pTiposCuenta
		{
			get { return TiposCuenta; }
		}

		public Controles pTiposPoliza
		{
			get { return TiposPoliza; }
		}

		public Controles pPreciosCE
		{
			get { return PreciosCE; }
		}

		public Controles pDatosParticulares
		{
			get { return DatosParticulares; }
		}

		public Controles pRoles
		{
			get { return Roles; }
		}

		public Controles pPermisosControlesPantallas
		{
			get { return PermisosControlesPantallas; }
		}


		public Controles pUsuarios
		{
			get { return Usuarios; }
		}

		public Controles pUsers
		{
			get { return Users; }
		}

		public Controles pTiposPV
		{
			get { return TiposPV; }
		}

		public Controles pConsultaPolizas
		{
			get { return ConcultaPolizas; }
		}

		public Controles pPolizas
		{
			get { return Polizas; }
		}

		public Controles pConsultaPolizasTipoF
		{
			get { return ConsultaPolizasTipoF; }
		}

		public Controles pFoliosPolizas
		{
			get { return FoliosPolizas; }
		}

		public Controles pAuxiliarCuentasTipo
		{
			get { return AuxiliarCuentasTipo; }
		}

		public Controles pSaldosDiarios
		{
			get { return SaldosDiarios; }
		}

		public Controles pCierreAnual
		{
			get { return CierreAnual; }
		}

		public Controles pAuxiliarCuentasRango
		{
			get { return AuxiliarCuentasRango; }
		}
		public Controles pPromedioSaldosDiarios
		{
			get { return PromedioSaldosDiarios; }
		}

		public Controles pAnexosCatalogo
		{
			get { return AnexosCatalogo; }
		}

		public Controles pPromedioSaldosIF
		{
			get { return PromedioSaldosIF; }
		}

		public Controles pBalanzaComprobacion
		{
			get { return BalanzaComprobacion; }
		}

		public Controles pAuxiliarMayorRango
		{
			get { return AuxiliarMayorRango; }
		}

		public Controles pAuxiliarMayorTipo
		{
			get { return AuxiliarMayorTipo; }
		}

		public Controles pMayorGeneral
		{
			get { return MayorGeneral; }
		}

		public Controles pBalanceGeneral
		{
			get { return BalanceGeneral; }
		}

		public Controles pMensajero
		{
			get { return Mensajero; }
		}

		public Controles pPolizaApertura
		{
			get { return PolizaApertura; }
		}

		public Controles pCierreMensual
		{
			get { return CierreMensual; }
		}

		public Controles pEstadoResultados
		{
			get { return EdoResultados; }
		}
		public Controles pTransportistas
		{
			get { return Transportistas; }
		}
		public Controles pUnidadesPorTransportistas
		{
			get { return UnidadesPorTransportistas; }
		}
		public Controles pTanques
		{
			get { return Tanques; }
		}

		public Controles pUnidadesAdmin
		{
			get { return UnidadesAdmin; }
		}


		public Controles pConfiguracionDeCuentasSucursal
		{
			get { return ConfiguracionDeCuentasSucursal; }
		}
		public Controles pConfiguracionPV
		{
			get { return ConfiguracionPV; }
		}
		public Controles pVentasCreditoConsulta
		{
			get { return VentasCreditoConsulta; }
		}
		public Controles pVentasCreditoRegistro
		{
			get { return VentasCreditoRegistro; }
		}

		public Controles pVentasContadoConsulta
		{
			get { return VentasContadoConsulta; }
		}
		public Controles pVentasContadoRegistro
		{
			get { return VentasContadoRegistro; }
		}

		public Controles pRecepcionGas
		{
			get { return RecepcionGas; }
		}
		public Controles pProcesoPolizaIngreso
		{
			get { return ProcesoPolizaIngreso; }
		}

		public Controles pConsumoAnticiposConsulta
		{
			get { return ConsumoAnticiposConsulta; }
		}

		public Controles pConsumoAnticiposRegistro
		{
			get { return ConsumoAnticiposRegistro; }
		}

		public Controles pPorcentajesFinales
		{
			get { return PorcentajesFinales; }
		}

		public Controles pObsequiosConsulta
		{
			get { return ObsequiosConsulta; }
		}

		public Controles pObsequiosRegistro
		{
			get { return ObsequiosRegistro; }
		}

		public Controles pChecquesDevueltos
		{
			get { return ChecquesDevueltos; }
		}

		public Controles pTipoPrecios
		{
			get { return TipoPrecios; }
		}
		public Controles pGeneraReportePolizaIngreso
		{
			get { return GeneraReportePolizaIngreso; }
		}
		public Controles pDepositosConsulta
		{
			get { return DepositosConsulta; }
		}
		public Controles pDepositosRegistro
		{
			get { return DepositosRegistro; }
		}
		public Controles pAutoconsumos
		{
			get { return Autoconsumos; }
		}
		public Controles pControlDocumentos
		{
			get { return ControlDocumentos; }
		}
		public Controles pTraspasosConsulta
		{
			get { return TraspasosConsulta; }
		}
		public Controles pTraspasosRegistro
		{
			get { return TraspasosRegistro; }
		}
		public Controles pTraspasosPorRecibir
		{
			get { return TraspasosPorRecibir; }
		}
		public Controles pDeudoresDiversos
		{
			get { return DeudoresDiversos; }
		}



		//******** Agregamos Propiedad por Pantalla

		#endregion
	}
}
