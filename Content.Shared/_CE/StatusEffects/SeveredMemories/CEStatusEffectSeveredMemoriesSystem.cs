using Content.Shared._CE.Health;
using Content.Shared._CE.Health.Components;
using Content.Shared._CE.Skill.Core;
using Content.Shared._CE.Skill.Core.Components;
using Content.Shared._CE.StatusEffects.Core;
using Content.Shared._CE.StatusEffects.Core.Components;
using Content.Shared.Fluids;
using Content.Shared.StatusEffectNew.Components;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Shared._CE.StatusEffects.SeveredMemories;

public sealed class CEStatusEffectSeveredMemoriesSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly CESharedDamageableSystem _damage = default!;
    [Dependency] private readonly CESharedSkillSystem _skills = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CEStatusEffectSeveredMemoriesComponent, CEStatusEffectStackEditedEvent>(OnStackEdited);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQuery<CEStatusEffectSeveredMemoriesComponent, CEStatusEffectStackComponent, StatusEffectComponent>();

        foreach (var (comp, stack, status) in query)
        {
            if (comp.DyingThreshold > stack.Stacks)
                continue;

            if (comp.NextHpLoose > _timing.CurTime)
                continue;

            if (status.AppliedTo is null)
                continue;

            var damage = new CEDamageSpecifier(comp.DamageType, comp.HpLoose);
            _damage.TakeDamage(status.AppliedTo.Value, damage);
        }
    }

    private void OnStackEdited(Entity<CEStatusEffectSeveredMemoriesComponent> ent,
        ref CEStatusEffectStackEditedEvent args)
    {
        LooseSkills(ent, args.NewStack, args.Target);

    }

    private void LooseSkills(Entity<CEStatusEffectSeveredMemoriesComponent> ent, int stack, EntityUid target)
    {
        if (ent.Comp.AbilityLoosingThreshold > stack)
            return;

        if (ent.Comp.LastLoosedAbilityStack is null)
        {
            ent.Comp.LastLoosedAbilityStack = stack;
            UnlearnRandomSkill(target);
            return;
        }

        var stackDelta = stack - ent.Comp.LastLoosedAbilityStack.Value;

        if (stackDelta <= ent.Comp.AbilityLoosingThreshold)
            return;

        for (var i = 0; i < stackDelta / ent.Comp.AbilityLoosingThreshold; i++)
        {
            UnlearnRandomSkill(target);
        }
    }

    private void UnlearnRandomSkill(EntityUid target)
    {
        if (!TryComp<CESkillStorageComponent>(target, out var storage))
            return;

        if (storage.LearnedSkills.Count == 0)
            return;

        var rand = new RobustRandom();
        rand.SetSeed((int)_timing.CurTick.Value);

        _skills.TryRemoveSkill(target, rand.Pick(storage.LearnedSkills), storage);
    }
}
