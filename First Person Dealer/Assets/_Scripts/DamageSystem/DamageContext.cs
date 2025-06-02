using System.Collections.Generic;

public class DamageContext
{
    // 基础信息
    public Card SourceCard { get; set; }          // 造成伤害的卡牌
    public EnemyBase Target { get; set; }          // 目标敌人
    public int CardEffectBaseDamage = 0;          // 卡牌效果产生的基础伤害值
    
    // 击中信息
    public bool IsCriticalHit = false;        // 是否暴击
    public float CriticalMultiplier = 2.0f;  // 暴击倍数
    
    // 弹夹上下文
    public List<Card> PreviousCards { get; set; }  // 之前打出的牌
    public Card LastCard { get; set; }             // 上一张牌
}