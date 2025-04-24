using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Card Data", menuName = "Card Data")]
public class CardDataSO: SerializedScriptableObject
{
    public Sprite image;
    public string cardName = "Card Name";
    [TextArea] public string description = "Description goes here.";
    public List<CardEffectBase> effects;
}
