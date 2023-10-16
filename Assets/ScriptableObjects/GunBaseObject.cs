using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunBase", menuName = "TowerDefense/Gun")]
public class GunBaseObject : ScriptableObject
{
    [Header("Gun Characteristics")]
    public float LockOnRange = 10.0f;
    [Header("Waves")]
    public List<BulletWave> WavesPerCycle = new List<BulletWave>();
    [Header("Dynamic Values")]
    public int CurrentWave = 0;
    public float WaveTimer = 0.0f;
    public EnemyBase LockedOnTargetEnemy = null;

    public void Reset()
    {
        CurrentWave = 0;
        WaveTimer = 0.0f;
        foreach (BulletWave wave in WavesPerCycle)
        {
            wave.Reset();
        }
    }
}
