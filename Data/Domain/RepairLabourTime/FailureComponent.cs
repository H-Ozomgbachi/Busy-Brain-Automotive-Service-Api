using System;

namespace Common.Data.Domain.RepairLabourTime
{
    public class FailureComponent : AggregateRoot
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string AssemblyOrSystemName { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid ModifiedBy { get; private set; }

        public FailureComponent(int id, string title, string assemblyOrSystemName, DateTime createdAt, Guid modifiedBy)
        {
            Id = id;
            Title = title;
            AssemblyOrSystemName = assemblyOrSystemName;
            CreatedAt = createdAt;
            ModifiedBy = modifiedBy;
        }

        public FailureComponent(string title, string assemblyOrSystemName, DateTime createdAt, Guid modifiedBy)
        {
            Title = title;
            AssemblyOrSystemName = assemblyOrSystemName;
            CreatedAt = createdAt;
            ModifiedBy = modifiedBy;
        }
    }
}
