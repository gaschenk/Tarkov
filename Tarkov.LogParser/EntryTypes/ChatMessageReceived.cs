// Copyright (c) 2023 Timothy Schenk. Subject to the GNU AGPL Version 3 License.

using System.Text.Json.Serialization;

namespace Tarkov.LogParser.EntryTypes;

public class ChatMessageReceived
{

}

public class UserConfirmed
{

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("eventId")]
    public string EventId { get; set; }

    [JsonPropertyName("profileid")]
    public string Profileid { get; set; }

    [JsonPropertyName("profileToken")]
    public string ProfileToken { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("ip")]
    public string Ip { get; set; }

    [JsonPropertyName("port")]
    public int Port { get; set; }

    [JsonPropertyName("sid")]
    public string Sid { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; }

    [JsonPropertyName("raidMode")]
    public string RaidMode { get; set; }

    [JsonPropertyName("mode")]
    public string Mode { get; set; }

    [JsonPropertyName("shortId")]
    public string ShortId { get; set; }

    [JsonPropertyName("additional_info")]
    public List<object> AdditionalInfo { get; set; }
}
