using Content.Shared.StatusEffectNew;

namespace Content.Shared._CE.StatusEffects.Core.Activation;

/// <summary>
/// Handles activatable status effect activation
/// </summary>
public sealed class CEStatusEffectActivatableSystem : EntitySystem
{
    [Dependency] private StatusEffectsSystem _status = default!;

    public override void Initialize()
    {
    }

    private void ApplyEffects(Entity<CEStatusEffectActivatableComponent> ent, EntityUid target)
    {
        foreach (var effect in ent.Comp.Effects)
        {
            if (_status.HasStatusEffect(target, effect))
                continue;

            _status.TrySetStatusEffectDuration(target, effect);
        }
    }

    private void RemoveEffects(Entity<CEStatusEffectActivatableComponent> ent, EntityUid target)
    {
        foreach (var effect in ent.Comp.Effects)
        {
            _status.TryRemoveStatusEffect(target, effect);
        }
    }
}
