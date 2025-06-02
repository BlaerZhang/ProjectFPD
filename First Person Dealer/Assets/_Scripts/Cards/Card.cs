using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    private readonly CardDataSO cardData;

    public Card(CardDataSO cardData)
    {
        this.cardData = cardData;
        CardDescription = cardData.description;
        CardEffects = cardData.effects;
    }

    public Sprite CardSprite => cardData.image;
    public string CardName => cardData.cardName;
    public string CardDescription { get; private set; }
    public List<CardEffectBase> CardEffects { get; private set; }

    public void ApplyEffects(DamageContext damageContext)
    {
        foreach (var effect in CardEffects)
        {
            effect.Apply(damageContext);
        }
    }
    
}
