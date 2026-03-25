namespace Tutorial3;

public class Employee : User
{
    public Employee(string name, string surname) : base(name, surname) {}
    public override string UserType => "Employee";
    public override int ActiveLoansLimit => 7;
}