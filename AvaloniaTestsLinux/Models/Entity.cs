using System;

namespace AvaloniaTestsLinux.Models;

internal class Entity
{
    internal int Code { get; }
    internal eName Name { get; }
    internal string Meaning { get; }
    internal DateTime CreatedAt { get; }

    internal enum eName
    {
        complate_state
    }
    
    internal Entity(int code, eName name, string meaning, DateTime createdAt)
    {
        Code = code;
        Name = name;
        Meaning = meaning;
        CreatedAt = createdAt;
    }
    
    
}