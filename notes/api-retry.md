

# POST /departments/{department}/hiring-requests

{
    "name": "Bob Smith"
}

201 Created
Location: /departments/{department}/hiring-requests/GUID

{
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

}


```json
{
    "id": "GUID",
    "personalInformation": {
        "name": "Jill Smith",
        "departmentAppliedTo": "CEO"

    },
    "applicationDate": "DTO",
    "status": "Awaiting Board Approval",
    "link": [
        "submit-approval": "/departments/ceo/hiring-requests/{GUID}/approval"
    ]
}


```

## GET /employees
## GET /departments/it/employees
## GET /departments/it/employees/ismith-bob
## GET /employees/ismith-bob