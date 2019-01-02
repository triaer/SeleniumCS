﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KiewitTeamBinder.Api.DocumentServiceReference {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.qa-software.com/", ConfigurationName="DocumentServiceReference.DocumentWebServiceSoap")]
    public interface DocumentWebServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListDocument", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable ListDocument(string sessionKey, string documentFilter, string orderBy, int startRowPosition, int noOfRows);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListDocument", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> ListDocumentAsync(string sessionKey, string documentFilter, string orderBy, int startRowPosition, int noOfRows);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListDocumentsAll", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable ListDocumentsAll(string sessionKey, string documentFilter, string registerView, string orderBy, int startRowPosition, int noOfRows);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListDocumentsAll", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> ListDocumentsAllAsync(string sessionKey, string documentFilter, string registerView, string orderBy, int startRowPosition, int noOfRows);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetDocumentDetails", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetDocumentDetails(string sessionKey, int documentKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetDocumentDetails", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetDocumentDetailsAsync(string sessionKey, int documentKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetDownloadLink", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetDownloadLink(string sessionKey, int int_viewFile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetDownloadLink", ReplyAction="*")]
        System.Threading.Tasks.Task<string> GetDownloadLinkAsync(string sessionKey, int int_viewFile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/SaveDocument", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet SaveDocument(string sessionKey, string documentDetailsXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/SaveDocument", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> SaveDocumentAsync(string sessionKey, string documentDetailsXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListDeletedDocuments", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable ListDeletedDocuments(string sessionKey, string documentFilter, string orderBy, int startRowPosition, int noOfRows);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListDeletedDocuments", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> ListDeletedDocumentsAsync(string sessionKey, string documentFilter, string orderBy, int startRowPosition, int noOfRows);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListDocumentsForAllProjects", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable ListDocumentsForAllProjects(string sessionKey, string documentFilter, string registerView, string orderBy, int startRowPosition, int noOfRows);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListDocumentsForAllProjects", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> ListDocumentsForAllProjectsAsync(string sessionKey, string documentFilter, string registerView, string orderBy, int startRowPosition, int noOfRows);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListDocumentsForAllProjectsAsynchronous", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ListDocumentsForAllProjectsAsynchronous(string sessionKey, string filter, string registerView, string orderBy, int startRowPosition, int noOfRows);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/ListDocumentsForAllProjectsAsynchronous", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ListDocumentsForAllProjectsAsynchronousAsync(string sessionKey, string filter, string registerView, string orderBy, int startRowPosition, int noOfRows);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetDocumentDetailsFromProject", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetDocumentDetailsFromProject(string sessionKey, int documentKey, int masterProjectKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetDocumentDetailsFromProject", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetDocumentDetailsFromProjectAsync(string sessionKey, int documentKey, int masterProjectKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetDownloadLinkFromProject", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string GetDownloadLinkFromProject(string sessionKey, int int_viewFile, int masterProjectKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetDownloadLinkFromProject", ReplyAction="*")]
        System.Threading.Tasks.Task<string> GetDownloadLinkFromProjectAsync(string sessionKey, int int_viewFile, int masterProjectKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetDocumentReviewStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetDocumentReviewStatus(string sessionKey, int documentKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.qa-software.com/GetDocumentReviewStatus", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetDocumentReviewStatusAsync(string sessionKey, int documentKey);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DocumentWebServiceSoapChannel : KiewitTeamBinder.Api.DocumentServiceReference.DocumentWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DocumentWebServiceSoapClient : System.ServiceModel.ClientBase<KiewitTeamBinder.Api.DocumentServiceReference.DocumentWebServiceSoap>, KiewitTeamBinder.Api.DocumentServiceReference.DocumentWebServiceSoap {
        
        public DocumentWebServiceSoapClient() {
        }
        
        public DocumentWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DocumentWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DocumentWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DocumentWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataTable ListDocument(string sessionKey, string documentFilter, string orderBy, int startRowPosition, int noOfRows) {
            return base.Channel.ListDocument(sessionKey, documentFilter, orderBy, startRowPosition, noOfRows);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> ListDocumentAsync(string sessionKey, string documentFilter, string orderBy, int startRowPosition, int noOfRows) {
            return base.Channel.ListDocumentAsync(sessionKey, documentFilter, orderBy, startRowPosition, noOfRows);
        }
        
        public System.Data.DataTable ListDocumentsAll(string sessionKey, string documentFilter, string registerView, string orderBy, int startRowPosition, int noOfRows) {
            return base.Channel.ListDocumentsAll(sessionKey, documentFilter, registerView, orderBy, startRowPosition, noOfRows);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> ListDocumentsAllAsync(string sessionKey, string documentFilter, string registerView, string orderBy, int startRowPosition, int noOfRows) {
            return base.Channel.ListDocumentsAllAsync(sessionKey, documentFilter, registerView, orderBy, startRowPosition, noOfRows);
        }
        
        public System.Data.DataSet GetDocumentDetails(string sessionKey, int documentKey) {
            return base.Channel.GetDocumentDetails(sessionKey, documentKey);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetDocumentDetailsAsync(string sessionKey, int documentKey) {
            return base.Channel.GetDocumentDetailsAsync(sessionKey, documentKey);
        }
        
        public string GetDownloadLink(string sessionKey, int int_viewFile) {
            return base.Channel.GetDownloadLink(sessionKey, int_viewFile);
        }
        
        public System.Threading.Tasks.Task<string> GetDownloadLinkAsync(string sessionKey, int int_viewFile) {
            return base.Channel.GetDownloadLinkAsync(sessionKey, int_viewFile);
        }
        
        public System.Data.DataSet SaveDocument(string sessionKey, string documentDetailsXml) {
            return base.Channel.SaveDocument(sessionKey, documentDetailsXml);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> SaveDocumentAsync(string sessionKey, string documentDetailsXml) {
            return base.Channel.SaveDocumentAsync(sessionKey, documentDetailsXml);
        }
        
        public System.Data.DataTable ListDeletedDocuments(string sessionKey, string documentFilter, string orderBy, int startRowPosition, int noOfRows) {
            return base.Channel.ListDeletedDocuments(sessionKey, documentFilter, orderBy, startRowPosition, noOfRows);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> ListDeletedDocumentsAsync(string sessionKey, string documentFilter, string orderBy, int startRowPosition, int noOfRows) {
            return base.Channel.ListDeletedDocumentsAsync(sessionKey, documentFilter, orderBy, startRowPosition, noOfRows);
        }
        
        public System.Data.DataTable ListDocumentsForAllProjects(string sessionKey, string documentFilter, string registerView, string orderBy, int startRowPosition, int noOfRows) {
            return base.Channel.ListDocumentsForAllProjects(sessionKey, documentFilter, registerView, orderBy, startRowPosition, noOfRows);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> ListDocumentsForAllProjectsAsync(string sessionKey, string documentFilter, string registerView, string orderBy, int startRowPosition, int noOfRows) {
            return base.Channel.ListDocumentsForAllProjectsAsync(sessionKey, documentFilter, registerView, orderBy, startRowPosition, noOfRows);
        }
        
        public string ListDocumentsForAllProjectsAsynchronous(string sessionKey, string filter, string registerView, string orderBy, int startRowPosition, int noOfRows) {
            return base.Channel.ListDocumentsForAllProjectsAsynchronous(sessionKey, filter, registerView, orderBy, startRowPosition, noOfRows);
        }
        
        public System.Threading.Tasks.Task<string> ListDocumentsForAllProjectsAsynchronousAsync(string sessionKey, string filter, string registerView, string orderBy, int startRowPosition, int noOfRows) {
            return base.Channel.ListDocumentsForAllProjectsAsynchronousAsync(sessionKey, filter, registerView, orderBy, startRowPosition, noOfRows);
        }
        
        public System.Data.DataSet GetDocumentDetailsFromProject(string sessionKey, int documentKey, int masterProjectKey) {
            return base.Channel.GetDocumentDetailsFromProject(sessionKey, documentKey, masterProjectKey);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetDocumentDetailsFromProjectAsync(string sessionKey, int documentKey, int masterProjectKey) {
            return base.Channel.GetDocumentDetailsFromProjectAsync(sessionKey, documentKey, masterProjectKey);
        }
        
        public string GetDownloadLinkFromProject(string sessionKey, int int_viewFile, int masterProjectKey) {
            return base.Channel.GetDownloadLinkFromProject(sessionKey, int_viewFile, masterProjectKey);
        }
        
        public System.Threading.Tasks.Task<string> GetDownloadLinkFromProjectAsync(string sessionKey, int int_viewFile, int masterProjectKey) {
            return base.Channel.GetDownloadLinkFromProjectAsync(sessionKey, int_viewFile, masterProjectKey);
        }
        
        public System.Data.DataSet GetDocumentReviewStatus(string sessionKey, int documentKey) {
            return base.Channel.GetDocumentReviewStatus(sessionKey, documentKey);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetDocumentReviewStatusAsync(string sessionKey, int documentKey) {
            return base.Channel.GetDocumentReviewStatusAsync(sessionKey, documentKey);
        }
    }
}
