using System;

namespace week2;

public class Student
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string? Email { get; set; }

    public Student(string id, string name, int age, string? email = null)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Age = age;
        Email = email;
    }

    // Parameterless constructor for serializers / tooling
    public Student()
    {
        Id = string.Empty;
        Name = string.Empty;
        Age = 0;
    }

    public override string ToString()
    {
        return $"Id={Id}, Name={Name}, Age={Age}, Email={Email}";
    }
}
