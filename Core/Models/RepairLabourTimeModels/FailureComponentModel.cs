using System;

namespace Common.Core.Models.RepairLabourTimeModels
{
    public class FailureComponentModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AssemblyOrSystemName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}