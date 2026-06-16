using Content.Shared._CE.Mana.Core;
using Content.Shared._CE.StatusEffects.Core;
using Content.Shared._CE.StatusEffects.Core.Components;
using Content.Shared.StatusEffectNew;
using Robust.Shared.GameStates;

namespace Content.Shared._CE.StatusEffects;

[RegisterComponent, NetworkedComponent]
public sealed partial class CEBonusManaComponent : Component
{
    /// <summary>
    /// Changes max mana by flat amount
    /// </summary>
    [DataField]
    public int FlatChange = 10;

    /// <summary>
    /// Changes max mana by flat amount per stack
    /// </summary>
    [DataField]
    public int FlatChangePerStack = 0;

    /// <summary>
    /// Changes max mana by percent
    /// </summary>
    [DataField]
    public float MultiplierChange = 0;

    /// <summary>
    /// Changes max mana by percent per stack
    /// </summary>
    [DataField]
    public float MultiplierChangePerStack = 0;
}

public sealed partial class CEBonusManaSystem : EntitySystem
{
    [Dependency] private CESharedMagicEnergySystem _mana = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CEBonusManaComponent, StatusEffectAppliedEvent>(OnApply);
        SubscribeLocalEvent<CEBonusManaComponent, StatusEffectRemovedEvent>(OnRemoved);
        SubscribeLocalEvent<CEBonusManaComponent, CEStatusEffectStackEditedEvent>(OnStackEdited);
        SubscribeLocalEvent<CEBonusManaComponent, StatusEffectRelayedEvent<CECalculateMaxManaEvent>>(OnCalculateMaxMana);
    }

    private void OnApply(Entity<CEBonusManaComponent> ent, ref StatusEffectAppliedEvent args)
    {
        _mana.RefreshMaxMana(args.Target);
    }

    private void OnRemoved(Entity<CEBonusManaComponent> ent, ref StatusEffectRemovedEvent args)
    {
        _mana.RefreshMaxMana(args.Target);
    }

    private void OnStackEdited(Entity<CEBonusManaComponent> ent, ref CEStatusEffectStackEditedEvent args)
    {
        _mana.RefreshMaxMana(args.Target);
    }

    private void OnCalculateMaxMana(Entity<CEBonusManaComponent> ent,
        ref StatusEffectRelayedEvent<CECalculateMaxManaEvent> args)
    {
        var stacks = 1;
        if (TryComp<CEStatusEffectStackComponent>(ent, out var stackComp))
            stacks = stackComp.Stacks;

        args.Args.FlatModifier += ent.Comp.FlatChange + ent.Comp.FlatChangePerStack * stacks;
        args.Args.Multiplier += ent.Comp.MultiplierChange + ent.Comp.MultiplierChangePerStack * stacks;
    }
}
