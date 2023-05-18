namespace libre_pansador_api.Interfaces
{
    public interface IEmployeesService
    {
        Models.Employee? Read(string userName);
    }
}
