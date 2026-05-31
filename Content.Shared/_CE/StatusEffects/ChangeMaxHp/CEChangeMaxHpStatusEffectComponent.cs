namespace Content.Shared._CE.StatusEffects.ChangeMaxHp;

/// <summary>
/// This is used for changing target hp
/// Positive delta is adding hp
/// Negative delta is reducing hp
/// </summary>
[RegisterComponent]
public sealed partial class CEChangeMaxHpStatusEffectComponent : Component
{
    /// <summary>
    /// Changes max HP by flat amount
    /// </summary>
    [DataField]
    public int FlatHpChange = 0;

    /// <summary>
    /// Changes max HP by flat amount per stack
    /// </summary>
    [DataField]
    public int FlatHpChangePerStack = 0;

    /// <summary>
    /// Changes max HP by percent
    /// </summary>
    [DataField]
    public float HpMultiplierChange = 0;

    /// <summary>
    /// Changes max HP by percent per stack
    /// </summary>
    [DataField]
    public float HpMultiplierChangePerStack = 0;
}
