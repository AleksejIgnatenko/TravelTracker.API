﻿namespace TravelTracker.Core.Models.AdvanceReportModels
{
    public record AdvanceReportRequest(
        Guid TripCertificateId,
        string DateOfDelivery
        );
}
