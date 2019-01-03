using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.VendorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;

namespace KiewitTeamBinder.UI.Pages.VendorDataModule
{
    public class ProcessDocumentSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string DefaultFilterAtHoldingAreaPane = "New Documents";
        public string GridViewHoldingAreaName = "GridViewHoldingArea"; 
        public string GridViewDocumentName = "GridViewDocReg";
        public string WindowTitle = "Process Documents into Document Register";
        public string[] listHeader = { "Document No.", "Title", "Rev", "Sts", "Discipline", "Category" };
        public string MessageOnValidateDocumentsDialog = "Document details are successfully validated.";
        public string ProcessMessage = "Processing Hold Documents in progress";
        public string ReceivedDate = DateTime.Now.AddDays(-7).ToString("MM/dd/yyyy");
        public string MessageOnProcessSaveDocumentsDialog = "Document details saved successfully.";
        public string MessageOnSaveDocumentsDialog = "Document details saved successfully.Do you want to start document review with the saved documents?";
        public string ColumnNameFilter = "Document No.";                                         
        public string IndexOptionFilterInDocument = "1";
        public string AcceptedOptionFilterInHoldingArea = "Accepted";

        public ColumnValuesInCondition ColumnValuesInConditionList = new ColumnValuesInCondition();

        public class ColumnValuesInCondition
        {
            public KeyValuePair<string, string> DocumentNo = new KeyValuePair<string, string>(DocumentDetailHeader.DocumentNo.ToDescription(), new SingleDocumentInfo().DocumentNo);
        }

        public SingleDocumentInfo SingleDocInformation = new SingleDocumentInfo()
        {
            DocumentNo = Utils.GetRandomValue("DOCUMENTNO"),
            RevStatus = "01 - Rev 01",
            Status = "VSUB - Vendor Submission",
            Title = Utils.GetRandomValue("Title"),
            Category = "BM - BILL OF MATERIAL",
            Discipline = "CON - Contruction",
            Type = "SUB - Submittal"
        };
    }
}
