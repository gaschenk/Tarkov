// Copyright (c) 2023 Timothy Schenk. Subject to the GNU AGPL Version 3 License.

using System.Globalization;
using System.Text;

namespace Tarkov.LogParser;

public class LogParser
{
    private readonly CultureInfo _cultureInfo = CultureInfo.GetCultureInfo("de-de");

    public IEnumerable<LogEntry> ParseLogEntries(string multiLineLogEntries)
    {
        using var reader =
            new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(multiLineLogEntries)));
        return ParseLogEntries(reader);
    }

    public LogEntry[] ParseLogFile(string filePath)
    {
        using var reader =
            new StreamReader(filePath);
        return ParseLogEntries(reader);
    }

    private LogEntry[] ParseLogEntries(StreamReader reader)
    {
        var logEntries = new List<LogEntry>();
        var stringBuilder = new StringBuilder();

        while (reader.ReadLine() is { } line)
        {
            // Check if the line is the start of a new log entry
            if (line.Contains('|') && stringBuilder.Length > 0)
            {
                logEntries.Add(ParseLogEntry(stringBuilder.ToString()));
                stringBuilder.Clear();
            }

            stringBuilder.AppendLine(line);
        }

        // Add the last log entry if exists
        if (stringBuilder.Length > 0)
        {
            logEntries.Add(ParseLogEntry(stringBuilder.ToString()));
        }

        return logEntries.ToArray();
    }

    public LogEntry ParseLogEntry(string logEntry)
    {
        var parts = logEntry.Split('|', 5);

        var isSimple = !ClientVersion.TryParse(parts[1], out var clientVersion);

        var logLevelEnumName = Enum.GetNames(typeof(LogLevel))
            .First(e => e.Equals(isSimple ? parts[1] : parts[2], StringComparison.CurrentCultureIgnoreCase));
        var logLevel = Enum.Parse<LogLevel>(logLevelEnumName);

        var logTargetEnumName = Enum.GetNames(typeof(LogTarget)).FirstOrDefault(e =>
        {
            var value = (isSimple ? parts[2] : parts[3]).Replace("_", string.Empty).Replace("-", string.Empty);
            return e.Equals(value,
                StringComparison.CurrentCultureIgnoreCase);
        });
        var target = logTargetEnumName is not null ? Enum.Parse<LogTarget>(logTargetEnumName) : LogTarget.Unknown;

        return new LogEntry(
            DateTimeOffset.ParseExact(parts[0], "yyyy-MM-dd HH:mm:ss.fff zzz", _cultureInfo),
            clientVersion,
            logLevel,
            target,
            parts.Length > 4 ? parts[4].Trim() : string.Empty,
            parts.Length > 4 && parts[4].Trim().Contains('\n'));
    }
}
