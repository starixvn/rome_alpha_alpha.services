namespace Alpha.Reports.Models.WinLoss;

public record DailyEnteredSport
{
    public bool IsToday { get; init; }

    public DateTime EventDate { get; init; }

    public int SportType { get; init; }

    public int Entered { get; init; }

    public int NonEntered { get; init; }
}