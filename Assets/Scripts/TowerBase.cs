using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("Gun Data")]
    public List<GunBaseObject> Guns = new List<GunBaseObject>();
    [Header("Dynamic Values")]
    public int CurrentGun = 0;
    //public Vector3 FirePositionOffset = Vector3.zero;

    void Start()
    {
        foreach (GunBaseObject gun in Guns)
        {
            gun.Reset();
        }
    }

    public bool TryCalculateEnemyInRange()
    {
        bool returnValue = false;
        float shortestDist = float.MaxValue;
        EnemyBase lookAtEnemy = null;
        foreach(EnemyBase enemy in GameManager.GlobalGameManager.AllEnemies)
        {
            var distanceVector = enemy.transform.position - transform.position;
            if(distanceVector.magnitude > shortestDist)
            {
                lookAtEnemy = enemy;
                shortestDist = distanceVector.magnitude;
                foreach (GunBaseObject gun in Guns)
                {
                    if (gun.LockOnRange >= shortestDist)
                    {
                        gun.LockedOnTargetEnemy = enemy;
                        returnValue = true;
                    }
                }
            }
        }
        if(lookAtEnemy != null)
        {
            transform.LookAt(lookAtEnemy.transform, Vector3.up);
        }
        return returnValue;
    }
    void Update()
    {
        if(TryCalculateEnemyInRange())
        {
            foreach (GunBaseObject gun in Guns)
            {
                gun.WaveTimer += Time.deltaTime;
                if (gun.WaveTimer >= gun.WavesPerCycle[gun.CurrentWave].FireDelay)
                {
                    StartCoroutine(FireWave(gun));
                    gun.CurrentWave++;
                    if(gun.CurrentWave >= gun.WavesPerCycle.Count)
                    {
                        gun.CurrentWave = 0;
                    }
                    gun.WaveTimer -= gun.WavesPerCycle[gun.CurrentWave].FireDelay;
                }
            }
        }
    }
    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }
    private IEnumerator FireWave(GunBaseObject gun)
    {
        var currentWave = gun.WavesPerCycle[gun.CurrentWave];
        while (currentWave.Bullets.Count > currentWave.CurrentBullet)
        {
            var currentBullet = currentWave.Bullets[currentWave.CurrentBullet];
            currentWave.BulletDelayTimer += Time.deltaTime;

            if (currentWave.BulletDelayTimer >= currentBullet.FireDelay)
            {
                var bulletSpawn = GameManager.GlobalGameManager.SpawnObject(currentBullet.ModelKey, transform.position);
                var bulletComponent = bulletSpawn.GetComponent<BulletBase>();
                bulletComponent.Initialize(currentBullet);
                bulletComponent.MoveDirection = (gun.LockedOnTargetEnemy.transform.position - transform.position).normalized;
                currentWave.BulletDelayTimer -= currentBullet.FireDelay;
                currentWave.CurrentBullet++;
            }
            yield return null;
        }
    }
}
