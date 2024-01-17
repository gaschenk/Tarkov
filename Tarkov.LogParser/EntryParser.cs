// Copyright (c) 2023 Timothy Schenk. Subject to the GNU AGPL Version 3 License.

using System.Reflection;
using System.Text.Json;

namespace Tarkov.LogParser;

public class EntryParser
{
    public object? Parse(LogEntry logEntry)
    {
        if (!logEntry.IsMultiLine)
        {
            return null;
        }

        var parts = logEntry.Data.Split('\n');
        if (parts.Length < 2)
        {
            return null;
        }

        var nameOfObject = parts[0].Trim();
        var dataOfObject = string.Concat(parts[1..]).Trim();

        var assembly = Assembly.GetAssembly(typeof(EntryParser));
        var typeOfObject = assembly?.GetTypes().First(t => t.Name == nameOfObject);

        if (typeOfObject == null)
        {
            return null;
        }

        var parsedObject = JsonSerializer.Deserialize(dataOfObject, typeOfObject);
        return parsedObject;
    }
}
