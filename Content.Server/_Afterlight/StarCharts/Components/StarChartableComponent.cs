using Robust.Shared.Map;

namespace Content.Server._Afterlight.StarCharts.Components;

/// <summary>
/// This is used for entities that should be findable with a redeemed star chart.
/// </summary>
[RegisterComponent]
public sealed class StarChartableComponent : Component
{
    [DataField("chartedLocation")]
    public MapCoordinates? ChartedLocation;

    [DataField("chartName", required: true)]
    public string ChartName = default!;

    [DataField("chartDescription", required: true)]
    public string ChartDescription = default!;
}
