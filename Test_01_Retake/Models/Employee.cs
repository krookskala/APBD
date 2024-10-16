namespace Test_01_Retake.Models;

public class Employee
{
    public int EmpId { get; set; }
    public string EmpName { get; set; } = string.Empty;
    public string JobName { get; set; } = string.Empty;
    public int? ManagerId { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public decimal? Commission { get; set; }
    public int DepId { get; set; }
}
