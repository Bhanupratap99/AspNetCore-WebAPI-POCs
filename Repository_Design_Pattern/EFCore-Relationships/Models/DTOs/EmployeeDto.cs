namespace EFCore_Relationships.Models.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }               // Different name than EmployeeID
    public string FullName { get; set; }      // Different name than Name
    public string Department { get; set; }    // Different name than Dept
    public string Gender { get; set; }        // Same
    public decimal AnnualSalary { get; set; } // Derived property (Salary * 12)
}
