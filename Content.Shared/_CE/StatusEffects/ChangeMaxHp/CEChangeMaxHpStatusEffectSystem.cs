using Content.Shared._CE.Health;
using Content.Shared._CE.StatusEffects.Core;
using Content.Shared._CE.StatusEffects.Core.Components;
using Content.Shared.StatusEffectNew;

namespace Content.Shared._CE.StatusEffects.ChangeMaxHp;

public sealed class CEChangeMaxHpStatusEffectSystem : EntitySystem
{
    [Dependency] private readonly CEMobStateSystem _mobState = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CEChangeMaxHpStatusEffectComponent, StatusEffectAppliedEvent>(OnApply);
        SubscribeLocalEvent<CEChangeMaxHpStatusEffectComponent, StatusEffectRemovedEvent>(OnRemoved);
        SubscribeLocalEvent<CEChangeMaxHpStatusEffectComponent, CEStatusEffectStackEditedEvent>(OnStackEdited);
        SubscribeLocalEvent<CEChangeMaxHpStatusEffectComponent, StatusEffectRelayedEvent<CECalculateMaxHealthEvent>>(OnCalculateMaxHealth);
    }

    private void OnApply(Entity<CEChangeMaxHpStatusEffectComponent> ent, ref StatusEffectAppliedEvent args)
    {
        _mobState.RefreshMaxHealth(args.Target);
    }

    private void OnRemoved(Entity<CEChangeMaxHpStatusEffectComponent> ent, ref StatusEffectRemovedEvent args)
    {
        _mobState.RefreshMaxHealth(args.Target);
    }

    private void OnStackEdited(Entity<CEChangeMaxHpStatusEffectComponent> ent, ref CEStatusEffectStackEditedEvent args)
    {
        _mobState.RefreshMaxHealth(args.Target);
    }

    private void OnCalculateMaxHealth(Entity<CEChangeMaxHpStatusEffectComponent> ent,
        ref StatusEffectRelayedEvent<CECalculateMaxHealthEvent> args)
    {
        var stacks = 1;
        if (TryComp<CEStatusEffectStackComponent>(ent, out var stackComp))
            stacks = stackComp.Stacks;

        args.Args.FlatModifier += ent.Comp.FlatHpChange + ent.Comp.FlatHpChangePerStack * stacks;
        args.Args.Multiplier += ent.Comp.HpMultiplierChange + ent.Comp.HpMultiplierChangePerStack * stacks;
    }
}
