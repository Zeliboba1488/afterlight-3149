using Robust.Shared.Configuration;

namespace Content.Server._Afterlight;

[CVarDefs]
public sealed class AfterlightCVars
{
    /// <summary>
    /// Whether or not ship spawning is enabled.
    /// </summary>
    public static readonly CVarDef<bool> ShipSpawningEnabled =
        CVarDef.Create("afterlight.shipspawning.enabled", true, CVar.SERVERONLY);

    public static readonly CVarDef<string> StoryMapPath =
        CVarDef.Create("afterlight.story_map_path", "/storydata.yml", CVar.SERVERONLY);

    public static readonly CVarDef<string> StoryBackupMapPath =
        CVarDef.Create("afterlight.story_map_backup_path", "/storyBack/storydata.yml", CVar.SERVERONLY);
}
