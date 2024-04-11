using System;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace AvaloniaTestsLinux.Models;

internal class Group
{
    internal Guid Id { get; }
    internal string Name { get; }
    internal Entity State { get; private set; }
    internal uint ComplatePercent { get; private set; }
    internal DateTime CreatedAt { get; }
    internal DateTime? ModifiedAt { get; private set; }
    internal ObservableCollection<Test> Tests { get; } = new ObservableCollection<Test>();

    internal Group(Guid id, string name, uint complatePercent, DateTime createdAt, DateTime? modifiedAt)
    {
        
    } 
}