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

        var startYearMonthBudget = budgets.Where(x => x.YearMonth == startYearMonth);
        var endYearMonthBudget = budgets.Where(x => x.YearMonth == endYearMonth);


        return 0;
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
}