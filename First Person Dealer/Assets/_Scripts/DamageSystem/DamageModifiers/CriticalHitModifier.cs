using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalHitModifier : IDamageModifier
{
    public float Priority => 100f; // 高优先级，最后应用
    
    public int ModifyDamage(DamageContext context)
    {
        if (context.IsCriticalHit)
        {
            return Mathf.RoundToInt(context.CardEffectBaseDamage * context.CriticalMultiplier);
        }
        return context.CardEffectBaseDamage;
    }
    
    public string GetModifierName() => "Critical Hit";
}
