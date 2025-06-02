using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Events;

public class MagManager : MonoBehaviour
{
    #region Properties
    // 当前弹药量
    public int CurrentAmmo => magazine.Count;
    // 最大弹药量（与牌组大小同步）
    public int MaxAmmo => DeckManager.instance.deck.Count;
    // 弹夹是否已满
    public bool IsMagFull => CurrentAmmo >= MaxAmmo;
    // 是否有弹药
    public bool HasAmmo => CurrentAmmo > 0;
    #endregion

    [Header("卡牌管理")]
    [SerializeField] private List<Card> magazine = new List<Card>();  // 当前弹夹
    [SerializeField] private List<Card> discard = new List<Card>();   // 弃牌堆

    private void Awake()
    {
        magazine = new List<Card>();
        discard = new List<Card>();
    }

    private void OnEnable()
    {
        // 订阅牌组变化事件（如果你之后实现了这个事件）
        DeckManager.OnDeckChanged += HandleDeckChanged;
    }

    private void OnDisable()
    {
        DeckManager.OnDeckChanged -= HandleDeckChanged;
    }

    /// <summary>
    /// 处理普通射击
    /// </summary>
    public void OnShoot()
    {        
        discard.Add(magazine[0]);
        magazine.RemoveAt(0);
        
        // 发送弹药状态变化事件
        WeaponEvents.TriggerAmmoChanged(CurrentAmmo, MaxAmmo);
    }

    /// <summary>
    /// 处理击中敌人的射击
    /// </summary>
    public void OnShoot(DamageContext damageContext)
    {
        // 设置damageContext
        damageContext.SourceCard = magazine[0];
        damageContext.PreviousCards = discard;
        damageContext.LastCard = discard.Count > 0 ? discard.Last() : null;
        
        magazine[0].ApplyEffects(damageContext);
        discard.Add(magazine[0]);
        magazine.RemoveAt(0);
        
        // 发送弹药状态变化事件
        WeaponEvents.TriggerAmmoChanged(CurrentAmmo, MaxAmmo);
    }

    /// <summary>
    /// 处理换弹操作
    /// </summary>
    public void OnReload()
    {
        // 清空弃牌堆
        discard.Clear();
        
        // 从牌组获取新的弹药
        magazine = Shuffle(DeckManager.instance.deck);
        
        // 发送弹药状态变化事件
        WeaponEvents.TriggerAmmoChanged(CurrentAmmo, MaxAmmo);
    }

    /// <summary>
    /// 洗牌算法
    /// </summary>
    private List<Card> Shuffle(List<Card> list)
    {
        return list.OrderBy(x => Random.value).ToList();
    }

    /// <summary>
    /// 处理牌组变化事件
    /// </summary>
    private void HandleDeckChanged()
    {
        // 如果需要，在这里处理牌组变化后的逻辑
        // 例如：更新UI、检查弹药状态等
        WeaponEvents.TriggerAmmoChanged(CurrentAmmo, MaxAmmo);
    }
}
