using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DeckManager : MonoBehaviour
{
    public static DeckManager instance;
    public int deckSize;
    public CardDataSO defaultCardData;
    public List<Card> deck;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetupDefaultDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupDefaultDeck()
    {
        deck = new List<Card>();
        for (int i = 0; i < deckSize; i++)
        {
            deck.Add(new Card(defaultCardData));
        }
    }

    public void DiscardCard(int index)
    {
        deck.RemoveAt(index);
    }

    public void AddCard(Card card)
    {
        deck.Add(card);
    }

    public void SwapCard(int index, Card newCard)
    {
        deck[index] = newCard;
    }
    
    [GUIColor(0.4f, 0.8f, 1f)]
    [Button("添加选中卡牌到牌组", ButtonSizes.Large)]
    public void AddCardFromCardData(CardDataSO cardData)
    {
        if (cardData == null)
        {
            Debug.LogError("请先选择一个有效的卡牌数据");
            return;
        }
        Card newCard = new Card(cardData);
        AddCard(newCard);
        Debug.Log($"已添加卡牌 [{cardData.cardName}] 到牌组");
    }
    
    
    [ButtonGroup("开发者工具")]
    [GUIColor(1f, 0.6f, 0.2f)]
    [Button("显示当前牌组信息", ButtonSizes.Medium)]
    [Tooltip("在控制台中显示当前牌组中的卡牌")]
    private void ShowDeckInfo()
    {
        if (deck == null || deck.Count == 0)
        {
            Debug.Log("牌组中没有卡牌");
            return;
        }
        
        Debug.Log($"===== 当前牌组信息 [{deck.Count}张牌] =====");
        Dictionary<string, int> cardCounts = new Dictionary<string, int>();
        
        foreach (Card card in deck)
        {
            string cardName = card.CardName;
            if (cardCounts.ContainsKey(cardName))
            {
                cardCounts[cardName]++;
            }
            else
            {
                cardCounts[cardName] = 1;
            }
        }
        
        foreach (var pair in cardCounts)
        {
            Debug.Log($"{pair.Key}: {pair.Value}张");
        }
    }
}
