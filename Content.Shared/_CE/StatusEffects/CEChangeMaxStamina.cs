using Content.Shared._CE.Stamina;
using Content.Shared._CE.StatusEffects.Core;
using Content.Shared._CE.StatusEffects.Core.Components;
using Content.Shared.StatusEffectNew;
using Robust.Shared.GameStates;

namespace Content.Shared._CE.StatusEffects;

[RegisterComponent, NetworkedComponent]
public sealed partial class CEBonusStaminaComponent : Component
{
    /// <summary>
    /// Changes max stamina by flat amount
    /// </summary>
    [DataField]
    public int FlatChange = 10;

    /// <summary>
    /// Changes max stamina by flat amount per stack
    /// </summary>
    [DataField]
    public int FlatChangePerStack = 0;

    /// <summary>
    /// Changes max stamina by percent
    /// </summary>
    [DataField]
    public float MultiplierChange = 0;

    /// <summary>
    /// Changes max stamina by percent per stack
    /// </summary>
    [DataField]
    public float MultiplierChangePerStack = 0;
}

public sealed partial class CEBonusStaminaSystem : EntitySystem
{
    [Dependency] private CEStaminaSystem _stamina = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CEBonusStaminaComponent, StatusEffectAppliedEvent>(OnApply);
        SubscribeLocalEvent<CEBonusStaminaComponent, StatusEffectRemovedEvent>(OnRemoved);
        SubscribeLocalEvent<CEBonusStaminaComponent, CEStatusEffectStackEditedEvent>(OnStackEdited);
        SubscribeLocalEvent<CEBonusStaminaComponent, StatusEffectRelayedEvent<CECalculateMaxStaminaEvent>>(OnCalculateMaxStamina);
    }

    private void OnApply(Entity<CEBonusStaminaComponent> ent, ref StatusEffectAppliedEvent args)
    {
        _stamina.RefreshMaxStamina(args.Target);
    }

    private void OnRemoved(Entity<CEBonusStaminaComponent> ent, ref StatusEffectRemovedEvent args)
    {
        _stamina.RefreshMaxStamina(args.Target);
    }

    private void OnStackEdited(Entity<CEBonusStaminaComponent> ent, ref CEStatusEffectStackEditedEvent args)
    {
        _stamina.RefreshMaxStamina(args.Target);
    }

    private void OnCalculateMaxStamina(Entity<CEBonusStaminaComponent> ent,
        ref StatusEffectRelayedEvent<CECalculateMaxStaminaEvent> args)
    {
        var stacks = 1;
        if (TryComp<CEStatusEffectStackComponent>(ent, out var stackComp))
            stacks = stackComp.Stacks;

        args.Args.FlatModifier += ent.Comp.FlatChange + ent.Comp.FlatChangePerStack * stacks;
        args.Args.Multiplier += ent.Comp.MultiplierChange + ent.Comp.MultiplierChangePerStack * stacks;
    }
}
