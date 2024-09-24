Hiring Employees (Operation)
    - name (valid name)
    - department - a valid department (uri)


Result: a new employee
    - id
    - name
    - salary
    - department
    - hireDate


Ways to send data to the api to fulfill an operation:

- body (POST, PUT)
- headers (Authorization) - who
- in the URL (route parameters)
- query string arguments (GET collection)


"Ubiquitous Language"

"A manager submits a hiring request for their department"

POST /departments/{department}/hiring-requests
Authorization: some manager
Content-Type: application/json

{
    "name": "Joseph K. Brown"
}


200 Ok


  {
 
    "id": "I99999",
    "name": "Bob Smith",
    "department" "IT",
    "salary": "182000",
    "status": "Pending Board Approval"
}  
