using System;
using System.Collections.Generic;
using System.Linq;

namespace week2;

public class StudentDAO
{
    private readonly List<Student> _students = new();

    // Add a new student. Throws InvalidOperationException if a student with same Id exists.
    public void Add(Student student)
    {
        if (student is null) throw new ArgumentNullException(nameof(student));
        if (string.IsNullOrWhiteSpace(student.Id)) throw new ArgumentException("Student Id is required", nameof(student));

        if (_students.Any(s => string.Equals(s.Id, student.Id, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Student with Id '{student.Id}' already exists.");

        _students.Add(student);
    }

    // Edit existing student by Id. Returns true if updated, false if not found.
    public bool Edit(Student student)
    {
        if (student is null) throw new ArgumentNullException(nameof(student));
        var existing = _students.FirstOrDefault(s => string.Equals(s.Id, student.Id, StringComparison.OrdinalIgnoreCase));
        if (existing is null) return false;

        existing.Name = student.Name;
        existing.Age = student.Age;
        existing.Email = student.Email;
        return true;
    }

    // Delete student by id. Returns true if removed.
    public bool Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return false;
        var existing = _students.FirstOrDefault(s => string.Equals(s.Id, id, StringComparison.OrdinalIgnoreCase));
        if (existing is null) return false;
        return _students.Remove(existing);
    }

    // Get all students.
    public List<Student> GetAlls()
    {
        // Return a shallow copy to avoid external modification of internal list
        return new List<Student>(_students);
    }

    // Get by id, null if not found
    public Student? GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        return _students.FirstOrDefault(s => string.Equals(s.Id, id, StringComparison.OrdinalIgnoreCase));
    }

    // Get students whose name contains the provided text (case-insensitive)
    public List<Student> GetByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return new List<Student>();
        return _students
            .Where(s => s.Name != null && s.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();
    }

    // Optional: clear all students
    public void Clear() => _students.Clear();
}
