using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DamageNumbersPro;
public class EnemyBase : MonoBehaviour
{
    public int health;
    public int maxHealth;
    private MeshRenderer meshRenderer;
    public DamageNumber damagePopUpPrefab;

    private void Start()
    {
        health = maxHealth;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void OnHit()
    {
        // Hit Feedback
        meshRenderer.transform.DOPunchScale(Vector3.one * 0.1f, 0.1f);
        meshRenderer.material.DOColor(Color.red, 0.1f).SetEase(Ease.Flash, 10, 2);
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        // Damage Feedback
        DamageNumber damageNumber = damagePopUpPrefab.Spawn(transform.position, damage);
        damageNumber.SetFollowedTarget(transform);
        damageNumber.SetSpamGroup(gameObject.GetInstanceID().ToString());
        
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
}
