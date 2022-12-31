using Content.Server._Citadel.Worldgen.Components;
using JetBrains.Annotations;
using Robust.Shared.Map;
using Robust.Shared.Utility;

namespace Content.Server._Citadel.Worldgen.Systems;

/// <summary>
/// This provides some additional functions for world generation systems.
/// Exists primarily for convenience and to avoid code duplication.
/// </summary>
public abstract class BaseWorldSystem : EntitySystem
{
    [Dependency] private readonly IMapManager _mapManager = default!;
    [Dependency] private readonly WorldControllerSystem _worldController = default!;

    /// <summary>
    /// Gets an entity's coordinates in chunk space as an integer value.
    /// </summary>
    /// <param name="ent"></param>
    /// <param name="xform"></param>
    /// <returns>Chunk space coordinates</returns>
    [Pure]
    public Vector2i GetChunkCoords(EntityUid ent, TransformComponent xform)
    {
        DebugTools.Assert(!HasComp<WorldChunkComponent>(ent));

        return WorldGen.WorldToChunkCoords(xform.WorldPosition).Floored();
    }

    /// <summary>
    /// Gets an entity's coordinates in chunk space as a floating point value.
    /// </summary>
    /// <param name="ent"></param>
    /// <param name="xform"></param>
    /// <returns>Chunk space coordinates</returns>
    [Pure]
    public Vector2 GetFloatingChunkCoords(EntityUid ent, TransformComponent xform)
    {
        return WorldGen.WorldToChunkCoords(xform.WorldPosition);
    }

    /// <summary>
    /// Attempts to get a chunk, creating it if it doesn't exist.
    /// </summary>
    /// <param name="chunk">Chunk coordinates to get the chunk entity for.</param>
    /// <param name="map">Map the chunk is in.</param>
    /// <returns>A chunk, if available.</returns>
    [Pure]
    public EntityUid? GetOrCreateChunk(Vector2i chunk, EntityUid map, WorldControllerComponent? controller = null)
    {
        return _worldController.GetOrCreateChunk(chunk, map, controller);
    }
}
