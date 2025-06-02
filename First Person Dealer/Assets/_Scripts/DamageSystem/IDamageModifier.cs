public interface IDamageModifier
{
    float Priority { get; }  // 执行优先级
    int ModifyDamage(DamageContext context);
    string GetModifierName();
}