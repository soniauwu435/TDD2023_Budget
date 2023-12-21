namespace Budget;

public class BudgetService
{
    private IBudgetRepo _budgetRepo;
    
    public decimal Query(DateTime start, DateTime end)
    {
        if (end < start) return 0;
        var budgets = _budgetRepo.GetAll();

        var startYearMonth = start.ToString("yyyyMM");
        var endYearMonth = end.ToString("yyyyMM");

        if (startYearMonth == endYearMonth)
        {
            var days = (end - start).Days + 1;
            var budget = budgets.First(x => x.YearMonth == startYearMonth);

            return budget.GetAmountByDays(days);
        }

        var temp = start;
        var totalBudget = 0m;
        while (temp < end)
        {
                
            var date = new DateTime(temp.Year, temp.Month, 1).AddMonths(1).AddDays(-1);
            var daysInMonth = (date - temp).Days + 1;
            totalBudget += budgets.First(x => x.YearMonth == temp.ToString("yyyyMM")).GetAmountByDays(daysInMonth);
            temp = temp.AddMonths(1);
        }

        totalBudget += budgets.First(x => x.YearMonth == end.ToString("yyyyMM")).GetAmountByDays(end.Day);
            
        return totalBudget;
        
    }
}

internal interface IBudgetRepo
{
    List<Budget> GetAll();
}



class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }
    public int DaysInMonth { get; set; }

    public decimal GetAmountByDays(int days)
    {
        return Amount / DaysInMonth * days;
    }
}
