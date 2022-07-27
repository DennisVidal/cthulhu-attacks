using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionWave : Wave 
{

    private int m_currentEnemyIndex = 0;
    private int m_currentEnemyCount = 0;
    public float m_spawnVelocity = 30.0f;
    public float m_enemySpawnDelayTime = 2.0f;
    private float m_enemySpawnDelayTimer = 0.0f;
    public float m_waveSpawnDelayTimer = 5.0f;
    
    public List<int> m_enemyCounts = new List<int>();
    public List<GameObject> m_enemyToSpawn = new List<GameObject>();
    public List<GameObject> m_spawnedEnemies { get; private set; }

    public static int m_minionWaveCount = 0;
    public int m_minionWaveNumber { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        m_waveState = WaveState.Inactive;
        m_spawnedEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_waveState == WaveState.Active)
        {
            if (m_waveSpawnDelayTimer > 0.0f)
            {
                m_waveSpawnDelayTimer -= Time.deltaTime;
            }
            else
            {
                if (m_enemySpawnDelayTimer > 0.0f)
                {
                    m_enemySpawnDelayTimer -= Time.deltaTime;
                }
                else
                {
                    m_enemySpawnDelayTimer = m_enemySpawnDelayTime;
                    SpawnEnemy();
                }
            }
        }
        else if (m_waveState == WaveState.AllEnemiesSpawned)
        {
            for (int i = 0; i < m_spawnedEnemies.Count; i++)
            {
                if (m_spawnedEnemies[i] == null)
                {
                    m_spawnedEnemies.RemoveAt(i);
                }
            }

            if (m_spawnedEnemies.Count == 0)
            {
                m_waveState = WaveState.Ended;
            }
        }
    }

    public override void SpawnEnemy()
    {
        m_spawnedEnemies.Add(Instantiate(m_enemyToSpawn[m_currentEnemyIndex], m_spawnpoint, Quaternion.LookRotation(m_spawnnormal, Vector3.up)));
        m_spawnedEnemies[m_spawnedEnemies.Count - 1].GetComponent<Rigidbody>().AddForce(m_spawnnormal * m_spawnVelocity);
        m_currentEnemyCount++;
        if (m_currentEnemyCount == m_enemyCounts[m_currentEnemyIndex])
        {
            m_currentEnemyCount = 0;
            m_currentEnemyIndex++;
            if (m_currentEnemyIndex == m_enemyToSpawn.Count)
            {
                m_waveState = WaveState.AllEnemiesSpawned;
            }
        }
    }

    public override void SetWaveNumber()
    {
        m_minionWaveCount++;
        m_minionWaveNumber = m_minionWaveCount;
    }
}
