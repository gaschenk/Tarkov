// Copyright (c) 2023 Timothy Schenk. Subject to the GNU AGPL Version 3 License.

using System.Globalization;

namespace Tarkov.LogParser.Tests;

public class GeneralLogParsingTests : IDisposable
{
    public GeneralLogParsingTests()
    {
        var inputData = new List<string>();
        var outputData = new List<LogEntry>();
        foreach (var entryParseData in EntryParseData)
        {
            inputData.Add(entryParseData[0] as string ?? throw new InvalidOperationException());
            outputData.Add(entryParseData[1] as LogEntry ?? throw new InvalidOperationException());
        }

        _multiLineEntries = inputData;
        _multiLineLogEntries = outputData;
    }

    private readonly List<string> _multiLineEntries;
    private readonly List<LogEntry> _multiLineLogEntries;

    public static TheoryData<string, LogEntry> EntryParseData
    {
        get
        {
            var data = new TheoryData<string, LogEntry>();
            data.Add("""
                     2023-12-29 23:07:43.098 +01:00|0.14.0.0.28375|Info|application|Game settings:
                     {
                       "Language": "en",
                       "QuickSlotsVisibility": "Autohide",
                       "StaminaVisibility": "Autohide",
                       "HealthVisibility": "Autohide",
                       "HealthColor": "WhiteToRed",
                       "NotificationTransportType": "Default",
                       "ConnectionType": "Default",
                       "HighlightScope": "All",
                       "FieldOfView": 75,
                       "HeadBobbing": 1.0,
                       "AutoEmptyWorkingSet": false,
                       "SetAffinityToLogicalCores": true,
                       "EnableHideoutPreload": true,
                       "StreamerModeEnabled": false,
                       "BlockGroupInvites": false,
                       "ItemQuickUseMode": "InRaidAndInLobby",
                       "AutoVaultingMode": "Hotkey",
                       "MalfunctionVisability": true,
                       "TradingIntermediateScreen": false,
                       "RagfairLinesCount": 15,
                       "EnvironmentUiType": "WoodEnvironmentUiType"
                     }
                     """,
                new LogEntry(new DateTime(2023, 12, 29, 23, 07, 43, 98, new CultureInfo("de-de").Calendar),
                    "0.14.0.0.28375",
                    "Info", "application",
                    """
                    Game settings:
                    {
                      "Language": "en",
                      "QuickSlotsVisibility": "Autohide",
                      "StaminaVisibility": "Autohide",
                      "HealthVisibility": "Autohide",
                      "HealthColor": "WhiteToRed",
                      "NotificationTransportType": "Default",
                      "ConnectionType": "Default",
                      "HighlightScope": "All",
                      "FieldOfView": 75,
                      "HeadBobbing": 1.0,
                      "AutoEmptyWorkingSet": false,
                      "SetAffinityToLogicalCores": true,
                      "EnableHideoutPreload": true,
                      "StreamerModeEnabled": false,
                      "BlockGroupInvites": false,
                      "ItemQuickUseMode": "InRaidAndInLobby",
                      "AutoVaultingMode": "Hotkey",
                      "MalfunctionVisability": true,
                      "TradingIntermediateScreen": false,
                      "RagfairLinesCount": 15,
                      "EnvironmentUiType": "WoodEnvironmentUiType"
                    }
                    """, true));

            data.Add(
                "2023-12-29 23:07:34.340 +01:00|0.14.0.0.28375|Info|application|Application awaken, updateQueue:'Update' ",
                new LogEntry(new DateTime(2023, 12, 29, 23, 07, 34, 340, new CultureInfo("de-de").Calendar),
                    "0.14.0.0.28375",
                    "Info", "application", "Application awaken, updateQueue:'Update'"));

            data.Add(
                "2023-12-29 23:07:34.349 +01:00|0.14.0.0.28375|Info|application|Assert.raiseExceptions:'True' ",
                new LogEntry(new DateTime(2023, 12, 29, 23, 07, 34, 349, new CultureInfo("de-de").Calendar),
                    "0.14.0.0.28375",
                    "Info", "application", "Assert.raiseExceptions:'True'"));

            data.Add(
                "2023-12-29 23:07:34.349 +01:00|0.14.0.0.28375|Info|application|Application obfuscation succeed. ",
                new LogEntry(new DateTime(2023, 12, 29, 23, 07, 34, 349, new CultureInfo("de-de").Calendar),
                    "0.14.0.0.28375",
                    "Info", "application", "Application obfuscation succeed."));

            data.Add(
                "2023-12-29 23:07:36.196 +01:00|0.14.0.0.28375|Info|application|driveType:SSD swapDriveType:SSD ",
                new LogEntry(new DateTime(2023, 12, 29, 23, 07, 36, 196, new CultureInfo("de-de").Calendar),
                    "0.14.0.0.28375",
                    "Info", "application", "driveType:SSD swapDriveType:SSD"));

            data.Add(
                "2023-12-29 23:07:38.100 +01:00|0.14.0.0.28375|Info|application|NVIDIA Reflex is available on this machine, Status: NvReflex_OK. ",
                new LogEntry(new DateTime(2023, 12, 29, 23, 07, 38, 100, new CultureInfo("de-de").Calendar),
                    "0.14.0.0.28375",
                    "Info", "application", "NVIDIA Reflex is available on this machine, Status: NvReflex_OK."));

            return data;
        }
    }

    [Theory]
    [MemberData(nameof(EntryParseData))]
    public void Test1(string input, LogEntry expectedOutput)
    {
        LogParser logParser = new();
        logParser.ParseLogEntry(input).Should().Be(expectedOutput);
    }

    [Fact]
    public void TestMultipleLogEntries()
    {
        LogParser logParser = new();
        var multiLineLogEntries = string.Join(Environment.NewLine,
            _multiLineEntries.ToArray() ?? throw new InvalidOperationException());
        var entries = logParser.ParseLogEntries(multiLineLogEntries);
        entries.Should().BeEquivalentTo(_multiLineLogEntries);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
