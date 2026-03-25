namespace Tutorial3;

public abstract class User
{
    public Guid ID { get;}= Guid.NewGuid();
    public string Name { get; set; }
    public string Surname { get; set; }
    public abstract string UserType { get; }
    public abstract int ActiveRentalLimit { get; }
    public User(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public override string ToString()
    {
        return $"{Name} {Surname} - {UserType}";
    }
}