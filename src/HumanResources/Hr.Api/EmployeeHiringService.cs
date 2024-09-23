namespace Hr.Api;

public class EmployeeHiringService
{

    public Employee Hire(EmployeeHiringRequest request)
    {

        return new Employee("99", "99", "99", 5);
    }
}


public record EmployeeHiringRequest(string Name, string Department);

public record Employee(string Id, string Name, string Department, decimal Salary);

/* Todo:
 * 
// name is required. Cannot be null, cannot be empty. Has to be at least 5 characters, and no more than 200
// department is required, and it has to have the values of "IT", "HR", "CEO", "SALES", "SUPPORT"
If you are are in the IT department:
Your employee ID starts with I, then a unique identifier.
Your starting salary is $180,000.

For *all* other departments:

Your employee ID starts with "S", then a unique identifier.
Your starting salary is $42,000.

*/