using Content.Shared._CE.Health;
using Content.Shared._CE.Health.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Shared._CE.StatusEffects.SeveredMemories;

[RegisterComponent]
public sealed partial class CEStatusEffectSeveredMemoriesComponent : Component
{
    /// <summary>
    /// How many stacks is needed to start loosing random abilities
    /// </summary>
    [DataField]
    public int AbilityLoosingThreshold = 20;

    /// <summary>
    /// Loose random ability each X stack
    /// </summary>
    [DataField]
    public int AbilityLoosingStack = 10;

    public int? LastLoosedAbilityStack;

    /// <summary>
    /// How many stacks is needed to start fading away
    /// </summary>
    [DataField]
    public int FadingThreshold = 60;

    /// <summary>
    /// How many stacks is needed to start dying
    /// </summary>
    [DataField]
    public int DyingThreshold = 90;

    /// <summary>
    /// How many hp is loosed per time
    /// </summary>
    [DataField]
    public int HpLoose = 1;

    [DataField(required: true)]
    public ProtoId<CEDamageTypePrototype> DamageType;

    /// <summary>
    /// How often hp loose is occuring
    /// </summary>
    [DataField]
    public TimeSpan HpLooseCycle = TimeSpan.FromSeconds(1);

    public TimeSpan NextHpLoose = TimeSpan.Zero;
}
