﻿namespace Hr.Tests;
public class EmployeeHiringServiceTests
{

    [Fact]
    public void Tacos()
    {
        // Given
        //var service = new EmployeeHiringService();
        //var request = new EmployeeHiringRequest("", "JANITOR");



        //// When
        //var ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.Hire(request));

        //Assert.Equal(nameof(EmployeeHiringRequest.Name), ex.ParamName);
    }
}

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