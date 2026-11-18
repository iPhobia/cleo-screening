public record TradeHistory
{
    public DateOnly StartDate { get; init; } // "2019-09-01"
    public Month[] Months { get; init; }
}

public record Month
{
    public int Length { get; init; } // amount of days in month
    public int FirstDay { get; init; } // day of week of month's first day (1-7)
}

public record Day
{
    public int Offset { get; init; } // offset is a position of TradingPosition of month's calendar grid relativlity to StartDate
    public TradingPosition[] Positions { get; init; } // FE can take Positions.Length to display amount according to design
    public decimal ProfitUsdt { get; init; }
    public decimal ProfitPercent { get; init; } // optionaly can be string, so FE does not need manually add '%'
}

public record TradingPosition
{
    // space for extensibility (at this point design does not require to have position details)
}