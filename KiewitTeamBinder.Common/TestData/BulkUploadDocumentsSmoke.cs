using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;

namespace KiewitTeamBinder.Common.TestData
{
    public class BulkUploadDocumentsSmoke
    {
        public string ProjectName = "Automation Project 1";
        public string[] SubItemLinks = {"Holding Area"};
        public string HoldingAreaPaneName = "Holding Area -";
        public string PackageModule = "Package";
        public string PackagesNode = "Packages";
        public string GridViewHoldingAreaName = "GridViewHoldingArea";
        public string FileNames = "\"File1.txt\" \"File2.txt\" \"File3.txt\" \"File4.txt\" \"File5.txt\" \"File6.txt\" \"File7.txt\" \"File8.txt\" \"File9.txt\" \"File10.txt\" \"File11.txt\" \"File12.txt\" \"File13.txt\" \"File14.txt\" \"File15.txt\" ";
        public int numberOfUploadFiles = 15;
        public string MessageOnToNextNRowsDialog = "Apply All to next 5 rows.";
        public string DefaultFilter = "New Documents";
        public string WindowTitle = "Bulk Upload Documents";
        public string FormTitle = "Bulk Upload Documents";
        public string VersionColumn = "Version*";
        public string DataOfComboBoxRev = "00 - Rev 00";
        public string DataOfComboBoxSts = "VSUB - Vendor Submission";
        public string DataOfTitle = "Vendor Submitted Document";
        public string DataOfComboBoxDics = "CON - Contruction";
        public string DataOfComboBoxCat = "CA - CALCULATION";
        public string DataOfComboBoxType = "SUB - Submittal";
        public int NumberOfRow = 14;
        public string DocumentNoTextboxContent = Utils.GetRandomValue("AUTO");
        public string HoverCopyAttributesItem = "All";
        public string MessageOnValidateDocumentsDialog = "Document details are successfully validated.";
        public string MessageOnSaveDocumentsDialog = "Document details saved successfully.Do you want to upload more documents?";
        public string HoldingAreaFilterByColumn = "Document No.";
        public string FilterWithValue = " 1";
        public string MessageOnValidationDiaglog = "Validating Documents in progress";


    }

}
