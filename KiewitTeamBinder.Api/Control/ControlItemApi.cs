using KiewitTeamBinder.API.Models;
using KiewitTeamBinder.Common.Models.Control;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KiewitTeamBinder.API.Control
{
    public class ControlItemApi : ApiBase
    {
        public CostItem CreateWbsCode(string accessToken, string baseUrl, string userConnectionId, string projectName, string cbsTaskDescription, WBSCostItemInput costItemInput, WBSCostItemOutput wbsCostItemOutput)
        {
            #region "Get ProjectId"
            // Get ProjectId
            // GET https://kwt-int-182.hds.ineight.com/CoreWebServices/odata/GetProjectsLocalized(OrganizationId=null)?$select=ProjectId,OrganizationId,ProjectDisplay,ProjectName,SourceSystemId,CreatedDate,ModifiedDate,City,CountryISOCode,RegionISOCode,ZipCode,Address1,Address2,Latitude,Longitude,Notes,StartDate,EndDate,ProjectStatusName,%20OrganizationPath,%20CreatedByFullName,%20ModifiedByFullName,%20TimeZoneName&%24format=json&%24top=50&%24filter=((contains(ProjectDisplay%2C%27automation+testing+only+1%27)+or+contains(ProjectName%2C%27automation+testing+only+1%27)+or+contains(ProjectStatusName%2C%27automation+testing+only+1%27)+or+contains(OrganizationPath%2C%27automation+testing+only+1%27)+or+contains(CreatedByFullName%2C%27automation+testing+only+1%27))+and+LanguageId+eq+1)&%24count=true HTTP/1.1
            // Response: 
            // {
            //    "@odata.context":"https://kwt-int-182.hds.ineight.com/CoreWebServices/odata/$metadata#ProjectLocalized(ProjectId,OrganizationId,ProjectDisplay,ProjectName,SourceSystemId,CreatedDate,ModifiedDate,City,CountryISOCode,RegionISOCode,ZipCode,Address1,Address2,Latitude,Longitude,Notes,StartDate,EndDate,ProjectStatusName,OrganizationPath,CreatedByFullName,ModifiedByFullName,TimeZoneName)","@odata.count":1,"value":[
            //      {
            //        "ProjectId":176,"OrganizationId":36,"ProjectDisplay":"109001","ProjectName":"Automation Testing Only 1","SourceSystemId":"109001","CreatedDate":"2018-04-30T19:01:16.5867363Z","ModifiedDate":"2018-04-30T21:14:50.7709693Z","City":"Omaha","CountryISOCode":"US","RegionISOCode":"US-NE","ZipCode":null,"Address1":null,"Address2":null,"Latitude":null,"Longitude":null,"Notes":null,"StartDate":"2017-03-04T00:00:00Z","EndDate":"2022-08-31T00:00:00Z","ProjectStatusName":"Active","OrganizationPath":"S100000 - PKS Inc : SA1000 - Kiewit Corporation : SB3000 - Infrastructure : SC3003 - Building : SE1000 - Kiewit Building Group","CreatedByFullName":"Vanessa Fichter","ModifiedByFullName":"Vanessa Fichter","TimeZoneName":"(UTC-06:00) Central Time (US & Canada)"
            //      }
            //    ]
            //  }
            #endregion
            string projectNameEncoded = HttpUtility.UrlEncode(projectName); // "automation+testing+only+1";
            string operationPath = "CoreWebServices/odata/GetProjectsLocalized(OrganizationId=null)?$select=ProjectId,OrganizationId,ProjectDisplay,ProjectName,SourceSystemId,CreatedDate,ModifiedDate,City,CountryISOCode,RegionISOCode,ZipCode,Address1,Address2,Latitude,Longitude,Notes,StartDate,EndDate,ProjectStatusName,%20OrganizationPath,%20CreatedByFullName,%20ModifiedByFullName,%20TimeZoneName&%24format=json&%24top=50&%24filter=((contains(ProjectDisplay%2C%27" + projectNameEncoded + "%27)+or+contains(ProjectName%2C%27" + projectNameEncoded + "%27))+and+LanguageId+eq+1)&%24count=true";
            var projectsResponse = SendHttpCall<ProjectsResponse>(accessToken, userConnectionId, baseUrl, operationPath, Method.GET);

            int projectId = projectsResponse.Value[0].ProjectId;

            #region "Get ControlItemParentId"
            // GET https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/WorkspaceView/Default.GetWorkspaceViewData(workspaceViewParameters=@workspace,projectId=176,accountId=1,deltathresholdvalue=5)?@workspace=[{%22DataBlockId%22:1,%22ColumnPrefix%22:%22A_%22,%22FromDate%22:%222018-06-19T21:44:58.343Z%22,%22ToDate%22:%222018-06-19T21:44:58.343Z%22,%22ForecastDeltaId%22:[],%22ForecastSnapshotId%22:0},{%22DataBlockId%22:13,%22ColumnPrefix%22:%22B_%22,%22FromDate%22:%222018-06-19T21:44:58.343Z%22,%22ToDate%22:%222018-06-19T21:44:58.343Z%22,%22ForecastDeltaId%22:[],%22ForecastSnapshotId%22:0},{%22DataBlockId%22:2,%22ColumnPrefix%22:%22C_%22,%22FromDate%22:%222018-06-19T21:44:58.343Z%22,%22ToDate%22:%222018-06-19T21:44:58.343Z%22,%22ForecastDeltaId%22:[],%22ForecastSnapshotId%22:0},{%22DataBlockId%22:3,%22ColumnPrefix%22:%22D_%22,%22FromDate%22:%222018-06-19T21:44:58.343Z%22,%22ToDate%22:%222018-06-19T21:44:58.343Z%22,%22ForecastDeltaId%22:[],%22ForecastSnapshotId%22:13125}]&_=1529444694900
            #endregion
            string parentCostItemDescription = cbsTaskDescription.Split('/')[1];

            string nowFilter = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            operationPath = "ControlWebServices/odata/WorkspaceView/Default.GetWorkspaceViewData(workspaceViewParameters=@workspace,projectId=" + projectId + ",accountId =1,deltathresholdvalue=5)?@workspace=[{%22DataBlockId%22:1,%22ColumnPrefix%22:%22A_%22,%22FromDate%22:%22" + nowFilter + "%22,%22ToDate%22:%22" + nowFilter + "%22,%22ForecastDeltaId%22:[],%22ForecastSnapshotId%22:0},{%22DataBlockId%22:13,%22ColumnPrefix%22:%22B_%22,%22FromDate%22:%22" + nowFilter + "%22,%22ToDate%22:%22" + nowFilter + "%22,%22ForecastDeltaId%22:[],%22ForecastSnapshotId%22:0},{%22DataBlockId%22:2,%22ColumnPrefix%22:%22C_%22,%22FromDate%22:%22" + nowFilter + "%22,%22ToDate%22:%22" + nowFilter + "%22,%22ForecastDeltaId%22:[],%22ForecastSnapshotId%22:0},{%22DataBlockId%22:3,%22ColumnPrefix%22:%22D_%22,%22FromDate%22:%22" + nowFilter + "%22,%22ToDate%22:%22" + nowFilter + "%22,%22ForecastDeltaId%22:[],%22ForecastSnapshotId%22:13125}]&_=1529444694900";
            var workspaceResponse = SendHttpCall<WorkspaceViewData>(accessToken, userConnectionId, baseUrl, operationPath, Method.GET, projectId);

            var costItems = workspaceResponse.Workspaces.Where(c => c.A_Description == parentCostItemDescription);
            int parentCostItemId = int.Parse(costItems.FirstOrDefault().CostItemID);

            #region "Unlock Budget"
            //  https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/CostItem/Default.UnLockBudget
            #endregion
            operationPath = "ControlWebServices/odata/CostItem/Default.UnLockBudget";
            BudgetLockUnLock budgetUnlock = new BudgetLockUnLock
            {
                CostItemIds = new List<object>(),
                ProjectId = projectId,
                AccountId = 1
            };
            try
            {
                var unlockResponse = SendHttpCall<OdataResponse>(accessToken, userConnectionId, baseUrl, operationPath, Method.POST, projectId, budgetUnlock);
            }
            catch (Exception)
            {
                // do nothing
            }

            #region "Get New CBS and WBS Code"
            //// FIRST CALL
            // POST https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/CostItem/Default.GetNewCostItemCbsCodeAndWbsCode?costItemId=172324&isSubOrdinate=1&RegenarateWBSPhaseCode=0&IsRegenarateWBSPhaseCode=false HTTP/1.1
            // REQUEST BODY - NA
            // RESPONSE - { "@odata.context":"https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/$metadata#Edm.String","value":"1.25.1.30_1317" }
            #endregion
            operationPath = "ControlWebServices/odata/CostItem/Default.GetNewCostItemCbsCodeAndWbsCode?costItemId=" + parentCostItemId.ToString() + "&isSubOrdinate=1&RegenarateWBSPhaseCode=0&IsRegenarateWBSPhaseCode=false";
            var response = SendHttpCall<OdataResponse>(accessToken, userConnectionId, baseUrl, operationPath, Method.POST, projectId);

            // Get CBS Position and WBS Phase Code
            string rawResponseValue = response.Value;
            string cbsPosition = rawResponseValue.Split('_')[0];
            string wbsPhaseCode = rawResponseValue.Split('_')[1];
            wbsCostItemOutput.WBSPhaseCode = int.Parse(wbsPhaseCode);

            #region "Insert initial CostItem"
            //// SECOND CALL
            // POST https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/CustomCostItemVM/Default.InsertNewCostItem?$expand=CostItem,CurrentEstimateSummaries HTTP/1.1
            // REQUEST BODY - {"newCostItemVm":{"CostItemID":0,"SelectedCostItemId":172324,"NewCbsPosition":"1.25.1.30","NewWbsPhasecode":"1317","IsInherited":1,"IsSubOrdinate":true,"RegenarateWBSPhaseCode":"0","IsRegenarateWBSPhaseCode":false,"ProjectId":179,"AccountId":1,"Currency":"USD"}}
            // RESPONSE
            //{
            //    "@odata.context":"https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/$metadata#NewCostItemWithSummariesVM/$entity","CostItemId":174073,"IsEnableManualEntry":"QuantitiesHide","CostItem":{
            //        "CostItemId":174073,"ProjectId":179,"AccountCodeId":null,"AllowAsBuilt":1,"ParentId":172324,"CBSPosition":"1.25.1.30","WBSPhaseCode":"1317","OptionalCode":null,"Description":"Daily Planning","Currency":"USD","IsTerminal":true,"Suspend":false,"Adjustment":false,"Allocated":false,"AllocationSource":null,"Area":null,"AssemblyQuantity":0,"CostSource":3,"CostSegment":1,"DataSource":null,"D_Group":null,"DistrictSpecificTag16":null,"DistrictSpecificTag19":null,"Tag20":null,"DistrictSpecificUserDefined3":null,"EstimateTag1":null,"EstimateTag2":null,"Estimator":null,"ExpressProductionAs":null,"HasAsBuiltEntries":null,"HasPeriodResources":false,"HideInProgress":false,"MinorityPercentage":null,"PayItemId":null,"Phase":null,"QuantityCheck":false,"QuantityDriverId":1,"QuantityWarning":false,"QuoteGroup":null,"ReferenceNumber":null,"Rework":null,"RiskDescription":null,"RiskLevel":null,"SAPStatusControl":null,"Superintendent":null,"Tag17":null,"Tag18":null,"Tag21":null,"Tag22":null,"Tag23":null,"Tag24":null,"Tag25":null,"Tag8":null,"UserDefined11":null,"UserDefined12":null,"UserDefined13":null,"UserDefined14":null,"UserDefined15":null,"UserDefined4":null,"UserDefined5":null,"UserDefined6":null,"UserDefined7":null,"UserDefined8":null,"WBSClientCode1":null,"WBSClientCode2":null,"WorkPlanId":null,"WorkType":null,"CBSContributeQuantity":null,"HideInSRM":null,"Engineer":null,"HideInConcur":null,"ScheduleId":null,"ProgressActualStartDate":null,"CETotalDays":null,"DaysComplete":null,"PlannedFinish":null,"PlannedStart":null,"WorkHoursPerDayHD":null,"WorkHoursPerDaySchedule":null,"CostCurve":null,"DaysDurationDriven":null,"DaysNonDurationDriven":null,"DaysTotal":null,"DaysPerWeek":null,"PeriodCountMismatch":null,"WorkHoursPerShift":null,"AccountId":1,"SourceSystemId":null,"SourceSystemName":null,"CreatedById":38925,"CreatedDate":"2018-05-20T11:50:26.16968Z","ModifiedById":38925,"ModifiedDate":"2018-05-20T11:50:26.16968Z","IsActive":true,"PlannedQuantity":null,"IssueNumber":null,"CCO":null,"RollupCompare":null,"TrackingCode":null,"HideInPOD":null,"ExternalSystemConfirmation":1,"ContributePrimaryToPrimaryQuantity":false,"ContributePrimaryToSecondaryQuantity":false,"ContributeSecondaryToSecondaryQuantity":false,"Resource":null,"Notes":null,"IsSync":null,"IsBudgetLocked":false,"Segment1":null,"Segment2":null,"Segment3":null,"Segment4":null
            //    },"CurrentEstimateSummaries":[
            //    {
            //  "CostItemId":174073,"CEEqpHoursDurationDriven":0,"CEEqpHoursNonDurationDriven":0,"CEMHDurationDriven":0,"CEMHNonDurationDriven":0,"CEMHMaintenance":0,"CEShiftsDurationDriven":0,"CEShiftsNonDurationDriven":0,"CETotalEqpHours":0,"CEFinalMH":null,"CEShiftsTotal":0,"CEFinalCost":0,"CEConstEqpTotalCost":0,"CEContingencyAllowancesTotalCost":0,"CEFOMRentedEquipmentTotalCost":0,"CEGATotalCost":0,"CELaborTotalCost":0,"CEMaterialsTotalCost":0,"CESubcontractTotalCost":0,"CESuppliesTotalCost":0,"CEUndefinedTotalCost":0,"CEFeesServicesTotalCost":0,"CECurrency":"USD","SecondaryQuantity":0,"CEUnitOfMeasure":82,"SecondaryUnitOfMeasure":0,"ForecastTOQty":1,"CEPercentComplete":0,"CEUnitCostToDate":0,"CEContingencyAllowancesUnitCost":0,"CELaborUnitCost":0,"CEMaterialsUnitCost":0,"CEFOMRentedEquipmentUnitCost":0,"CESubcontractUnitCost":0,"CESuppliesUnitCost":0,"CEFeesServicesUnitCost":0,"CEUndefinedUnitCost":0,"CEConstEqpUnitCost":0,"CEGAUnitCost":0,"CEEqpHrsToDate":0,"CEUnitCostGL":0,"CEEqpHrGLTotal":0,"PercentOfCEEqpHrsExpended":0,"CEEqpHoursEarned":0,"CERemainingMH":0,"CEMHExpendedPercent":0,"CERemainingEqpHour":0,"CERemainingCost":0,"CEFinalUnitCost":0,"CEMHEarnedToDate":0,"CECostEarnedToDate":0,"CEEqpHrsPerUnit":0,"CEMHPerUnit":0,"CEShiftsPerUnit":0,"CEUnitsPerShift":0,"CEUnitsPerEqpHr":0,"CEUnitsPerMH":0,"CEQtyRemaining":1,"CEUnitCostGLPercent":0,"CEEqpHrGLPercentTotal":0,"CECostExpendedPercent":0,"CECPI":0,"CETotalCostGLToDate":0,"CEPF":0,"CEMHGLToDate":0,"CEManHourBurnRate":0,"CEMHGLToDatePercent":0,"CECostBurnRate":0,"CETotalCostGLPercentToDate":0,"CERemainingTotalCostPercent":0,"CEEqpHrGLPercentToDate":0,"CELaborCostPerManHour":0,"CEEqpCostPerHour":0
            //}
            #endregion
            NewCostItemVmMessage jsonCostItem = new NewCostItemVmMessage
            {
                newCostItemVm = new NewCostItemVm
                {
                    CostItemID = 0,
                    SelectedCostItemId = parentCostItemId,
                    NewCbsPosition = cbsPosition,
                    NewWbsPhasecode = wbsPhaseCode,
                    IsInherited = 1,
                    IsSubOrdinate = true,
                    RegenarateWBSPhaseCode = "0",
                    IsRegenarateWBSPhaseCode = false,
                    ProjectId = projectId,
                    AccountId = 1,
                    Currency = "USD"
                }
            };

            operationPath = "ControlWebServices/odata/CustomCostItemVM/Default.InsertNewCostItem?$expand=CostItem,CurrentEstimateSummaries";
            var costItemMessage = SendHttpCall<CostItemMessage>(accessToken, userConnectionId, baseUrl, operationPath, Method.POST, projectId, jsonCostItem);

            // Description = Time Stamp
            // Account Code = .03  279abb5b-a229-4661-ab8c-cf19c568393c
            // Forecast Quantity = 1000
            // CE Final Cost = 100000
            // CE Final MHrs = 2000
            // CE total equipment hours = 2000
            // Allow as built = All

            #region "Update Description"
            // POST https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/CostItem/Default.CostItemDetailAutoSave HTTP/1.1
            // {"costItemIds":[174073],"projectId":179,"columnName":"Description","currentValue":"Automated_WBS_20180520064936","userId":38925}
            #endregion
            CostItemAutoSave costItemAutoSave = new CostItemAutoSave
            {
                costItemIds = new List<int> { costItemMessage.CostItemId },
                projectId = projectId,
                columnName = "Description",
                currentValue = costItemInput.Description,
                userId = costItemMessage.CostItem.CreatedById
            };

            operationPath = "ControlWebServices/odata/CostItem/Default.CostItemDetailAutoSave";
            var response3 = SendHttpCall(accessToken, userConnectionId, baseUrl, operationPath, Method.POST, projectId, costItemAutoSave);

            #region "Update Account Code"
            // POST https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/CostItem/Default.UpdateAccountCode HTTP/1.1
            // {"costItemIds":[174073],"accountCodeId":2}
            #endregion
            var costItemUpdateAccountCode = new CostItemUpdateAccountCode
            {
                costItemIds = new List<int> { costItemMessage.CostItemId },
                accountCodeId = 2
            };

            operationPath = "ControlWebServices/odata/CostItem/Default.UpdateAccountCode";
            var response4 = SendHttpCall(accessToken, userConnectionId, baseUrl, operationPath, Method.POST, projectId, costItemUpdateAccountCode);

            #region "Update Forecast TO Qty"
            // POST https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/CurrentEstimateSummary/Default.UpdateForecastTOQty HTTP/1.1
            // {"ProjectId":179,"AccountId":1,"ForecastTOQtyPropVm":{"CostItemIds":[174073],"ForecastTOQtyValue":"1000","ProportionalColumnName":"CEFinalUnitCost","KeepCostAsItIs":false,"IsCEFinalMH":false,"IsCETotalEqpHours":false}}
            #endregion
            var forecastTOQtyProp = new ForecastTOQtyPropMessage
            {
                ProjectId = projectId,
                AccountId = 1,
                ForecastTOQtyPropVm = new ForecastTOQtyPropVm
                {
                    CostItemIds = new List<int> { costItemMessage.CostItemId },
                    ForecastTOQtyValue = costItemInput.ForecastQty.ToString(),
                    ProportionalColumnName = "CEFinalUnitCost",
                    KeepCostAsItIs = false,
                    IsCEFinalMH = false,
                    IsCETotalEqpHours = false
                }
            };

            operationPath = "ControlWebServices/odata/CurrentEstimateSummary/Default.UpdateForecastTOQty";
            var forecastTOQtyResponse = SendHttpCall<WorkspaceViewData>(accessToken, userConnectionId, baseUrl, operationPath, Method.POST, projectId, forecastTOQtyProp);

            #region "Save CE Values"
            // POST https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/CurrentEstimateSummary/Default.SaveCEProportionalColumns HTTP/1.1
            // {"CurrEstimateDetails":[{"CostItemIds":"174073","PlugColumn":"CEFinalMH","CETotalEqpHours":"","CEFinalMH":"1000","CEConstEqpTotalCost":"","CELaborTotalCost":"0.00000000000","CEFinalCost":"","CELaborCostPerManHour":""}],"ProjectId":179,"AccountId":1}
            #endregion

            var currentEstimateDetailMessage = new CurrEstimateDetailMessage
            {
                ProjectId = projectId,
                AccountId = 1,
                CurrEstimateDetails = new List<CurrEstimateDetail>
                {
                    new  CurrEstimateDetail
                    {
                        CostItemIds = costItemMessage.CostItemId.ToString(),
                        PlugColumn = "CEFinalMH",
                        CETotalEqpHours = costItemInput.TotalEquipmentHrs.ToString(),
                        CEFinalMH = costItemInput.FinalMHrs.ToString(),
                        CEConstEqpTotalCost = "",
                        CELaborTotalCost = "0.00000000000",
                        CEFinalCost = costItemInput.FinalCost.ToString(),
                        CELaborCostPerManHour = ""
                    }
                }
            };
            operationPath = "ControlWebServices/odata/CurrentEstimateSummary/Default.SaveCEProportionalColumns";
            var ceValueResponse = SendHttpCall(accessToken, userConnectionId, baseUrl, operationPath, Method.POST, projectId, currentEstimateDetailMessage);

            #region "Update AllowAsBuilt"
            // POST https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/CostItem/Default.CostItemDetailAutoSave HTTP/1.1
            // "costItemIds":[174073],"projectId":179,"columnName":"AllowAsBuilt","currentValue":"2","userId":38925}
            #endregion

            var allowAsBuildUpdate = new CostItemAutoSave
            {
                costItemIds = new List<int> { costItemMessage.CostItemId },
                projectId = projectId,
                columnName = "AllowAsBuilt",
                currentValue = "2", // All
                userId = costItemMessage.CostItem.CreatedById
            };
            operationPath = "ControlWebServices/odata/CostItem/Default.CostItemDetailAutoSave";
            var allowAsBuildUpdateResponse = SendHttpCall(accessToken, userConnectionId, baseUrl, operationPath, Method.POST, projectId, allowAsBuildUpdate);



            #region "Lock Budget"
            //  https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/CostItem/Default.LockBudget
            #endregion
            operationPath = "ControlWebServices/odata/CostItem/Default.LockBudget";
            BudgetLockUnLock budgetLock = new BudgetLockUnLock
            {
                CostItemIds = new List<object>(),
                ProjectId = projectId,
                AccountId = 1
            };
            var lockResponse = SendHttpCall<OdataResponse>(accessToken, userConnectionId, baseUrl, operationPath, Method.POST, projectId, budgetLock);

            #region "Get Cost Item Category"
            #endregion
            operationPath = "/ControlWebServices/odata/CostCategory/Default.GetCostItemCategoryDetails(costItemId=" + costItemMessage.CostItemId + ",Type ='Task',AccountId=1,snapShotId=0,projectId=" + projectId + ",finalcost=0)";
            var costItemCategoryResponse = SendHttpCall<CostCategoryDetailsMessage>(accessToken, userConnectionId, baseUrl, operationPath, Method.GET, projectId);

            var costItemCategory = costItemCategoryResponse.CostCategoryMessage.Where(item => item.Name == "Total");
            wbsCostItemOutput.TotalCurrentBudget = costItemCategory.FirstOrDefault().CurrentBudgetValue;
            wbsCostItemOutput.TotalCurrentEstimate = costItemCategory.FirstOrDefault().CurrentEstimateValue;
            costItemCategory = costItemCategoryResponse.CostCategoryMessage.Where(item => item.Name == "Man Hours");
            wbsCostItemOutput.MHRsCurrentBudget = costItemCategory.FirstOrDefault().CurrentBudgetValue;
            wbsCostItemOutput.MHRsCurrentEstimate = costItemCategory.FirstOrDefault().CurrentEstimateValue;
            costItemCategory = costItemCategoryResponse.CostCategoryMessage.Where(item => item.Name == "Qty");
            wbsCostItemOutput.QtyCurrentBudget = costItemCategory.FirstOrDefault().CurrentBudgetValue;
            wbsCostItemOutput.QtyCurrentEstimate = costItemCategory.FirstOrDefault().CurrentEstimateValue;
            wbsCostItemOutput.ForecastQty = costItemCategory.FirstOrDefault().ForecastValue;

            #region "Sync WBS Code to Plan"
            // Call 3
            // POST https://kwt-int-182.hds.ineight.com/ControlWebServices/odata/CostItem/Default.SyncCBSStructure HTTP/1.1
            // REQUEST BODY {"AccountId":1,"ProjectId":179}
            #endregion
            var accountProjectRequest = new AccountProjectRequest
            {
                AccountId = 1,
                ProjectId = projectId
            };

            operationPath = "ControlWebServices/odata/CostItem/Default.SyncCBSStructure";
            var response9 = SendHttpCall(accessToken, userConnectionId, baseUrl, operationPath, Method.POST, projectId, accountProjectRequest);

            return costItemMessage.CostItem;
        }
    }
}
