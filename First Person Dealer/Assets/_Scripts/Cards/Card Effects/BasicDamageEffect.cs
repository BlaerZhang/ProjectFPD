using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDamageEffect : CardEffectBase
{
    public int Damage = 5;
    private int finalDamage;
    public override void Apply(DamageContext damageContext)
    {
        Debug.Log("Basic Damage Effect with damage: " + Damage);
        damageContext.CardEffectBaseDamage = Damage;
        finalDamage = GameManager.Instance.damageCalculator.CalculateDamage(damageContext);
        damageContext.Target.TakeDamage(finalDamage);
    }
}
