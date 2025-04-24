using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase
{
    public int baseDamage = 0;
    public Sprite image;
    public string cardName = "Card Name";
    public string description = "Description goes here.";
    
    protected abstract int CalculateDamage();
    
    protected abstract void Effect();
}
