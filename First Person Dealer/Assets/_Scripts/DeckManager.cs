using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
