﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 이 소스 코드가 Microsoft.VSDesigner, 버전 4.0.30319.42000에서 자동으로 생성되었습니다.
// 
#pragma warning disable 1591

namespace linuxclient.SOAP {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SysteminfoSoap", Namespace="http://tempuri.org/")]
    public partial class Systeminfo : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback LOGINOperationCompleted;
        
        private System.Threading.SendOrPostCallback COMPUTERNAMEOperationCompleted;
        
        private System.Threading.SendOrPostCallback OSOperationCompleted;
        
        private System.Threading.SendOrPostCallback CPUOperationCompleted;
        
        private System.Threading.SendOrPostCallback MEMORYOperationCompleted;
        
        private System.Threading.SendOrPostCallback DISKOperationCompleted;
        
        private System.Threading.SendOrPostCallback TrafficOperationCompleted;
        
        private System.Threading.SendOrPostCallback ALLOperationCompleted;
        
        private System.Threading.SendOrPostCallback create_logOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Systeminfo() {
            this.Url = global::linuxclient.Properties.Settings.Default.linuxclient_SOAP_Systeminfo;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;
        
        /// <remarks/>
        public event LOGINCompletedEventHandler LOGINCompleted;
        
        /// <remarks/>
        public event COMPUTERNAMECompletedEventHandler COMPUTERNAMECompleted;
        
        /// <remarks/>
        public event OSCompletedEventHandler OSCompleted;
        
        /// <remarks/>
        public event CPUCompletedEventHandler CPUCompleted;
        
        /// <remarks/>
        public event MEMORYCompletedEventHandler MEMORYCompleted;
        
        /// <remarks/>
        public event DISKCompletedEventHandler DISKCompleted;
        
        /// <remarks/>
        public event TrafficCompletedEventHandler TrafficCompleted;
        
        /// <remarks/>
        public event ALLCompletedEventHandler ALLCompleted;
        
        /// <remarks/>
        public event create_logCompletedEventHandler create_logCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void HelloWorldAsync() {
            this.HelloWorldAsync(null);
        }
        
        /// <remarks/>
        public void HelloWorldAsync(object userState) {
            if ((this.HelloWorldOperationCompleted == null)) {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldOperationCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }
        
        private void OnHelloWorldOperationCompleted(object arg) {
            if ((this.HelloWorldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/LOGIN", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet LOGIN(string Computername, string IP) {
            object[] results = this.Invoke("LOGIN", new object[] {
                        Computername,
                        IP});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void LOGINAsync(string Computername, string IP) {
            this.LOGINAsync(Computername, IP, null);
        }
        
        /// <remarks/>
        public void LOGINAsync(string Computername, string IP, object userState) {
            if ((this.LOGINOperationCompleted == null)) {
                this.LOGINOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLOGINOperationCompleted);
            }
            this.InvokeAsync("LOGIN", new object[] {
                        Computername,
                        IP}, this.LOGINOperationCompleted, userState);
        }
        
        private void OnLOGINOperationCompleted(object arg) {
            if ((this.LOGINCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LOGINCompleted(this, new LOGINCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/COMPUTERNAME", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string COMPUTERNAME([System.Xml.Serialization.XmlElementAttribute("Computername")] string Computername1, string IP) {
            object[] results = this.Invoke("COMPUTERNAME", new object[] {
                        Computername1,
                        IP});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void COMPUTERNAMEAsync(string Computername1, string IP) {
            this.COMPUTERNAMEAsync(Computername1, IP, null);
        }
        
        /// <remarks/>
        public void COMPUTERNAMEAsync(string Computername1, string IP, object userState) {
            if ((this.COMPUTERNAMEOperationCompleted == null)) {
                this.COMPUTERNAMEOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCOMPUTERNAMEOperationCompleted);
            }
            this.InvokeAsync("COMPUTERNAME", new object[] {
                        Computername1,
                        IP}, this.COMPUTERNAMEOperationCompleted, userState);
        }
        
        private void OnCOMPUTERNAMEOperationCompleted(object arg) {
            if ((this.COMPUTERNAMECompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.COMPUTERNAMECompleted(this, new COMPUTERNAMECompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/OS", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string OS([System.Xml.Serialization.XmlElementAttribute("OS")] string OS1, string IP) {
            object[] results = this.Invoke("OS", new object[] {
                        OS1,
                        IP});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void OSAsync(string OS1, string IP) {
            this.OSAsync(OS1, IP, null);
        }
        
        /// <remarks/>
        public void OSAsync(string OS1, string IP, object userState) {
            if ((this.OSOperationCompleted == null)) {
                this.OSOperationCompleted = new System.Threading.SendOrPostCallback(this.OnOSOperationCompleted);
            }
            this.InvokeAsync("OS", new object[] {
                        OS1,
                        IP}, this.OSOperationCompleted, userState);
        }
        
        private void OnOSOperationCompleted(object arg) {
            if ((this.OSCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.OSCompleted(this, new OSCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CPU", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CPU([System.Xml.Serialization.XmlElementAttribute("CPU")] string CPU1, string IP) {
            object[] results = this.Invoke("CPU", new object[] {
                        CPU1,
                        IP});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CPUAsync(string CPU1, string IP) {
            this.CPUAsync(CPU1, IP, null);
        }
        
        /// <remarks/>
        public void CPUAsync(string CPU1, string IP, object userState) {
            if ((this.CPUOperationCompleted == null)) {
                this.CPUOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCPUOperationCompleted);
            }
            this.InvokeAsync("CPU", new object[] {
                        CPU1,
                        IP}, this.CPUOperationCompleted, userState);
        }
        
        private void OnCPUOperationCompleted(object arg) {
            if ((this.CPUCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CPUCompleted(this, new CPUCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/MEMORY", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string MEMORY([System.Xml.Serialization.XmlElementAttribute("MEMORY")] string MEMORY1, string IP) {
            object[] results = this.Invoke("MEMORY", new object[] {
                        MEMORY1,
                        IP});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void MEMORYAsync(string MEMORY1, string IP) {
            this.MEMORYAsync(MEMORY1, IP, null);
        }
        
        /// <remarks/>
        public void MEMORYAsync(string MEMORY1, string IP, object userState) {
            if ((this.MEMORYOperationCompleted == null)) {
                this.MEMORYOperationCompleted = new System.Threading.SendOrPostCallback(this.OnMEMORYOperationCompleted);
            }
            this.InvokeAsync("MEMORY", new object[] {
                        MEMORY1,
                        IP}, this.MEMORYOperationCompleted, userState);
        }
        
        private void OnMEMORYOperationCompleted(object arg) {
            if ((this.MEMORYCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.MEMORYCompleted(this, new MEMORYCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DISK", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string DISK([System.Xml.Serialization.XmlElementAttribute("DISK")] string DISK1, string IP) {
            object[] results = this.Invoke("DISK", new object[] {
                        DISK1,
                        IP});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void DISKAsync(string DISK1, string IP) {
            this.DISKAsync(DISK1, IP, null);
        }
        
        /// <remarks/>
        public void DISKAsync(string DISK1, string IP, object userState) {
            if ((this.DISKOperationCompleted == null)) {
                this.DISKOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDISKOperationCompleted);
            }
            this.InvokeAsync("DISK", new object[] {
                        DISK1,
                        IP}, this.DISKOperationCompleted, userState);
        }
        
        private void OnDISKOperationCompleted(object arg) {
            if ((this.DISKCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DISKCompleted(this, new DISKCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Traffic", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Traffic([System.Xml.Serialization.XmlElementAttribute("Traffic")] string Traffic1, string IP) {
            object[] results = this.Invoke("Traffic", new object[] {
                        Traffic1,
                        IP});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TrafficAsync(string Traffic1, string IP) {
            this.TrafficAsync(Traffic1, IP, null);
        }
        
        /// <remarks/>
        public void TrafficAsync(string Traffic1, string IP, object userState) {
            if ((this.TrafficOperationCompleted == null)) {
                this.TrafficOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTrafficOperationCompleted);
            }
            this.InvokeAsync("Traffic", new object[] {
                        Traffic1,
                        IP}, this.TrafficOperationCompleted, userState);
        }
        
        private void OnTrafficOperationCompleted(object arg) {
            if ((this.TrafficCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TrafficCompleted(this, new TrafficCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ALL", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ALL([System.Xml.Serialization.XmlElementAttribute("CPU")] string CPU1, [System.Xml.Serialization.XmlElementAttribute("MEMORY")] string MEMORY1, [System.Xml.Serialization.XmlElementAttribute("DISK")] string DISK1, string IP) {
            object[] results = this.Invoke("ALL", new object[] {
                        CPU1,
                        MEMORY1,
                        DISK1,
                        IP});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ALLAsync(string CPU1, string MEMORY1, string DISK1, string IP) {
            this.ALLAsync(CPU1, MEMORY1, DISK1, IP, null);
        }
        
        /// <remarks/>
        public void ALLAsync(string CPU1, string MEMORY1, string DISK1, string IP, object userState) {
            if ((this.ALLOperationCompleted == null)) {
                this.ALLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnALLOperationCompleted);
            }
            this.InvokeAsync("ALL", new object[] {
                        CPU1,
                        MEMORY1,
                        DISK1,
                        IP}, this.ALLOperationCompleted, userState);
        }
        
        private void OnALLOperationCompleted(object arg) {
            if ((this.ALLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ALLCompleted(this, new ALLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/create_log", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string create_log(string serverip, string cpu, string memory, string traffic) {
            object[] results = this.Invoke("create_log", new object[] {
                        serverip,
                        cpu,
                        memory,
                        traffic});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void create_logAsync(string serverip, string cpu, string memory, string traffic) {
            this.create_logAsync(serverip, cpu, memory, traffic, null);
        }
        
        /// <remarks/>
        public void create_logAsync(string serverip, string cpu, string memory, string traffic, object userState) {
            if ((this.create_logOperationCompleted == null)) {
                this.create_logOperationCompleted = new System.Threading.SendOrPostCallback(this.Oncreate_logOperationCompleted);
            }
            this.InvokeAsync("create_log", new object[] {
                        serverip,
                        cpu,
                        memory,
                        traffic}, this.create_logOperationCompleted, userState);
        }
        
        private void Oncreate_logOperationCompleted(object arg) {
            if ((this.create_logCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.create_logCompleted(this, new create_logCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void LOGINCompletedEventHandler(object sender, LOGINCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LOGINCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LOGINCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void COMPUTERNAMECompletedEventHandler(object sender, COMPUTERNAMECompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class COMPUTERNAMECompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal COMPUTERNAMECompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void OSCompletedEventHandler(object sender, OSCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class OSCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal OSCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void CPUCompletedEventHandler(object sender, CPUCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CPUCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CPUCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void MEMORYCompletedEventHandler(object sender, MEMORYCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MEMORYCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal MEMORYCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void DISKCompletedEventHandler(object sender, DISKCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DISKCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DISKCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void TrafficCompletedEventHandler(object sender, TrafficCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TrafficCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TrafficCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void ALLCompletedEventHandler(object sender, ALLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ALLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ALLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void create_logCompletedEventHandler(object sender, create_logCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class create_logCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal create_logCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591