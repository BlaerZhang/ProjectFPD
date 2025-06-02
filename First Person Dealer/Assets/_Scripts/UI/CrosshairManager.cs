using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JoostenProductions;
using Events;
using DG.Tweening;
public class CrosshairManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image reloadIndicator;
    [SerializeField] private Image criticalHitIndicator;
    [SerializeField] private float criticalHitFadeOutDuration = 0.1f;
    [SerializeField] private float criticalHitShowDuration = 0.1f;
    void OnEnable()
    {
         // 订阅事件
        WeaponEvents.OnReloadStart += HandleReloadStart;
        WeaponEvents.OnReloadComplete += HandleReloadComplete;
        WeaponEvents.OnReloadCancelled += HandleReloadCancel;
        WeaponEvents.OnEnemyHit += HandleEnemyHit;
        WeaponEvents.OnCriticalHit += HandleCriticalHit;
    }

    void OnDisable()
    {
        WeaponEvents.OnReloadStart -= HandleReloadStart;
        WeaponEvents.OnReloadComplete -= HandleReloadComplete;
        WeaponEvents.OnReloadCancelled -= HandleReloadCancel;
        WeaponEvents.OnEnemyHit -= HandleEnemyHit;
        WeaponEvents.OnCriticalHit -= HandleCriticalHit;
    }

    private void Start()
    {
        // 确保初始状态是隐藏的
        if (reloadIndicator != null)
        {
            reloadIndicator.fillAmount = 0f;
        }
        if (criticalHitIndicator != null)
        {
            criticalHitIndicator.color = new Color(criticalHitIndicator.color.r, criticalHitIndicator.color.g, criticalHitIndicator.color.b, 0f);
        }
    }

    private void HandleReloadStart(float reloadTime)
    {
        if (reloadIndicator != null)
        {
            reloadIndicator.fillAmount = 0f;
            reloadIndicator.DOFillAmount(1f, reloadTime).SetEase(Ease.Linear);
        }
    }

    private void HandleReloadComplete()
    {
        if (reloadIndicator != null)
        {
            reloadIndicator.fillAmount = 1f;
            reloadIndicator.DOFillAmount(0f, 0.2f);
        }
    }

    private void HandleReloadCancel()
    {
        if (reloadIndicator != null)
        {
            reloadIndicator.DOKill();
            reloadIndicator.DOFillAmount(0f, 0.1f);
        }
    }

    private void HandleEnemyHit()
    {
        // 处理击中敌人事件
    }

    private void HandleCriticalHit()
    {
        criticalHitIndicator.DOKill();
        criticalHitIndicator.color = new Color(criticalHitIndicator.color.r, criticalHitIndicator.color.g, criticalHitIndicator.color.b, 1f);
        
        // 每次击中都重新开始计时
        criticalHitIndicator
            .DOFade(0f, criticalHitFadeOutDuration)
            .SetDelay(criticalHitShowDuration);
    }
}
