using Hr.Api.HiringNewEmployees.Entities;
using Hr.Api.HiringNewEmployees.Models;
using Riok.Mapperly.Abstractions;

namespace Hr.Api.HiringNewEmployees;
// TODO 2 - Show The Mapper
[Mapper]
public partial class EmployeeHiringRequestMapper
{
    public partial EmployeeHiringRequestResponseModel ToResponseModel(EmployeeHiringRequestEntity entity);
}

