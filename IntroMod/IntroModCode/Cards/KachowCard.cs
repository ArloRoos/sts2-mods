using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.ValueProps;

namespace IntroMod.IntroModCode.Cards;

[Pool(typeof(IroncladCardPool))]
public sealed class KachowCard() : CustomCardModel(0, CardType.Attack, CardRarity.Basic, TargetType.AllEnemies)
{
    public override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(1000m, ValueProp.Move)];

    public override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .TargetingAllOpponents(base.CombatState)
            .WithAttackerAnim(
                Ironclad.GetHeavyAnimIfApplicable(base.Owner.Character),
                Ironclad.GetHeavyAttackDelayIfApplicable(base.Owner.Character))
            .WithHitFx("vfx/vfx_heavy_blunt", null, "heavy_attack.mp3")
            .WithHitVfxSpawnedAtBase()
            .Execute(choiceContext);
    }

    public override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(1000m);
    }
}