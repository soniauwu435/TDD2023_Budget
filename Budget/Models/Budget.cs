namespace Budget.Models;

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }
    public int DaysInMonth { get; set; }

    public decimal GetAmountByDays(int days)
    {
        return Amount / DaysInMonth * days;
    }
}