using System.Globalization;
using Content.Server.Chat.Managers;
using Content.Server.GameTicking;
using Robust.Server.GameObjects;
using Robust.Shared.Configuration;
using Robust.Shared.Map;

namespace Content.Server._Afterlight.Story;

/// <summary>
/// This handles the game's story and it's persistence.
/// </summary>
public sealed class StoryManagementSystem : EntitySystem
{
    [Dependency] private readonly MapLoaderSystem _mapLoader = default!;
    [Dependency] private readonly IConfigurationManager _cfg = default!;
    [Dependency] private readonly IMapManager _map = default!;
    [Dependency] private readonly IChatManager _chatManager = default!;

    private MapId? _storyMap;

    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<GameRunLevelChangedEvent>(OnRunLevelChanged);
    }

    private void OnRunLevelChanged(GameRunLevelChangedEvent ev)
    {
        if (ev.New == GameRunLevel.InRound)
        {
            var newMap = _map.CreateMap();
            _storyMap = newMap;
            if (!_mapLoader.TryLoad(newMap, _cfg.GetCVar(AfterlightCVars.StoryMapPath), out _))
            {
                _chatManager.DispatchServerAnnouncement("Failed to load the game's story data. Please contact your server administrator!");
            }
        }
        else if (ev.Old == GameRunLevel.InRound && _storyMap is not null)
        {
            // Save the game if we leave round for any reason.
            _mapLoader.SaveMap(_storyMap.Value, _cfg.GetCVar(AfterlightCVars.StoryMapPath));
            // For good measure, do it again at a backup location.
            _mapLoader.SaveMap(_storyMap.Value, $"{_cfg.GetCVar(AfterlightCVars.StoryBackupMapPath)}_{DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        }
    }
}
