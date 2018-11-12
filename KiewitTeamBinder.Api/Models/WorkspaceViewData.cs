using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.API.Models
{
    public class Workspace
    {
        public string CostItemID { get; set; }
        public int CBSLevel { get; set; }
        public bool IsTerminal { get; set; }
        public bool IsHidden { get; set; }
        public bool IsCollapsed { get; set; }
        public string ParentId { get; set; }
        public string UserId { get; set; }
        public string CBSPosition { get; set; }
        public bool IsForeCastMethodDisable { get; set; }
        public string CostSource { get; set; }
        public string Currency { get; set; }
        public int ChangeOrderCount { get; set; }
        public string AccountCode { get; set; }
        public string RiskLevel { get; set; }
        public string ForeCastMethod_T { get; set; }
        public string CEConstEqpTotalCost { get; set; }
        public string Rework { get; set; }
        public string Phase { get; set; }
        public string WorkType { get; set; }
        public string A_Description { get; set; }
        public string Area { get; set; }
        public string UserDefined6 { get; set; }
        public string A_CostItemID { get; set; }
        public string DistrictSpecificTag16 { get; set; }
        public string CEUnitOfMeasure_T { get; set; }
        public int AllowAsBuilt { get; set; }
        public string DistrictSpecificTag19 { get; set; }
        public string CELaborTotalCost { get; set; }
        public string CCO { get; set; }
        public string WorkPlanId { get; set; }
        public string UserDefined4 { get; set; }
        public string RiskDescription { get; set; }
        public string A_CBSPosition { get; set; }
        public string SAPStatusControl { get; set; }
        public string ChangeStatus { get; set; }
        public string Superintendent { get; set; }
        public string DistrictSpecificUserDefined3 { get; set; }
        public string Tag18 { get; set; }
        public string EstimateTag1 { get; set; }
        public string CostItemId { get; set; }
        public string WBSClientCode1 { get; set; }
        public string UserDefined7 { get; set; }
        public string Estimator { get; set; }
        public string Tag17 { get; set; }
        public string A_ParentId { get; set; }
        public string UserDefined8 { get; set; }
        public string WBSClientCode2 { get; set; }
        public string CBSContributeQuantity_T { get; set; }
        public string ModifiedBy { get; set; }
        public string IssueNumber { get; set; }
        public string WbsPhaseCode { get; set; }
        public string D_Group { get; set; }
        public string Tag8 { get; set; }
        public string UserDefined5 { get; set; }
        public string isEnableManualEntry { get; set; }
        public string HierarchyNode { get; set; }
        public bool IsTerminal_T { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string EstimateTag2 { get; set; }
        public string A_WBSPhaseCode { get; set; }
        public bool HideinProgress { get; set; }
        public string B_ParentId { get; set; }
        public bool B_HideInProgress { get; set; }
        public string B_ChangeStatus { get; set; }
        public string B_CEUnitOfMeasure { get; set; }
        public string B_AccountCode { get; set; }
        public string B_PayItemNumber { get; set; }
        public int B_AllowAsBuilt { get; set; }
        public bool B_IsTerminal { get; set; }
        public string B_WBSPhaseCode { get; set; }
        public string CostItemId1 { get; set; }
        public string B_ParentId1 { get; set; }
        public string HierarchyNode1 { get; set; }
        public string B_SAPStatusControl { get; set; }
        public string B_CBSContributeQuantity { get; set; }
        public string B_ForecastTOQty { get; set; }
        public string B_Resource { get; set; }
        public string C_ParentId { get; set; }
        public string C_TotalCostToDate { get; set; }
        public string C_LaborCostMHToDate { get; set; }
        public string C_UnitCostToDate { get; set; }
        public string C_CBTotalCostGLToDate { get; set; }
        public string C_MHPerUnitToDate { get; set; }
        public string C_MHToDate { get; set; }
        public string C_CEPercentComplete { get; set; }
        public string C_QtyCompleteToDate { get; set; }
        public string C_UnitsPerMHToDate { get; set; }
        public string C_CBPF { get; set; }
        public string C_CBRemainingMH { get; set; }
        public string C_CBQtyRemaining { get; set; }
        public string C_CBRemainingCost { get; set; }
        public string C_CBMHGLToDate { get; set; }
        public string C_CBUnitCostGL { get; set; }
        public string C_CBUnitCostGLPercent { get; set; }
        public string C_CBTotalCostGLPercentToDate { get; set; }
        public string C_CFToDate { get; set; }
        public string D_ParentId { get; set; }
        public string D_ForecastRemainingUnitCost { get; set; }
        public string D_DeltaFromStraightLine { get; set; }
        public string D_ForecastRemainingManHour { get; set; }
        public string D_ForecastFinalManHourPerUnit { get; set; }
        public string D_ForecastFinalPF { get; set; }
        public string D_ForecastRemainingPF { get; set; }
        public string D_CEQtyRemaining { get; set; }
        public string D_CBForecastFinalMHGL { get; set; }
        public string D_CBForecastGainLoss { get; set; }
        public string D_ForecastMethodId { get; set; }
        public string D_ForecastRemainingCost { get; set; }
        public string D_ForecastRemainingManHourPerUnit { get; set; }
        public string D_ForecastFinalManHour { get; set; }
        public string D_ForecastFinalCost { get; set; }
        public string D_ForecastFinalUnitCost { get; set; }
        public string D_Notes { get; set; }
        public string D_PlugColumnName { get; set; }
    }

    public class WorkspaceViewData
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string Context { get; set; }
        [JsonProperty(PropertyName = "value")]
        public IList<Workspace> Workspaces { get; set; }
    }

}
