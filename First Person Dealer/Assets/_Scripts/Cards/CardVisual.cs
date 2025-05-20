using System.Collections;
using System.Collections.Generic;
using JoostenProductions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardVisual : OverridableMonoBehaviour
{
    public CardView cardView;
    public Image CardImage;
    public TMP_Text CardNameText;
    public TMP_Text CardDescriptionText;

    public override void UpdateMe()
    {
        if (cardView == null) return;

        Vector3 targetPosition = cardView.transform.position;
        Vector3 currentPosition = transform.position;
        Vector3 rotationDelta = cardView.transform.position - transform.position;

        transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.unscaledDeltaTime * 10);
        transform.eulerAngles = new Vector3(0, 0, rotationDelta.x * 0.1f);
    }
}
