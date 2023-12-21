using Budget.Interface;

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
            return budgets.FirstOrDefault(x => x.YearMonth == startYearMonth)?.GetAmountByDays(days) ?? 0;
        }

        var temp = start;
        var totalBudget = 0m;
        while (temp < new DateTime(end.Year, end.Month, 1))
        {
            var date = new DateTime(temp.Year, temp.Month, 1).AddMonths(1).AddDays(-1);
            var daysInMonth = (date - temp).Days + 1;
            totalBudget += budgets.FirstOrDefault(x => x.YearMonth == temp.ToString("yyyyMM"))?.GetAmountByDays(daysInMonth) ?? 0;
            temp = temp.AddMonths(1);
        }

        totalBudget += budgets.FirstOrDefault(x => x.YearMonth == end.ToString("yyyyMM"))?.GetAmountByDays(end.Day) ?? 0;
            
        return totalBudget;
        
    }
}