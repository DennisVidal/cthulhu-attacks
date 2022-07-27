using UnityEngine;

public class BossWave : Wave {

    public GameObject m_bossPrefab;
    public float m_waveSpawnDelayTimer = 5.0f;
    public GameObject m_spawnedBoss { get; private set; }

    public static int m_bossWaveCount=0;
    public int m_bossWaveNumber = -1;

    // Use this for initialization
    void Start ()
    {
        m_waveState = WaveState.Inactive;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_waveState == WaveState.Active)
        {
            if (m_waveSpawnDelayTimer > 0.0f)
            {
                m_waveSpawnDelayTimer -= Time.deltaTime;
            }
            else
            {
                SpawnEnemy();
            }
        }

        if(m_waveState == WaveState.AllEnemiesSpawned)
        {
            if (m_spawnedBoss.GetComponent<BossHealth>().m_reachedThreshold)
            {
                m_waveState = WaveState.Ended;
                DestroyObject(m_spawnedBoss);
            }
        }
    }


    public override void SpawnEnemy()
    {
        m_spawnedBoss = Instantiate(m_bossPrefab, m_spawnpoint, Quaternion.LookRotation(m_spawnnormal, Vector3.up)); //spawn boss
        BossHealth bossHealthComponent = m_spawnedBoss.GetComponent<BossHealth>(); //get health component
        if (bossHealthComponent)
        {
            if (bossHealthComponent.m_healthThreshold == -1) //set threshold if it wasn't set manually
            {
                float multiplier = ((float)m_bossWaveCount - (float)m_bossWaveNumber) / (float)m_bossWaveCount;
                int threshold = (int)(multiplier * (float)BossHealth.m_maxHealth);
                bossHealthComponent.SetHealthThreshold(threshold);
            }
        }

        m_waveState = WaveState.AllEnemiesSpawned;
    }

    public override void SetWaveNumber() //increase total boss wave count and set wave number
    {
            m_bossWaveCount++;
            m_bossWaveNumber = m_bossWaveCount;
    }
}
