using Content.Shared._CE.StatusEffectStacks;
using Content.Shared.StatusEffect;
using Content.Shared.StatusEffectNew.Components;

namespace Content.Shared._CE.StatusEffects.Core.Activation.SourceInRadius;

/// <summary>
/// This handles...
/// </summary>
public sealed class CEStatusEffectActivateSourceInRadiusSystem : EntitySystem
{
    [Dependency] private CEStatusEffectStackSystem _stackSystem = default!;
    public override void Initialize()
    {

    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<CEStatusEffectActivateSourceInRadiusComponent, CEStatusEffectActivatableComponent, CEStatusEffectSourceComponent, StatusEffectComponent>();

        while (query.MoveNext(out var ent,
                   out var radiusComponent,
                   out var activatableComponent,
                   out var sourceComponent,
                   out var statusEffectComponent))
        {
            var source = _stackSystem.GetSource((ent, sourceComponent));

            if (source is null)
            {
                if (!activatableComponent.Active)
                    continue;


            }


        }
    }
}
