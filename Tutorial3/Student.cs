namespace Tutorial3;

public class Student : User
{
    public Student(string name, string surname) : base(name, surname) {}
    public override string UserType => "Student";
    public override int ActiveRentalLimit => 2;
}