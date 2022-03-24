using System;

namespace Common.Data.Domain.RepairLabourTime
{
    public class MaintenanceItem : AggregateRoot
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Code { get; private set; }
        public int LabourTimeHours { get; private set; }
        public string TruckModel { get; private set; }
        public decimal CostPerHour { get; private set; }
        public int FailureComponentId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ModifiedAt { get; private set; }
        public Guid ModifiedBy { get; private set; }

        public MaintenanceItem(int id, string title, string code, int labourTimeHours, string truckModel, decimal costPerHour, int failureComponentId, DateTime createdAt, DateTime modifiedAt, Guid modifiedBy)
        {
            Id = id;
            Title = title;
            Code = code;
            LabourTimeHours = labourTimeHours;
            TruckModel = truckModel;
            CostPerHour = costPerHour;
            FailureComponentId = failureComponentId;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            ModifiedBy = modifiedBy;
        }

        public MaintenanceItem(string title, string code, int labourTimeHours, string truckModel, decimal costPerHour, int failureComponentId, DateTime createdAt, DateTime modifiedAt, Guid modifiedBy)
        {
            Title = title;
            Code = code;
            LabourTimeHours = labourTimeHours;
            TruckModel = truckModel;
            CostPerHour = costPerHour;
            FailureComponentId = failureComponentId;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            ModifiedBy = modifiedBy;
        }
    }
}
