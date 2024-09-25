using Riok.Mapperly.Abstractions;

namespace Hr.Api.HiringNewEmployees;

[Mapper]
public partial class EmployeeHiringRequestMapper
{
    public partial EmployeeHiringRequestResponseModel ToResponseModel(EmployeeHiringRequestEntity entity);
}

