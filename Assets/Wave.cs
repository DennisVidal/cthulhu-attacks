using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveState {Inactive, Active, AllEnemiesSpawned, Ended};

public class Wave : MonoBehaviour
{

    public Vector3 m_spawnpoint = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 m_spawnnormal = new Vector3(0.0f, 1.0f, 0.0f);
    public WaveState m_waveState { get; protected set; }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartWave()
    {
        m_waveState = WaveState.Active;
    }

    public void SetSpawnPointAndNormal(Vector3 point, Vector3 normal)
    {
        m_spawnpoint = point;
        m_spawnnormal = normal;
    }

    public virtual void SpawnEnemy()
    {
    }

    public virtual void SetWaveNumber()
    {
    }
}
