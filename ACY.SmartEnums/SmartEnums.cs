using System;
using System.Collections.Generic;
using System.Linq;

namespace ACY.SmartEnum;

public abstract class SmartEnums<TEnum> where TEnum : SmartEnums<TEnum>
{
    public string Name { get; }
    public int Value { get; }

    protected SmartEnums(string name, int value)
    {
        Name = name;
        Value = value;
        _items.Add((TEnum)this);
    }

    private static readonly List<TEnum> _items = new List<TEnum>();

    public static IReadOnlyCollection<TEnum> List => _items.AsReadOnly();

    public static TEnum FromName(string name)
    {
        var match = _items.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (match == null) throw new ArgumentException($"No SmartEnum with name '{name}' found.");
        return match;
    }

    public static TEnum FromValue(int value)
    {
        var match = _items.FirstOrDefault(x => x.Value == value);
        if (match == null) throw new ArgumentException($"No SmartEnum with value '{value}' found.");
        return match;
    }

    public override string ToString() => Name;
}