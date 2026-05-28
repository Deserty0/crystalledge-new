using Content.Shared.Alert;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE.StatusEffects.Core.Components;

/// <summary>
/// Changes alert on stack
/// </summary>
[RegisterComponent]
public sealed partial class CEStatusEffectAlertOnStackComponent : Component
{
    /// <summary>
    /// Category of alerts
    /// </summary>
    [DataField(required: true)]
    public ProtoId<AlertCategoryPrototype> AlertsCategory;

    /// <summary>
    /// Stack: Alert
    /// Alerts must be in same group
    /// </summary>
    [DataField(required: true)]
    public SortedDictionary<int, ProtoId<AlertPrototype>>  Alerts;
}
