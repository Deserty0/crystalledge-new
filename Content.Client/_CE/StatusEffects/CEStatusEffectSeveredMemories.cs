using Content.Shared._CE.StatusEffects.Core;
using Content.Shared._CE.StatusEffects.SeveredMemories;
using Robust.Client.GameObjects;

namespace Content.Client._CE.StatusEffects;

public sealed class CEStatusEffectSeveredMemories : CESharedStatusEffectSeveredMemoriesSystem
{
    [Dependency] private readonly SpriteSystem _sprite = default!;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CEStatusEffectSeveredMemoriesComponent, CEStatusEffectStackEditedEvent>(OnStackChanged);
    }

    private void OnStackChanged(Entity<CEStatusEffectSeveredMemoriesComponent> ent,
        ref CEStatusEffectStackEditedEvent args)
    {
        if (args.NewStack < ent.Comp.FadingThreshold)
            return;

        if (!TryComp<SpriteComponent>(args.Target, out var sprite))
            return;

        var stackDelta = args.NewStack - ent.Comp.FadingThreshold;
        var color = sprite.Color;
        color.A = (byte)(255 - 255*(stackDelta / 100f));

        _sprite.SetColor(args.Target, color);
    }
}
