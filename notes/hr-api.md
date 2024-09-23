# HR.Api

An API that allows us to hire new employees.

## POST /employees

Send us an entity like this:

```json
{
    "name": "Bob Smith",
    "department": "IT"
}

```

// name is required. Cannot be null, cannot be empty. Has to be at least 5 characters, and no more than 200
// department is required, and it has to have the values of "IT", "HR", "CEO", "SALES", "SUPPORT"



### Result: 201 Created (or 200, whatever)

```json
{
 
    "id": "I99999",
    "name": "Bob Smith",
    "department" "IT",
    "salary": "182000"
}
```

The business rules:

If you are are in the IT department:
Your employee ID starts with I, then a unique identifier.
Your starting salary is $180,000.

For *all* other departments:

Your employee ID starts with "S", then a unique identifier.
Your starting salary is $42,000.



Testing:
- Your programming language.
- Unit Testing is the second lowest