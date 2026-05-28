using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Content.Shared._CE.StatusEffects.Core.Components;
using Content.Shared.Alert;
using Content.Shared.StatusEffectNew;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

namespace Content.Shared._CE.StatusEffects.Core;

/// <summary>
/// Changes additional status effect alert
/// </summary>
public sealed class CEStatusEffectAlertOnStackSystem : EntitySystem
{
    [Dependency] private readonly AlertsSystem _alerts = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<CEStatusEffectAlertOnStackComponent, CEStatusEffectEndingAttemptEvent>(OnBeforeEnded, after: [typeof(CEStatusEffectStackSystem)]);
        SubscribeLocalEvent<CEStatusEffectAlertOnStackComponent, CEStatusEffectStackEditedEvent>(OnStatusEffectStackChange);
    }

    private void OnBeforeEnded(Entity<CEStatusEffectAlertOnStackComponent> ent,
        ref CEStatusEffectEndingAttemptEvent args)
    {
        if (args.Cancelled)
            return;

        _alerts.ClearAlertCategory(args.Target, ent.Comp.AlertsCategory);
    }

    private void OnStatusEffectStackChange(Entity<CEStatusEffectAlertOnStackComponent> ent, ref CEStatusEffectStackEditedEvent args)
    {
        if (!TryGetAlertByStack(args.NewStack, ent.Comp, out var alert))
        {
            _alerts.ClearAlertCategory(args.Target, ent.Comp.AlertsCategory);
            return;
        }

        _alerts.ShowAlert(args.Target, alert.Value);
    }

    private bool TryGetAlertByStack(
        int stack,
        CEStatusEffectAlertOnStackComponent comp,
        [NotNullWhen(true)] out ProtoId<AlertPrototype>? alert)
    {
        alert = null;

        foreach (var pair in comp.Alerts.Reverse())
        {
            if (pair.Key <= stack)
            {
                alert = pair.Value;
                break;
            }
        }

        return alert != null;
    }
}
