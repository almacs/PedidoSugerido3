using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace PedidoSugeridoLamosa
{
    public class UsuarioInf
    {
        public UsuarioInf()
        {
        }
       
        #region Private Members

        private string p_iUsuarioId;
        private string p_sNombreUsuario;
        private string p_sApellidoPaterno;
        private string p_sApellidoMaterno;
        private string p_sContrasenia;
        private string p_sCorreoElectronico;
        private string p_siCienteId;
        private string p_siCompaniaId;
        private string p_iRolId;
        private int p_siEstatusId;
        private string p_siCliente;
        private string p_sSucursalId;
        private string p_sNombreSucursal;
        private string p_sNombreRol;
        private DataTable p_UsuData;

        #endregion

        #region Public Members
        public string Id_usuario 
        {
            get 
            {
                return p_iUsuarioId;
            }
            set 
            {
                p_iUsuarioId = value;
            }
        }

        public string Nombre_usuario
        {
            get
            {
                return p_sNombreUsuario;
            }
            set
            {
                p_sNombreUsuario = value;
            }
        }

        public string Apellido_paterno
        {
            get
            {
                return p_sApellidoPaterno;
            }
            set
            {
                p_sApellidoPaterno = value;
            }
        }

        public string Apellido_materno
        {
            get
            {
                return p_sApellidoMaterno;
            }
            set
            {
                p_sApellidoMaterno = value;
            }
        }

        public string Password
        {
            get
            {
                return p_sContrasenia;
            }
            set
            {
                p_sContrasenia = value;
            }
        }

        public string Mail
        {
            get
            {
                return p_sCorreoElectronico;
            }
            set
            {
                p_sCorreoElectronico = value;
            }
        }

        public string id_cliente
        {
            get
            {
                return p_siCienteId;
            }
            set
            {
                p_siCienteId = value;
            }
        }


        public string id_compania
        {
            get
            {
                return p_siCompaniaId;
            }
            set
            {
                p_siCompaniaId = value;
            }
        }


        public string id_tipo_usuario
        {
            get
            {
                return p_iRolId;
            }
            set
            {
                p_iRolId = value;
            }
        }

            public int id_estatus 
        {
            get
            {
                return p_siEstatusId;
            }
            set
            {
                p_siEstatusId = value;
            }
        }

            public string Nombre_cliente
            {
                get
                {
                    return p_siCliente;
                }
                set
                {
                    p_siCliente = value;
                }
            }

            public string id_sucursal
            {
                get
                {
                    return p_sSucursalId;
                }
                set
                {
                    p_sSucursalId = value;
                }
            }

            public string Nombre_sucursal
            {
                get
                {
                    return p_sNombreSucursal;
                }
                set
                {
                    p_sNombreSucursal = value;
                }
            }

            public string Nombre_rol
            {
                get
                {
                    return p_sNombreRol;
                }
                set
                {
                    p_sNombreRol = value;
                }
            }

            public DataTable UsuData
            {
                get
                {
                    return p_UsuData;
                }
                set
                {
                    p_UsuData = value;
                }
            }
        #endregion
    }

}
