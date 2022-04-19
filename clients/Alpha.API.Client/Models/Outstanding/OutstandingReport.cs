namespace Alpha.API.Client.Models.Outstanding;

public record OutstandingReport(IEnumerable<OutstandingEntity> Outstandings, OutstandingEntity Total);
