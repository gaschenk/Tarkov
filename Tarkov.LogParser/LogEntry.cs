// Copyright (c) 2023 Timothy Schenk. Subject to the GNU AGPL Version 3 License.

namespace Tarkov.LogParser;

public record LogEntry(
    DateTimeOffset TimeStamp,
    ClientVersion? ClientVersion,
    LogLevel LogLevel,
    LogTarget LogTarget,
    string Data,
    bool IsMultiLine = false);

public enum LogLevel
{
    Debug,
    Trace,
    Info,
    Warn,
    Error
}

public enum LogTarget
{
    Export,
    FilesChecker,
    AiData,
    AiMoveData,
    AiMapSettingsData,
    AiCoverData,
    AiDecision,
    AntiCheat,
    Application,
    Assets,
    Backend,
    BackendCache,
    BackendQueue,
    Exfiltration,
    Insurance,
    Inventory,
    DataTransfer,
    ObjectPool,
    Ping,
    Player,
    Scenes,
    Screen,
    Fps,
    UiSounds,
    Localization,
    Surprises,
    Resources,
    PushNotifications,
    Spawns,
    SpawnSystem,
    MapErrors,
    Channels,
    AssetBundle,
    Hideout,
    ConnectionDiagnostics,
    Speaker,
    SpatialAudio,
    Winter,
    NetworkConnection,
    NetworkMessages,
    HandsStates,
    AnimEventsEmitter,
    AnimEventsContainer,
    AnimEvents,
    FastAnimatorController,
    NetSimulationAppLogger,
    Unknown
}

public enum LogLayout
{
    Simple,
    Complex
}

public class ClientVersion
{
    public int MajorVersion { get; set; }
    public int MinorVersion { get; set; }
    public int RevisionVersion { get; set; }
    public int PatchVersion { get; set; }
    public int Build { get; set; }

    public static bool TryParse(string text, out ClientVersion? version)
    {
        version = null;
        var parts = text.Split('.');

        if (parts.Length is > 5 or < 5)
        {
            return false;
        }

        if (int.TryParse(parts[0], out var majorVersion) == false)
        {
            return false;
        }

        if (int.TryParse(parts[1], out var minorVersion) == false)
        {
            return false;
        }

        if (int.TryParse(parts[2], out var revisionVersion) == false)
        {
            return false;
        }

        if (int.TryParse(parts[3], out var patchVersion) == false)
        {
            return false;
        }

        if (int.TryParse(parts[4], out var build) == false)
        {
            return false;
        }

        version = new ClientVersion
        {
            MajorVersion = majorVersion,
            MinorVersion = minorVersion,
            RevisionVersion = revisionVersion,
            PatchVersion = patchVersion,
            Build = build
        };
        return true;
    }

    public static ClientVersion? Parse(string text)
    {
        var version = new ClientVersion();
        var parts = text.Split('.');

        if (parts.Length is > 5 or < 5)
        {
            return null;
        }

        if (int.TryParse(parts[0], out var majorVersion) == false)
        {
            return null;
        }

        if (int.TryParse(parts[1], out var minorVersion) == false)
        {
            return null;
        }

        if (int.TryParse(parts[2], out var revisionVersion) == false)
        {
            return null;
        }

        if (int.TryParse(parts[3], out var patchVersion) == false)
        {
            return null;
        }

        if (int.TryParse(parts[4], out var build) == false)
        {
            return null;
        }

        version.MajorVersion = majorVersion;
        version.MinorVersion = minorVersion;
        version.RevisionVersion = revisionVersion;
        version.PatchVersion = patchVersion;
        version.Build = build;

        return version;
    }
}
