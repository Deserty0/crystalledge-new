namespace Content.Shared._CE.StatusEffects.SeveredMemories;

/// <summary>
/// Effect is hardcoded to only work with SeveredMemories ent proto
/// </summary>
[RegisterComponent]
public sealed partial class CEStatusEffectSeveredMemoriesComponent : Component
{
    /// <summary>
    /// How many stacks is needed to start loosing random abilities
    /// </summary>
    [DataField]
    public int AbilityLoosingThreshold = 60;

    /// <summary>
    /// Loose random ability each X stack
    /// </summary>
    [DataField]
    public int AbilityLoosingStack = 10;

}
