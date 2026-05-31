namespace Content.Shared._CE.StatusEffects.Core.Activation.SourceInRadius;

/// <summary>
/// This is used for...
/// </summary>
[RegisterComponent]
public sealed partial class CEStatusEffectActivateSourceInRadiusComponent : Component
{
    [DataField(required: true)]
    public float Radius;
}
