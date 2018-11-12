using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.API.Models
{
    public class CostItem
    {
        public int CostItemId { get; set; }
        public int ProjectId { get; set; }
        public object AccountCodeId { get; set; }
        public int AllowAsBuilt { get; set; }
        public int ParentId { get; set; }
        public string CBSPosition { get; set; }
        public string WBSPhaseCode { get; set; }
        public object OptionalCode { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public bool IsTerminal { get; set; }
        public bool Suspend { get; set; }
        public bool Adjustment { get; set; }
        public bool Allocated { get; set; }
        public object AllocationSource { get; set; }
        public object Area { get; set; }
        public int AssemblyQuantity { get; set; }
        public int CostSource { get; set; }
        public int CostSegment { get; set; }
        public object DataSource { get; set; }
        public object D_Group { get; set; }
        public object DistrictSpecificTag16 { get; set; }
        public object DistrictSpecificTag19 { get; set; }
        public object Tag20 { get; set; }
        public object DistrictSpecificUserDefined3 { get; set; }
        public object EstimateTag1 { get; set; }
        public object EstimateTag2 { get; set; }
        public object Estimator { get; set; }
        public object ExpressProductionAs { get; set; }
        public object HasAsBuiltEntries { get; set; }
        public bool HasPeriodResources { get; set; }
        public bool HideInProgress { get; set; }
        public object MinorityPercentage { get; set; }
        public object PayItemId { get; set; }
        public object Phase { get; set; }
        public bool QuantityCheck { get; set; }
        public int QuantityDriverId { get; set; }
        public bool QuantityWarning { get; set; }
        public object QuoteGroup { get; set; }
        public object ReferenceNumber { get; set; }
        public object Rework { get; set; }
        public object RiskDescription { get; set; }
        public object RiskLevel { get; set; }
        public object SAPStatusControl { get; set; }
        public object Superintendent { get; set; }
        public object Tag17 { get; set; }
        public object Tag18 { get; set; }
        public object Tag21 { get; set; }
        public object Tag22 { get; set; }
        public object Tag23 { get; set; }
        public object Tag24 { get; set; }
        public object Tag25 { get; set; }
        public object Tag8 { get; set; }
        public object UserDefined11 { get; set; }
        public object UserDefined12 { get; set; }
        public object UserDefined13 { get; set; }
        public object UserDefined14 { get; set; }
        public object UserDefined15 { get; set; }
        public object UserDefined4 { get; set; }
        public object UserDefined5 { get; set; }
        public object UserDefined6 { get; set; }
        public object UserDefined7 { get; set; }
        public object UserDefined8 { get; set; }
        public object WBSClientCode1 { get; set; }
        public object WBSClientCode2 { get; set; }
        public object WorkPlanId { get; set; }
        public object WorkType { get; set; }
        public object CBSContributeQuantity { get; set; }
        public object HideInSRM { get; set; }
        public object Engineer { get; set; }
        public object HideInConcur { get; set; }
        public object ScheduleId { get; set; }
        public object ProgressActualStartDate { get; set; }
        public object CETotalDays { get; set; }
        public object DaysComplete { get; set; }
        public object PlannedFinish { get; set; }
        public object PlannedStart { get; set; }
        public object WorkHoursPerDayHD { get; set; }
        public object WorkHoursPerDaySchedule { get; set; }
        public object CostCurve { get; set; }
        public object DaysDurationDriven { get; set; }
        public object DaysNonDurationDriven { get; set; }
        public object DaysTotal { get; set; }
        public object DaysPerWeek { get; set; }
        public object PeriodCountMismatch { get; set; }
        public object WorkHoursPerShift { get; set; }
        public int AccountId { get; set; }
        public object SourceSystemId { get; set; }
        public object SourceSystemName { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public object PlannedQuantity { get; set; }
        public object IssueNumber { get; set; }
        public object CCO { get; set; }
        public object RollupCompare { get; set; }
        public object TrackingCode { get; set; }
        public object HideInPOD { get; set; }
        public int ExternalSystemConfirmation { get; set; }
        public bool ContributePrimaryToPrimaryQuantity { get; set; }
        public bool ContributePrimaryToSecondaryQuantity { get; set; }
        public bool ContributeSecondaryToSecondaryQuantity { get; set; }
        public object Resource { get; set; }
        public object Notes { get; set; }
        public object IsSync { get; set; }
        public bool IsBudgetLocked { get; set; }
        public object Segment1 { get; set; }
        public object Segment2 { get; set; }
        public object Segment3 { get; set; }
        public object Segment4 { get; set; }
    }

    public class CurrentEstimateSummary
    {
        public int CostItemId { get; set; }
        public int CEEqpHoursDurationDriven { get; set; }
        public int CEEqpHoursNonDurationDriven { get; set; }
        public int CEMHDurationDriven { get; set; }
        public int CEMHNonDurationDriven { get; set; }
        public int CEMHMaintenance { get; set; }
        public int CEShiftsDurationDriven { get; set; }
        public int CEShiftsNonDurationDriven { get; set; }
        public int CETotalEqpHours { get; set; }
        public object CEFinalMH { get; set; }
        public int CEShiftsTotal { get; set; }
        public int CEFinalCost { get; set; }
        public int CEConstEqpTotalCost { get; set; }
        public int CEContingencyAllowancesTotalCost { get; set; }
        public int CEFOMRentedEquipmentTotalCost { get; set; }
        public int CEGATotalCost { get; set; }
        public int CELaborTotalCost { get; set; }
        public int CEMaterialsTotalCost { get; set; }
        public int CESubcontractTotalCost { get; set; }
        public int CESuppliesTotalCost { get; set; }
        public int CEUndefinedTotalCost { get; set; }
        public int CEFeesServicesTotalCost { get; set; }
        public string CECurrency { get; set; }
        public int SecondaryQuantity { get; set; }
        public int CEUnitOfMeasure { get; set; }
        public int SecondaryUnitOfMeasure { get; set; }
        public int ForecastTOQty { get; set; }
        public int CEPercentComplete { get; set; }
        public int CEUnitCostToDate { get; set; }
        public int CEContingencyAllowancesUnitCost { get; set; }
        public int CELaborUnitCost { get; set; }
        public int CEMaterialsUnitCost { get; set; }
        public int CEFOMRentedEquipmentUnitCost { get; set; }
        public int CESubcontractUnitCost { get; set; }
        public int CESuppliesUnitCost { get; set; }
        public int CEFeesServicesUnitCost { get; set; }
        public int CEUndefinedUnitCost { get; set; }
        public int CEConstEqpUnitCost { get; set; }
        public int CEGAUnitCost { get; set; }
        public int CEEqpHrsToDate { get; set; }
        public int CEUnitCostGL { get; set; }
        public int CEEqpHrGLTotal { get; set; }
        public int PercentOfCEEqpHrsExpended { get; set; }
        public int CEEqpHoursEarned { get; set; }
        public int CERemainingMH { get; set; }
        public int CEMHExpendedPercent { get; set; }
        public int CERemainingEqpHour { get; set; }
        public int CERemainingCost { get; set; }
        public int CEFinalUnitCost { get; set; }
        public int CEMHEarnedToDate { get; set; }
        public int CECostEarnedToDate { get; set; }
        public int CEEqpHrsPerUnit { get; set; }
        public int CEMHPerUnit { get; set; }
        public int CEShiftsPerUnit { get; set; }
        public int CEUnitsPerShift { get; set; }
        public int CEUnitsPerEqpHr { get; set; }
        public int CEUnitsPerMH { get; set; }
        public int CEQtyRemaining { get; set; }
        public int CEUnitCostGLPercent { get; set; }
        public int CEEqpHrGLPercentTotal { get; set; }
        public int CECostExpendedPercent { get; set; }
        public int CECPI { get; set; }
        public int CETotalCostGLToDate { get; set; }
        public int CEPF { get; set; }
        public int CEMHGLToDate { get; set; }
        public int CEManHourBurnRate { get; set; }
        public int CEMHGLToDatePercent { get; set; }
        public int CECostBurnRate { get; set; }
        public int CETotalCostGLPercentToDate { get; set; }
        public int CERemainingTotalCostPercent { get; set; }
        public int CEEqpHrGLPercentToDate { get; set; }
        public int CELaborCostPerManHour { get; set; }
        public int CEEqpCostPerHour { get; set; }
    }

    public class CostItemMessage
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string Context { get; set; }
        public int CostItemId { get; set; }
        public string IsEnableManualEntry { get; set; }
        public CostItem CostItem { get; set; }
        public IList<CurrentEstimateSummary> CurrentEstimateSummaries { get; set; }
    }

    public class CostItemAutoSave
    {
        public IList<int> costItemIds { get; set; }
        public int projectId { get; set; }
        public string columnName { get; set; }
        public string currentValue { get; set; }
        public int userId { get; set; }
    }

    public class CostItemUpdateAccountCode
    {
        public IList<int> costItemIds { get; set; }
        public int accountCodeId { get; set; }
    }
}
