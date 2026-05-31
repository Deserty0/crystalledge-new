using Robust.Shared.Prototypes;

namespace Content.Shared._CE.StatusEffects.Core.Activation;

/// <summary>
/// When activated applies another effects to entity
/// When deactivated removes effects from entity
/// Can be used to create illusion of deactivated status effect
/// </summary>
[RegisterComponent, Access(typeof(CEStatusEffectActivatableSystem), Other = AccessPermissions.Read)]
public sealed partial class CEStatusEffectActivatableComponent : Component
{
    /// <summary>
    /// Effects to be applied/removed
    /// </summary>
    [DataField]
    public HashSet<EntProtoId> Effects = [];

    /// <summary>
    /// You can read it freely!!!!
    /// Use <see cref="CEStatusEffectActivatableSystem"/> to change it
    /// </summary>
    public bool Active = false;
}
