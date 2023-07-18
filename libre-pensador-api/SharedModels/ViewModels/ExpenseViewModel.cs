namespace SharedModels.Models.ViewModels
{
    public class ExpenseViewModel : Expense
    {
        public DateTime DateInLocalTime
        { 
            get
            {
                return this.Date.ToLocalTime();
            }
            set 
            {
                this.Date = value.ToUniversalTime();
            }
        }
        public string CategoryName { get; set; } = null!;
    }
}
