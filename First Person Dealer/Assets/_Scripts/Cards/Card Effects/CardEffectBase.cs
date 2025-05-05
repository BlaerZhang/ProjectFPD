using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CardEffectBase
{
    public abstract void Apply(EnemyBase enemy);
}
