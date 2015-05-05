using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TasksScheduler
{
    public class clsBackOrder
    {

        public clsBackOrder()
        {
        }

        public void updateBackOrder()
        {


            DataSet ds = this.getBackOrderToLookup();
            DataTable dtToLookup = ds.Tables[0];
            DataTable dtDetail = ds.Tables[1];

            for (int int_reg = 0; int_reg < dtToLookup.Rows.Count; int_reg++)
            {
                DataView dvItems = new DataView(dtDetail);
                String CompanyId = dtToLookup.Rows[int_reg]["id_cia"].ToString();
                String CustomerId = dtToLookup.Rows[int_reg]["id_cliente"].ToString();
                String StoreId = dtToLookup.Rows[int_reg]["id_entrega"].ToString();


                dvItems.RowFilter = "id_cia = '" + CompanyId + 
                              "' AND id_cliente = '" + CustomerId + 
                              "' AND id_entrega = '" + StoreId + "'";

                com.lamosa.sap.services.DT_BackOrder_response ws_response = this.getBackOrder(CompanyId, CustomerId, StoreId, dvItems);

                

                StringBuilder strXMLItems = new StringBuilder();
                strXMLItems.Append("<Items>");

                for (int int_items = 0; int_items < ws_response.Items.Length; int_items++)
                {
                    strXMLItems.Append("<Item>");

                    strXMLItems.Append("<ItemId>");
                    strXMLItems.Append(ws_response.Items[int_items].ItemId);
                    strXMLItems.Append("</ItemId>");
                    strXMLItems.Append("<ItemCustomerId>");
                    strXMLItems.Append(ws_response.Items[int_items].ItemCustomerId);
                    strXMLItems.Append("</ItemCustomerId>");
                    strXMLItems.Append("<ItemDesc>");
                    strXMLItems.Append(ws_response.Items[int_items].ItemDesc);
                    strXMLItems.Append("</ItemDesc>");
                    strXMLItems.Append("<InventoryPending>");
                    strXMLItems.Append(ws_response.Items[int_items].InventoryPending);
                    strXMLItems.Append("</InventoryPending>");
                    strXMLItems.Append("<ConvertionValue>");
                    strXMLItems.Append(ws_response.Items[int_items].ConvertionValue);
                    strXMLItems.Append("</ConvertionValue>");

                    strXMLItems.Append("</Item>");
                }

                strXMLItems.Append("</Items>");

                this.setBackOrder(CompanyId, CustomerId, StoreId, strXMLItems.ToString());

            }



            this.resetCustomerInventory();

        }



        private DataSet resetCustomerInventory()
        {
            SqlConnection sqlC = this.getConnection();

            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("sapResetCustomerInventory", sqlC);
                sql.CommandTimeout = 0;
                sql.CommandType = CommandType.StoredProcedure;
                //SqlParameter opar = new SqlParameter("@pxml", Xml);
                //sql.Parameters.Add(opar);

                SqlDataAdapter data = new SqlDataAdapter(sql);
                data.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlC.Close();
            }
        }



        private DataSet getBackOrderToLookup()
        {
            SqlConnection sqlC = this.getConnection();

            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("sapBackOrderArt_s", sqlC);
                sql.CommandTimeout = 0;
                sql.CommandType = CommandType.StoredProcedure;
                //SqlParameter opar = new SqlParameter("@pxml", Xml);
                //sql.Parameters.Add(opar);
                
                SqlDataAdapter data = new SqlDataAdapter(sql);
                data.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlC.Close();
            }
        }

        private com.lamosa.sap.services.DT_BackOrder_response getBackOrder(String CompayId, String CustomerId, String StoreId, DataView dvItems)
        {
            com.lamosa.sap.services.SI_BackOrder_OutboundClient ws_client = new TasksScheduler.com.lamosa.sap.services.SI_BackOrder_OutboundClient("HTTP_Port");
            com.lamosa.sap.services.DT_BackOrder_request ws_request = new TasksScheduler.com.lamosa.sap.services.DT_BackOrder_request();
            com.lamosa.sap.services.DT_BackOrder_response ws_response = new TasksScheduler.com.lamosa.sap.services.DT_BackOrder_response();

            com.lamosa.sap.services.DT_BackOrder_requestItem[] oItems = new TasksScheduler.com.lamosa.sap.services.DT_BackOrder_requestItem[dvItems.Count];
            

            com.lamosa.sap.services.DT_BackOrder_requestStore[] oStores = new TasksScheduler.com.lamosa.sap.services.DT_BackOrder_requestStore[1];
            com.lamosa.sap.services.DT_BackOrder_requestStore oStore = new TasksScheduler.com.lamosa.sap.services.DT_BackOrder_requestStore();

            ws_client.ClientCredentials.UserName.UserName = System.Configuration.ConfigurationManager.AppSettings["userWSSAP"];
            ws_client.ClientCredentials.UserName.Password = System.Configuration.ConfigurationManager.AppSettings["pwdWSSAP"];


            ws_request.CompayId = CompayId;
            ws_request.CustomerId = CustomerId;

            for (int int_reg = 0; int_reg < dvItems.Count; int_reg++)
            {
                com.lamosa.sap.services.DT_BackOrder_requestItem oItem = new TasksScheduler.com.lamosa.sap.services.DT_BackOrder_requestItem();
                oItem.ItemId = dvItems[int_reg]["Cve_Articulo"].ToString();
                oItems[int_reg] = oItem;
            }

            oStore.StoreId = StoreId;            
            oStores[0] = oStore;

            ws_request.Items = oItems;
            ws_request.Stores = oStores;

            ws_response = ws_client.SI_BackOrder_Outbound(ws_request);

            return ws_response;
            
        }



        private DataSet setBackOrder(String CompanyId, String CustomerId, String StoreId, String XMLItems)
        {
            SqlConnection sqlC = this.getConnection();

            try
            {
                DataSet ds = new DataSet();
                SqlCommand sql = new SqlCommand("sapBackOrderArt_i", sqlC);
                sql.CommandTimeout = 0;
                sql.CommandType = CommandType.StoredProcedure;
                SqlParameter pCompanyId = new SqlParameter("@pid_cia", CompanyId);
                sql.Parameters.Add(pCompanyId);
                SqlParameter pCustomerId = new SqlParameter("@pid_cliente", CustomerId);
                sql.Parameters.Add(pCustomerId);
                SqlParameter pStoreId = new SqlParameter("@pid_entrega", StoreId);
                sql.Parameters.Add(pStoreId);
                SqlParameter pXMLItems = new SqlParameter("@pXMLItems", XMLItems);
                sql.Parameters.Add(pXMLItems);

                SqlDataAdapter data = new SqlDataAdapter(sql);
                data.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlC.Close();
            }
        }
        

        private SqlConnection getConnection()
        {
            SqlConnection sqlconnection = new SqlConnection();
            sqlconnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["lamosaConnectionString"].ToString();
            return sqlconnection;
        }
    }
}
