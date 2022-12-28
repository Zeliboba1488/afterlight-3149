using System.Linq;
using Content.Server.Atmos.EntitySystems;
using Content.Server.GameTicking;
using Content.Server.Gravity;
using Content.Server.Power.Components;
using Content.Server.Power.EntitySystems;
using Robust.Shared.Random;

namespace Content.Server._Afterlight.Story;

/// <summary>
/// This handles making the ship feel like it's been empty for a while.
/// </summary>
/// <remarks>In the prior codebase this also set the temperature lower, but that was done under the expectation it would warm up with time, which no longer happens.</remarks>
public sealed class AbandonedShipSystem : EntitySystem
{
    [Dependency] private readonly ApcSystem _apcSystem = default!;
    [Dependency] private readonly GravityGeneratorSystem _gravityGeneratorSystem = default!;

    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<PostGameMapLoad>(OnPostGameMapLoad);
    }

    private void OnPostGameMapLoad(PostGameMapLoad ev)
    {
        foreach (var ent in ev.Grids.SelectMany(x => Transform(x).ChildEntities))
        {
            if (TryComp<GravityGeneratorComponent>(ent, out var gravGen))
            {
                _gravityGeneratorSystem.DepowerGenerator(ent, gravGen);
            }

            if (!TryComp<BatteryComponent>(ent, out var bat))
                continue;

            bat.CurrentCharge = 0;

            if (!TryComp<ApcComponent>(ent, out var apc) || apc.MainBreakerEnabled)
                continue;

            _apcSystem.ApcToggleBreaker(ent, apc);
        }
    }
}
