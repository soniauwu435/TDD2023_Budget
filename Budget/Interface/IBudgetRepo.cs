namespace Budget.Interface;

public interface IBudgetRepo
{
    List<Models.Budget> GetAll();
}