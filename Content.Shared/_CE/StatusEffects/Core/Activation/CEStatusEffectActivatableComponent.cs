using Robust.Shared.Prototypes;

namespace Content.Shared._CE.StatusEffects.Core.Activation;

/// <summary>
/// When activated applies another effects to entity
/// When deactivated removes effects from entity
/// Can be used to create illusion of deactivated status effect
/// </summary>
[RegisterComponent]
public sealed partial class CEStatusEffectActivatableComponent : Component
{
    /// <summary>
    /// Effects to be applied/removed
    /// </summary>
    [DataField]
    public HashSet<EntProtoId> Effects = [];

    public bool Active = false;
}
