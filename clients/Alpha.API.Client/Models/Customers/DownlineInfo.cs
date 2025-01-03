namespace Alpha.API.Client.Models.Customers;

public record DownlineInfo(int CustId, byte RoleId, bool IsSubAcc, int AgentId);
