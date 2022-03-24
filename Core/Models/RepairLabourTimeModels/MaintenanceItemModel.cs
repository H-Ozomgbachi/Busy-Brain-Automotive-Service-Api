namespace Common.Core.Models.RepairLabourTimeModels
{
    public class MaintenanceItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int LabourTimeHours { get; set; }
        public string TruckModel { get; set; }
        public decimal CostPerHour { get; set; }
        public int FailureComponentId { get; set; }
    }
}