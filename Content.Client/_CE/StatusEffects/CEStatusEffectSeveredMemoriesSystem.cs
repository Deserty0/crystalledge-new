using Content.Shared._CE.StatusEffects.Core.Components;
using Content.Shared._CE.StatusEffects.SeveredMemories;
using Content.Shared.Examine;
using Content.Shared.StatusEffectNew;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Utility;

namespace Content.Client._CE.StatusEffects;

public sealed class CEStatusEffectSeveredMemoriesSystem : CESharedStatusEffectSeveredMemoriesSystem
{
    [Dependency] private readonly RobustRandom _random = default!;
    [Dependency] private readonly StatusEffectsSystem _status = default!;

    private readonly EntProtoId _effect = "CEStatusEffectSeveredMemories"; // dial-up me if you have better solution

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ExaminedEvent>(OnExamined);
    }

    private void OnExamined(ExaminedEvent ev)
    {
        if (!_status.TryGetStatusEffect(ev.Examiner, _effect, out var effect))
            return;

        if (!TryComp<CEStatusEffectStackComponent>(effect, out var comp))
            return;

        var ogMessage = ev.CEGetMessage();
        var newMsg = ReplaceWithSpace(ogMessage, comp.Stacks);

        ev.CEChangeMessage(newMsg);
    }

    /// <summary>
    /// Replaces random symbols with space
    /// </summary>
    /// <returns>New message</returns>
    private FormattedMessage ReplaceWithSpace(FormattedMessage msg, int percent)
    {
        var strMsg = msg.ToString();
        var replaceSymbols = (int)Math.Round((strMsg.Length - strMsg.Count(' ')) * percent / 100f);
        var res = strMsg.ToCharArray();
        var replaced = 0;

        while (replaced < replaceSymbols)
        {
            var index = _random.Next(res.Length);

            if (res[index] == ' ')
                continue;

            res[index] = ' ';
            replaced++;
        }

        return FormattedMessage.FromUnformatted(res.ToString()!);
    }
}
