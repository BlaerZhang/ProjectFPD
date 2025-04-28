using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagManager : MonoBehaviour
{
    public List<Card> magazine;
    public List<Card> discard;
    public int ammo;

    private void Awake()
    {
        magazine = new List<Card>();
        discard = new List<Card>();
        ammo = 0;
    }

    public void OnShoot()
    {
        ammo = magazine.Count;
    }

    public void OnReload()
    {
        // TODO: Apply reload effects here
        discard.Clear();

        magazine = shuffle(DeckManager.instance.deck);
        ammo = magazine.Count;
    }

    private List<Card> shuffle(List<Card> list)
    {
        return list.OrderBy(x => Random.value).ToList();
    }
    
}
