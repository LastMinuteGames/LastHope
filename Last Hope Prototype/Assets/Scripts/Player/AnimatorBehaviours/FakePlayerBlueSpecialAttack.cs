using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerBlueSpecialAttack : PlayerBaseAttackState
{
    protected override void LoadStateSettings()
    {
        attackName = "Blue";
        AudioSources.instance.PlaySound((int)AudiosSoundFX.Player_Combat_ChargeBlueAttack);
    }
}
