using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

// [RequireComponent(typeof(Animator))]
public class AssaultRifleDeck : MonoBehaviour
{
    [SerializeField]
    private Camera fPDCamera;
    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem MuzzleFlashParticle;
    [SerializeField]
    private Transform BulletSpawnPoint;
    [SerializeField]
    private ParticleSystem ImpactParticle;
    [SerializeField]
    private GameObject cardTrail;
    [SerializeField]
    private float ShootDelay = 0.5f;
    [SerializeField]
    private LayerMask Mask;
    [SerializeField]
    private float BulletSpeed = 100;

    private Animator Animator;
    private float LastShootTime;

    private void Awake()
    {
        // Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0)) Shoot();
    }

    public void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time)
        {
            // Animator.SetBool("IsShooting", true);
            // MuzzleFlashParticle.Play();
            Vector3 direction = GetDirection();

            if (Physics.Raycast(fPDCamera.transform.position, direction, out RaycastHit hit, float.MaxValue, Mask))
            {
                GameObject trail = Instantiate(cardTrail, BulletSpawnPoint.position, Quaternion.LookRotation(direction,Vector3.up));

                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                LastShootTime = Time.time;
            }
            // this has been updated to fix a commonly reported problem that you cannot fire if you would not hit anything
            else
            {
                GameObject trail = Instantiate(cardTrail, BulletSpawnPoint.position, Quaternion.LookRotation(direction,Vector3.up));

                StartCoroutine(SpawnTrail(trail, BulletSpawnPoint.position + GetDirection() * 100, Vector3.zero, false));

                LastShootTime = Time.time;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = fPDCamera.transform.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator SpawnTrail(GameObject Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {
        // This has been updated from the video implementation to fix a commonly raised issue about the bullet trails
        // moving slowly when hitting something close, and not
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }
        // Animator.SetBool("IsShooting", false);
        Trail.transform.position = HitPoint;
        if (MadeImpact)
        {
            // Instantiate(ImpactParticle, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, 10);
    }
}