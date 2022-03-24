using System;

namespace Common.Data.Domain.Events.PriceWatchAnalytics
{
    public class CurrentPriceAddEvent : IEvent
    {
        public string Origin { get; private set; }
        public string Destination { get; private set; }
        public int TruckSize { get; private set; }
        public string ProductType { get; private set; }
        public decimal LoadPrice { get; private set; }
        public DateTime EffectDate { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime Created { get; private set; }
        public int OrganisationId { get; private set; }
        public Guid ChangedBy { get; private set; }

        public CurrentPriceAddEvent(string origin, string destination, int truckSize, string productType, decimal loadPrice, DateTime effectDate, bool isActive, DateTime created, int organisationId, Guid changedBy)
        {
            Origin = origin;
            Destination = destination;
            TruckSize = truckSize;
            ProductType = productType;
            LoadPrice = loadPrice;
            EffectDate = effectDate;
            IsActive = isActive;
            Created = created;
            OrganisationId = organisationId;
            ChangedBy = changedBy;
        }
    }
}
