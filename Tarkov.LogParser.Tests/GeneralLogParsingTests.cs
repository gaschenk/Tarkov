// Copyright (c) 2023 Timothy Schenk. Subject to the GNU AGPL Version 3 License.

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
                new LogEntry(new DateTimeOffset(2023, 12, 29, 23, 07, 43, 98, TimeSpan.FromHours(1)),
                    ClientVersion.Parse("0.14.0.0.28375"),
                    LogLevel.Info, LogTarget.Application,
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
                new LogEntry(new DateTimeOffset(2023, 12, 29, 23, 07, 34, 340, TimeSpan.FromHours(1)),
                    ClientVersion.Parse("0.14.0.0.28375"),
                    LogLevel.Info, LogTarget.Application, "Application awaken, updateQueue:'Update'"));

            data.Add(
                "2023-12-29 23:07:34.349 +01:00|0.14.0.0.28375|Info|application|Assert.raiseExceptions:'True' ",
                new LogEntry(new DateTimeOffset(2023, 12, 29, 23, 07, 34, 349, TimeSpan.FromHours(1)),
                    ClientVersion.Parse("0.14.0.0.28375"),
                    LogLevel.Info, LogTarget.Application, "Assert.raiseExceptions:'True'"));

            data.Add(
                "2023-12-29 23:07:34.349 +01:00|0.14.0.0.28375|Info|application|Application obfuscation succeed. ",
                new LogEntry(new DateTimeOffset(2023, 12, 29, 23, 07, 34, 349, TimeSpan.FromHours(1)),
                    ClientVersion.Parse("0.14.0.0.28375"),
                    LogLevel.Info, LogTarget.Application, "Application obfuscation succeed."));

            data.Add(
                "2023-12-29 23:07:36.196 +01:00|0.14.0.0.28375|Info|application|driveType:SSD swapDriveType:SSD ",
                new LogEntry(new DateTimeOffset(2023, 12, 29, 23, 07, 36, 196, TimeSpan.FromHours(1)),
                    ClientVersion.Parse("0.14.0.0.28375"),
                    LogLevel.Info, LogTarget.Application, "driveType:SSD swapDriveType:SSD"));

            data.Add(
                "2023-12-29 23:07:38.100 +01:00|0.14.0.0.28375|Info|application|NVIDIA Reflex is available on this machine, Status: NvReflex_OK. ",
                new LogEntry(new DateTimeOffset(2023, 12, 29, 23, 07, 38, 100, TimeSpan.FromHours(1)),
                    ClientVersion.Parse("0.14.0.0.28375"),
                    LogLevel.Info, LogTarget.Application,
                    "NVIDIA Reflex is available on this machine, Status: NvReflex_OK."));

            data.Add(
                """
                2024-01-14 10:22:05.846 +01:00|Info|push-notifications|Got notification | UserConfirmed
                {
                  "type": "userConfirmed",
                  "eventId": "abcdef01234567890abcdef0",
                  "profileid": "abcdef01234567890abcdef0",
                  "profileToken": "abcdef01234567890abcdef012345678",
                  "status": "Busy",
                  "ip": "74.119.145.122",
                  "port": 17003,
                  "sid": "74.119.145.122-17003_PID_14.01.24_09.03.32",
                  "version": "live",
                  "location": "bigmap",
                  "raidMode": "Online",
                  "mode": "deathmatch",
                  "shortId": "123A4B",
                  "additional_info": []
                }
                """,
                new LogEntry(new DateTimeOffset(2024, 1, 14, 10, 22, 5, 846, TimeSpan.FromHours(1)),
                    null,
                    LogLevel.Info, LogTarget.PushNotifications, """
                                                                UserConfirmed
                                                                {
                                                                  "type": "userConfirmed",
                                                                  "eventId": "abcdef01234567890abcdef0",
                                                                  "profileid": "abcdef01234567890abcdef0",
                                                                  "profileToken": "abcdef01234567890abcdef012345678",
                                                                  "status": "Busy",
                                                                  "ip": "74.119.145.122",
                                                                  "port": 17003,
                                                                  "sid": "74.119.145.122-17003_PID_14.01.24_09.03.32",
                                                                  "version": "live",
                                                                  "location": "bigmap",
                                                                  "raidMode": "Online",
                                                                  "mode": "deathmatch",
                                                                  "shortId": "123A4B",
                                                                  "additional_info": []
                                                                }
                                                                """,
                    true));

            return data;
        }
    }

    [Theory]
    [MemberData(nameof(EntryParseData))]
    public void TestSingleLogEntryParsing(string input, LogEntry expectedOutput)
    {
        LogParser logParser = new();
        logParser.ParseLogEntry(input).Should().BeEquivalentTo(expectedOutput);
    }

    [Fact]
    public void TestMultipleLogEntries()
    {
        LogParser logParser = new();
        var multiLineLogEntries = string.Join("\r\n",
            _multiLineEntries.ToArray() ?? throw new InvalidOperationException());
        var entries = logParser.ParseLogEntries(multiLineLogEntries);
        entries.Should().BeEquivalentTo(_multiLineLogEntries);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
