using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageCalculator : MonoBehaviour
{ 
    public int CalculateDamage(DamageContext context)
    {
        // 根据当前上下文动态获取适用的修正器
        var applicableModifiers = DamageModifierFactory.GetApplicableModifiers(context);
        
        int damage = context.CardEffectBaseDamage;
        
        // 只计算真正适用的修正器
        foreach (var modifier in applicableModifiers.OrderBy(m => m.Priority))
        {
            float oldDamage = damage;
            damage = modifier.ModifyDamage(context);
            Debug.Log($"{modifier.GetModifierName()}: {oldDamage:F1} -> {damage:F1}");
        }
        
        return damage;
    }
}