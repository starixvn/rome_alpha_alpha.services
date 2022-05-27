namespace Alpha.Reports.Models.Outstanding;

public record CustomerOutstanding(
    decimal SuperOutstanding,
    decimal MasterOutstanding,
    decimal AgentOutstanding,
    decimal MemberOutstanding
);