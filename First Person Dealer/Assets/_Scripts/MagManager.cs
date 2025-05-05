using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagManager : MonoBehaviour
{
    [SerializeField]
    private List<Card> magazine;
    [SerializeField]
    private List<Card> discard;
    [SerializeField]
    public int ammo;

    private void Awake()
    {
        magazine = new List<Card>();
        discard = new List<Card>();
        ammo = 0;
    }

    public void OnShoot()
    {
        discard.Add(magazine[0]);
        magazine.RemoveAt(0);
        ammo = magazine.Count;
    }

    public void OnShoot(EnemyBase enemy)
    {
        magazine[0].ApplyEffects(enemy);
        discard.Add(magazine[0]);
        magazine.RemoveAt(0);
        ammo = magazine.Count;
    }


    public void OnReload()
    {
        // TODO: Apply reload effects here
        discard.Clear();
        magazine = Shuffle(DeckManager.instance.deck);
        ammo = magazine.Count;
    }

    private List<Card> Shuffle(List<Card> list)
    {
        return list.OrderBy(x => Random.value).ToList();
    }
    
}
