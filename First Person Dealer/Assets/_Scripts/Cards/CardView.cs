using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    private Card card;
    [SerializeField] private SpriteRenderer CardSpriteRenderer;
    [SerializeField] private TMP_Text CardNameText;
    [SerializeField] private TMP_Text CardDescriptionText;

    public void Initialize(Card card)
    {
        this.card = card;
        CardSpriteRenderer.sprite = card.CardSprite;
        CardNameText.text = card.CardName;
        CardDescriptionText.text = card.CardDescription;
    }

    private void OnMouseDown(){}
    private void OnMouseUp(){}
    private void OnMouseEnter(){}
    private void OnMouseExit(){}
    private void OnMouseDrag(){}
}
