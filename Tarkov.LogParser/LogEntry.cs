// Copyright (c) 2023 Timothy Schenk. Subject to the GNU AGPL Version 3 License.

namespace Tarkov.LogParser;

public record LogEntry(
    DateTime TimeStamp,
    string ClientVersion,
    string LogLevel,
    string SectionName,
    string Data,
    bool IsMultiLine = false);
