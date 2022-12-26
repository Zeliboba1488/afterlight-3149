using Content.Server._Afterlight.Worldgen.Components;
using Robust.Server.GameObjects;
using Robust.Server.Maps;

namespace Content.Server._Afterlight.Worldgen.Systems;

/// <summary>
/// This handles placing blueprints into the world via markers.
/// </summary>
public sealed class BlueprintPlacerSystem : EntitySystem
{
    [Dependency] private readonly MapLoaderSystem _mapLoader = default!;
    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<BlueprintPlacerComponent, MapInitEvent>(OnMapInit);
    }

    private void OnMapInit(EntityUid uid, BlueprintPlacerComponent component, MapInitEvent args)
    {
        var xform = Transform(uid);
        var options = new MapLoadOptions()
        {
            LoadMap = false,
            Offset = xform.WorldPosition,
            Rotation = xform.LocalRotation,
        };

        _mapLoader.TryLoad(xform.MapID, component.Blueprint.ToString(), out _, options);
    }
}
