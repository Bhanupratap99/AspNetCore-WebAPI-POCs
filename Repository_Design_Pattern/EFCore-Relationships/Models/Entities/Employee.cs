namespace EFCore_Relationships.Models.Entities;
public  class Employee
{
    public int EmployeeID { get; set; }       // PK
    public string Name { get; set; }          // Full Name
    public string Gender { get; set; }        // Male/Female
    public int? Salary { get; set; }          // Monthly Salary
    public string Dept { get; set; }          // Department Name
}
