namespace libre_pansador_api.Interfaces
{
    public interface IEmployeesService
    {
        Models.Employee? Read(string userName);
        Models.Employee? Create(Models.Employee newEmployee);
        Models.Employee? Delete(string userName);
        Models.Employee? Update(string userName, Models.RequestModels.UpdateEmployeeRequest updatedEmployee);
    }
}
