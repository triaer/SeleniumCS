﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agoda.Api.SessionServiceReference {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.qa-software.com/", ConfigurationName="SessionServiceReference.SessionWebServiceSoap")]
    public interface SessionWebServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/Logon", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string Logon(string userID, string companyID, string password, string projNo, string connectingProduct);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/Logon", ReplyAction="*")]
        System.Threading.Tasks.Task<string> LogonAsync(string userID, string companyID, string password, string projNo, string connectingProduct);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/LogonWithApplication", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string LogonWithApplication(string userID, string companyID, string password, string projNo, string connectingProduct);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/LogonWithApplication", ReplyAction="*")]
        System.Threading.Tasks.Task<string> LogonWithApplicationAsync(string userID, string companyID, string password, string projNo, string connectingProduct);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/LogOff", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void LogOff(string sessionKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/LogOff", ReplyAction="*")]
        System.Threading.Tasks.Task LogOffAsync(string sessionKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/LogoffStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string LogoffStatus(string sessionKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/LogoffStatus", ReplyAction="*")]
        System.Threading.Tasks.Task<string> LogoffStatusAsync(string sessionKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetUserProjectsList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetUserProjectsList(string userID, string companyID, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetUserProjectsList", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetUserProjectsListAsync(string userID, string companyID, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/IsValidSession", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string IsValidSession(string sessionKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/IsValidSession", ReplyAction="*")]
        System.Threading.Tasks.Task<string> IsValidSessionAsync(string sessionKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/CheckAndGetSessionKey", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string CheckAndGetSessionKey(string sessionKey, int projMasterKey, int projectKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/CheckAndGetSessionKey", ReplyAction="*")]
        System.Threading.Tasks.Task<string> CheckAndGetSessionKeyAsync(string sessionKey, int projMasterKey, int projectKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/AutoLogin", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string AutoLogin(string guid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/AutoLogin", ReplyAction="*")]
        System.Threading.Tasks.Task<string> AutoLoginAsync(string guid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/AutoLoginWithApplication", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string AutoLoginWithApplication(string guid, string connectingProduct, string loginMethod);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/AutoLoginWithApplication", ReplyAction="*")]
        System.Threading.Tasks.Task<string> AutoLoginWithApplicationAsync(string guid, string connectingProduct, string loginMethod);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetSessionInformation", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetSessionInformation(string sessionKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetSessionInformation", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetSessionInformationAsync(string sessionKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListLogonHistory", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable ListLogonHistory(string sessionKey, int targetUserKey, string filter, string orderBy);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListLogonHistory", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> ListLogonHistoryAsync(string sessionKey, int targetUserKey, string filter, string orderBy);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SessionWebServiceSoapChannel : Agoda.Api.SessionServiceReference.SessionWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SessionWebServiceSoapClient : System.ServiceModel.ClientBase<Agoda.Api.SessionServiceReference.SessionWebServiceSoap>, Agoda.Api.SessionServiceReference.SessionWebServiceSoap {
        
        public SessionWebServiceSoapClient() {
        }
        
        public SessionWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SessionWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SessionWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SessionWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Logon(string userID, string companyID, string password, string projNo, string connectingProduct) {
            return base.Channel.Logon(userID, companyID, password, projNo, connectingProduct);
        }
        
        public System.Threading.Tasks.Task<string> LogonAsync(string userID, string companyID, string password, string projNo, string connectingProduct) {
            return base.Channel.LogonAsync(userID, companyID, password, projNo, connectingProduct);
        }
        
        public string LogonWithApplication(string userID, string companyID, string password, string projNo, string connectingProduct) {
            return base.Channel.LogonWithApplication(userID, companyID, password, projNo, connectingProduct);
        }
        
        public System.Threading.Tasks.Task<string> LogonWithApplicationAsync(string userID, string companyID, string password, string projNo, string connectingProduct) {
            return base.Channel.LogonWithApplicationAsync(userID, companyID, password, projNo, connectingProduct);
        }
        
        public void LogOff(string sessionKey) {
            base.Channel.LogOff(sessionKey);
        }
        
        public System.Threading.Tasks.Task LogOffAsync(string sessionKey) {
            return base.Channel.LogOffAsync(sessionKey);
        }
        
        public string LogoffStatus(string sessionKey) {
            return base.Channel.LogoffStatus(sessionKey);
        }
        
        public System.Threading.Tasks.Task<string> LogoffStatusAsync(string sessionKey) {
            return base.Channel.LogoffStatusAsync(sessionKey);
        }
        
        public System.Data.DataTable GetUserProjectsList(string userID, string companyID, string password) {
            return base.Channel.GetUserProjectsList(userID, companyID, password);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetUserProjectsListAsync(string userID, string companyID, string password) {
            return base.Channel.GetUserProjectsListAsync(userID, companyID, password);
        }
        
        public string IsValidSession(string sessionKey) {
            return base.Channel.IsValidSession(sessionKey);
        }
        
        public System.Threading.Tasks.Task<string> IsValidSessionAsync(string sessionKey) {
            return base.Channel.IsValidSessionAsync(sessionKey);
        }
        
        public string CheckAndGetSessionKey(string sessionKey, int projMasterKey, int projectKey) {
            return base.Channel.CheckAndGetSessionKey(sessionKey, projMasterKey, projectKey);
        }
        
        public System.Threading.Tasks.Task<string> CheckAndGetSessionKeyAsync(string sessionKey, int projMasterKey, int projectKey) {
            return base.Channel.CheckAndGetSessionKeyAsync(sessionKey, projMasterKey, projectKey);
        }
        
        public string AutoLogin(string guid) {
            return base.Channel.AutoLogin(guid);
        }
        
        public System.Threading.Tasks.Task<string> AutoLoginAsync(string guid) {
            return base.Channel.AutoLoginAsync(guid);
        }
        
        public string AutoLoginWithApplication(string guid, string connectingProduct, string loginMethod) {
            return base.Channel.AutoLoginWithApplication(guid, connectingProduct, loginMethod);
        }
        
        public System.Threading.Tasks.Task<string> AutoLoginWithApplicationAsync(string guid, string connectingProduct, string loginMethod) {
            return base.Channel.AutoLoginWithApplicationAsync(guid, connectingProduct, loginMethod);
        }
        
        public System.Data.DataTable GetSessionInformation(string sessionKey) {
            return base.Channel.GetSessionInformation(sessionKey);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetSessionInformationAsync(string sessionKey) {
            return base.Channel.GetSessionInformationAsync(sessionKey);
        }
        
        public System.Data.DataTable ListLogonHistory(string sessionKey, int targetUserKey, string filter, string orderBy) {
            return base.Channel.ListLogonHistory(sessionKey, targetUserKey, filter, orderBy);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> ListLogonHistoryAsync(string sessionKey, int targetUserKey, string filter, string orderBy) {
            return base.Channel.ListLogonHistoryAsync(sessionKey, targetUserKey, filter, orderBy);
        }
    }
}
