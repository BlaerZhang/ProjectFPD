using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDamageEffect : CardEffectBase
{
    public int Damage = 5;
    public override void Apply(EnemyBase enemy)
    {
        Debug.Log("Basic Damage Effect with damage: " + Damage);
        enemy.TakeDamage(Damage);
    }
}
