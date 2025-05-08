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
        // 处理关键部位击中事件
    }
}
