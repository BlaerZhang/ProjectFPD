using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageModifierFactory
{
    public static List<IDamageModifier> GetApplicableModifiers(DamageContext context)
    {
        var applicableModifiers = new List<IDamageModifier>();
        
        // 1. 检查暴击
        if (context.IsCriticalHit)
        {
            applicableModifiers.Add(new CriticalHitModifier());
        }
        
        return applicableModifiers;
    }

}
