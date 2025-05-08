using System;
using UnityEngine;

namespace Events
{
    /// <summary>
    /// 武器系统的事件中心，处理所有武器相关的事件通知
    /// </summary>
    public static class WeaponEvents
    {
        // 开始换弹事件，参数为换弹所需时间
        public static event Action<float> OnReloadStart;
        
        // 换弹完成事件
        public static event Action OnReloadComplete;
        
        // 换弹被取消事件
        public static event Action OnReloadCancelled;

        // 击中敌人事件
        public static event Action OnEnemyHit;

        // 关键部位击中事件
        public static event Action OnCriticalHit;

        // 弹药状态变化事件 (当前弹药量, 最大弹药量)
        public static event Action<int, int> OnAmmoChanged;

        // 触发开始换弹事件
        public static void TriggerReloadStart(float reloadTime)
        {
            OnReloadStart?.Invoke(reloadTime);
        }

        // 触发换弹完成事件
        public static void TriggerReloadComplete()
        {
            OnReloadComplete?.Invoke();
        }

        // 触发换弹取消事件
        public static void TriggerReloadCancel()
        {
            OnReloadCancelled?.Invoke();
        }

        // 触发击中敌人事件
        public static void TriggerEnemyHit()
        {
            OnEnemyHit?.Invoke();
        }

        // 触发关键部位击中事件
        public static void TriggerCriticalHit()
        {
            OnCriticalHit?.Invoke();
        }

        // 触发弹药变化事件
        public static void TriggerAmmoChanged(int currentAmmo, int maxAmmo)
        {
            OnAmmoChanged?.Invoke(currentAmmo, maxAmmo);
        }
    }
} 