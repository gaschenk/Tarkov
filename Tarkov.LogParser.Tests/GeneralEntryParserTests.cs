// Copyright (c) 2023 Timothy Schenk. Subject to the GNU AGPL Version 3 License.

using Tarkov.LogParser.EntryTypes;

namespace Tarkov.LogParser.Tests;

public class GeneralEntryParserTests
{
    public static IEnumerable<object[]> UserConfirmedData => new List<object[]>
    {
        new object[]
        {
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
            new UserConfirmed()
            {
                Type = "userConfirmed",

                EventId = "abcdef01234567890abcdef0",
                Profileid = "abcdef01234567890abcdef0",
                ProfileToken = "abcdef01234567890abcdef012345678",
                Status = "Busy",
                Ip = "74.119.145.122",
                Port = 17003,
                Sid = "74.119.145.122-17003_PID_14.01.24_09.03.32",
                Version = "live",
                Location = "bigmap",
                RaidMode = "Online",
                Mode = "deathmatch",
                ShortId = "123A4B",
                AdditionalInfo = []
            }
        }
    };

    [Theory]
    [MemberData(nameof(UserConfirmedData))]
    public void UserConfirmedParsing(string input, UserConfirmed expectedOutput)
    {
        LogParser logParser = new();
        EntryParser entryParser = new();
        var dataObject = entryParser.Parse(logParser.ParseLogEntry(input));
        dataObject.Should().BeEquivalentTo(expectedOutput);
    }
}
