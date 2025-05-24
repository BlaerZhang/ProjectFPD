using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Card card;
    private CardVisual cardVisual;
    [SerializeField] private GameObject CardVisualPrefab;
    public CardVisual InitializeAndCreateCardVisual(Card card)
    {
        this.card = card;
        cardVisual = Instantiate(CardVisualPrefab, transform).GetComponent<CardVisual>();
        cardVisual.cardView = this;
        cardVisual.CardImage.sprite = card.CardSprite;
        cardVisual.CardNameText.text = card.CardName;
        cardVisual.CardDescriptionText.text = card.CardDescription;
        return cardVisual;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 处理鼠标按下事件
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 处理鼠标松开事件
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardVisual.transform.DOScale(1.1f, 0.1f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardVisual.transform.DOScale(1f, 0.1f).SetUpdate(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cardVisual.transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }
}
