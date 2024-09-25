using Riok.Mapperly.Abstractions;

namespace Hr.Api.HiringNewEmployees;

[Mapper]
public partial class EmployeeHiringRequestMapper
{
    public partial EmployeeHiringRequestResponseModel ToResponseModel(EmployeeHiringRequestEntity entity);
}

/*{
    "id": "GUID",
    "personalInformation": {
        "name": "Bob Smith",
        "departmentAppliedTo": "{department}"
    }, 
    "applicationDate": "DTO",
    "status": "Hired",
    "links": [        
        "self": "/departments/{department}/hiring-requests/GUID"
        "employee": "/employees/ismith-bob",
        "employee:role": "/departments/it",
        "hiring-requests:submitter": "/employees/sjones-sue"

    ]

}*/
