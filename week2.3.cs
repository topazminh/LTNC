
using System;
using week2;

var dao = new StudentDAO();

dao.Add(new Student("S001", "Nguyen Van A", 20, "a@example.com"));
dao.Add(new Student("S002", "Tran Thi B", 22));

Console.WriteLine("All students:");
foreach (var s in dao.GetAlls())
{
    Console.WriteLine(s);
}

var found = dao.GetById("S001");
Console.WriteLine($"GetById S001: {found}");

var byName = dao.GetByName("Tran");
Console.WriteLine($"GetByName 'Tran' found {byName.Count} result(s)");

// Edit
dao.Edit(new Student("S002", "Tran Thi B Updated", 23, "b@example.com"));
Console.WriteLine("After edit:");
foreach (var s in dao.GetAlls()) Console.WriteLine(s);

// Delete
dao.Delete("S001");
Console.WriteLine("After delete S001:");
foreach (var s in dao.GetAlls()) Console.WriteLine(s);


