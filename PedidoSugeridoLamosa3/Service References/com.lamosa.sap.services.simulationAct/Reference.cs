﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3625
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace com.lamosa.sap.services.simulationAct {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://lamosa.com/A2A/PedidoSugerido", ConfigurationName="com.lamosa.sap.services.simulationAct.SI_Simulation_Outbound")]
    public interface SI_Simulation_Outbound {
        
        // CODEGEN: Generating message contract since the operation SI_Simulation_Outbound is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        com.lamosa.sap.services.simulationAct.SI_Simulation_OutboundResponse SI_Simulation_Outbound(com.lamosa.sap.services.simulationAct.SI_Simulation_OutboundRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://lamosa.com/A2A/PedidoSugerido")]
    public partial class DT_Simulation_request : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string compayIdField;
        
        private string customerIdField;
        
        private DT_Simulation_requestStore[] storesField;
        
        private DT_Simulation_requestItem[] itemsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string CompayId {
            get {
                return this.compayIdField;
            }
            set {
                this.compayIdField = value;
                this.RaisePropertyChanged("CompayId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string CustomerId {
            get {
                return this.customerIdField;
            }
            set {
                this.customerIdField = value;
                this.RaisePropertyChanged("CustomerId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Store", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public DT_Simulation_requestStore[] Stores {
            get {
                return this.storesField;
            }
            set {
                this.storesField = value;
                this.RaisePropertyChanged("Stores");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public DT_Simulation_requestItem[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://lamosa.com/A2A/PedidoSugerido")]
    public partial class DT_Simulation_requestStore : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string storeIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string StoreId {
            get {
                return this.storeIdField;
            }
            set {
                this.storeIdField = value;
                this.RaisePropertyChanged("StoreId");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://lamosa.com/A2A/PedidoSugerido")]
    public partial class DT_Simulation_response : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string compayIdField;
        
        private string companyNameField;
        
        private string companyAddressField;
        
        private string customerIdField;
        
        private string customerNameField;
        
        private string customerAddressField;
        
        private DT_Simulation_responseStore[] storesField;
        
        private string customerUnitOfMeasureField;
        
        private DT_Simulation_responseItem[] itemsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string CompayId {
            get {
                return this.compayIdField;
            }
            set {
                this.compayIdField = value;
                this.RaisePropertyChanged("CompayId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string CompanyName {
            get {
                return this.companyNameField;
            }
            set {
                this.companyNameField = value;
                this.RaisePropertyChanged("CompanyName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string CompanyAddress {
            get {
                return this.companyAddressField;
            }
            set {
                this.companyAddressField = value;
                this.RaisePropertyChanged("CompanyAddress");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string CustomerId {
            get {
                return this.customerIdField;
            }
            set {
                this.customerIdField = value;
                this.RaisePropertyChanged("CustomerId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string CustomerName {
            get {
                return this.customerNameField;
            }
            set {
                this.customerNameField = value;
                this.RaisePropertyChanged("CustomerName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string CustomerAddress {
            get {
                return this.customerAddressField;
            }
            set {
                this.customerAddressField = value;
                this.RaisePropertyChanged("CustomerAddress");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Store", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public DT_Simulation_responseStore[] Stores {
            get {
                return this.storesField;
            }
            set {
                this.storesField = value;
                this.RaisePropertyChanged("Stores");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string CustomerUnitOfMeasure {
            get {
                return this.customerUnitOfMeasureField;
            }
            set {
                this.customerUnitOfMeasureField = value;
                this.RaisePropertyChanged("CustomerUnitOfMeasure");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=8)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public DT_Simulation_responseItem[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
                this.RaisePropertyChanged("Items");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://lamosa.com/A2A/PedidoSugerido")]
    public partial class DT_Simulation_responseStore : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string storeIdField;
        
        private string storeNameField;
        
        private string storeAddressField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string StoreId {
            get {
                return this.storeIdField;
            }
            set {
                this.storeIdField = value;
                this.RaisePropertyChanged("StoreId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string StoreName {
            get {
                return this.storeNameField;
            }
            set {
                this.storeNameField = value;
                this.RaisePropertyChanged("StoreName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string StoreAddress {
            get {
                return this.storeAddressField;
            }
            set {
                this.storeAddressField = value;
                this.RaisePropertyChanged("StoreAddress");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://lamosa.com/A2A/PedidoSugerido")]
    public partial class DT_Simulation_responseItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string itemIdField;
        
        private string itemCustomerIdField;
        
        private string itemDescField;
        
        private string inventoryPendingField;
        
        private string isDiscontinuedField;
        
        private DT_Simulation_responseItemBackOrder backOrderField;
        
        private DT_Simulation_responseItemHistory historyField;
        
        private string billPriceField;
        
        private string unitOfMeasureField;
        
        private string convertionValueField;
        
        private string itemZoneField;
        
        private string itemLeadTimeField;
        
        private string palleteMT2Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string ItemId {
            get {
                return this.itemIdField;
            }
            set {
                this.itemIdField = value;
                this.RaisePropertyChanged("ItemId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string ItemCustomerId {
            get {
                return this.itemCustomerIdField;
            }
            set {
                this.itemCustomerIdField = value;
                this.RaisePropertyChanged("ItemCustomerId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string ItemDesc {
            get {
                return this.itemDescField;
            }
            set {
                this.itemDescField = value;
                this.RaisePropertyChanged("ItemDesc");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string InventoryPending {
            get {
                return this.inventoryPendingField;
            }
            set {
                this.inventoryPendingField = value;
                this.RaisePropertyChanged("InventoryPending");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string IsDiscontinued {
            get {
                return this.isDiscontinuedField;
            }
            set {
                this.isDiscontinuedField = value;
                this.RaisePropertyChanged("IsDiscontinued");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public DT_Simulation_responseItemBackOrder BackOrder {
            get {
                return this.backOrderField;
            }
            set {
                this.backOrderField = value;
                this.RaisePropertyChanged("BackOrder");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public DT_Simulation_responseItemHistory History {
            get {
                return this.historyField;
            }
            set {
                this.historyField = value;
                this.RaisePropertyChanged("History");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string BillPrice {
            get {
                return this.billPriceField;
            }
            set {
                this.billPriceField = value;
                this.RaisePropertyChanged("BillPrice");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=8)]
        public string UnitOfMeasure {
            get {
                return this.unitOfMeasureField;
            }
            set {
                this.unitOfMeasureField = value;
                this.RaisePropertyChanged("UnitOfMeasure");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=9)]
        public string ConvertionValue {
            get {
                return this.convertionValueField;
            }
            set {
                this.convertionValueField = value;
                this.RaisePropertyChanged("ConvertionValue");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=10)]
        public string ItemZone {
            get {
                return this.itemZoneField;
            }
            set {
                this.itemZoneField = value;
                this.RaisePropertyChanged("ItemZone");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=11)]
        public string ItemLeadTime {
            get {
                return this.itemLeadTimeField;
            }
            set {
                this.itemLeadTimeField = value;
                this.RaisePropertyChanged("ItemLeadTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=12)]
        public string PalleteMT2 {
            get {
                return this.palleteMT2Field;
            }
            set {
                this.palleteMT2Field = value;
                this.RaisePropertyChanged("PalleteMT2");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://lamosa.com/A2A/PedidoSugerido")]
    public partial class DT_Simulation_responseItemBackOrder : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string pendingField;
        
        private string noCreditField;
        
        private string pendingToAssignField;
        
        private string pendingToAssingNoInventoryField;
        
        private string assignedField;
        
        private string inTransitField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string Pending {
            get {
                return this.pendingField;
            }
            set {
                this.pendingField = value;
                this.RaisePropertyChanged("Pending");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string NoCredit {
            get {
                return this.noCreditField;
            }
            set {
                this.noCreditField = value;
                this.RaisePropertyChanged("NoCredit");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string PendingToAssign {
            get {
                return this.pendingToAssignField;
            }
            set {
                this.pendingToAssignField = value;
                this.RaisePropertyChanged("PendingToAssign");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string PendingToAssingNoInventory {
            get {
                return this.pendingToAssingNoInventoryField;
            }
            set {
                this.pendingToAssingNoInventoryField = value;
                this.RaisePropertyChanged("PendingToAssingNoInventory");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string Assigned {
            get {
                return this.assignedField;
            }
            set {
                this.assignedField = value;
                this.RaisePropertyChanged("Assigned");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string InTransit {
            get {
                return this.inTransitField;
            }
            set {
                this.inTransitField = value;
                this.RaisePropertyChanged("InTransit");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://lamosa.com/A2A/PedidoSugerido")]
    public partial class DT_Simulation_responseItemHistory : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string month_H1Field;
        
        private string month_H2Field;
        
        private string month_H3Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string Month_H1 {
            get {
                return this.month_H1Field;
            }
            set {
                this.month_H1Field = value;
                this.RaisePropertyChanged("Month_H1");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string Month_H2 {
            get {
                return this.month_H2Field;
            }
            set {
                this.month_H2Field = value;
                this.RaisePropertyChanged("Month_H2");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string Month_H3 {
            get {
                return this.month_H3Field;
            }
            set {
                this.month_H3Field = value;
                this.RaisePropertyChanged("Month_H3");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3082")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://lamosa.com/A2A/PedidoSugerido")]
    public partial class DT_Simulation_requestItem : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string itemIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string ItemId {
            get {
                return this.itemIdField;
            }
            set {
                this.itemIdField = value;
                this.RaisePropertyChanged("ItemId");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SI_Simulation_OutboundRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://lamosa.com/A2A/PedidoSugerido", Order=0)]
        public com.lamosa.sap.services.simulationAct.DT_Simulation_request MT_Simulation_request;
        
        public SI_Simulation_OutboundRequest() {
        }
        
        public SI_Simulation_OutboundRequest(com.lamosa.sap.services.simulationAct.DT_Simulation_request MT_Simulation_request) {
            this.MT_Simulation_request = MT_Simulation_request;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SI_Simulation_OutboundResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://lamosa.com/A2A/PedidoSugerido", Order=0)]
        public com.lamosa.sap.services.simulationAct.DT_Simulation_response MT_Simulation_response;
        
        public SI_Simulation_OutboundResponse() {
        }
        
        public SI_Simulation_OutboundResponse(com.lamosa.sap.services.simulationAct.DT_Simulation_response MT_Simulation_response) {
            this.MT_Simulation_response = MT_Simulation_response;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface SI_Simulation_OutboundChannel : com.lamosa.sap.services.simulationAct.SI_Simulation_Outbound, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class SI_Simulation_OutboundClient : System.ServiceModel.ClientBase<com.lamosa.sap.services.simulationAct.SI_Simulation_Outbound>, com.lamosa.sap.services.simulationAct.SI_Simulation_Outbound {
        
        public SI_Simulation_OutboundClient() {
        }
        
        public SI_Simulation_OutboundClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SI_Simulation_OutboundClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_Simulation_OutboundClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SI_Simulation_OutboundClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        com.lamosa.sap.services.simulationAct.SI_Simulation_OutboundResponse com.lamosa.sap.services.simulationAct.SI_Simulation_Outbound.SI_Simulation_Outbound(com.lamosa.sap.services.simulationAct.SI_Simulation_OutboundRequest request) {
            return base.Channel.SI_Simulation_Outbound(request);
        }
        
        public com.lamosa.sap.services.simulationAct.DT_Simulation_response SI_Simulation_Outbound(com.lamosa.sap.services.simulationAct.DT_Simulation_request MT_Simulation_request) {
            com.lamosa.sap.services.simulationAct.SI_Simulation_OutboundRequest inValue = new com.lamosa.sap.services.simulationAct.SI_Simulation_OutboundRequest();
            inValue.MT_Simulation_request = MT_Simulation_request;
            com.lamosa.sap.services.simulationAct.SI_Simulation_OutboundResponse retVal = ((com.lamosa.sap.services.simulationAct.SI_Simulation_Outbound)(this)).SI_Simulation_Outbound(inValue);
            return retVal.MT_Simulation_response;
        }
    }
}