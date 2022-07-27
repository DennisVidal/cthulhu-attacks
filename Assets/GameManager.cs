using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public List<GameObject> m_enemyList = new List<GameObject>();
    public GameObject m_player;

    public bool m_bSpawnWaves = false;
    public List<Wave> m_waveList;
    public int m_currentWaveIndex = 0;

    public int m_maxBossHealth = 0;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        BossHealth.SetMaxHealth(m_maxBossHealth); //set max boss health
        for(int i = 0; i < m_waveList.Count; i++) //assign wave numbers
        {
            m_waveList[i].SetWaveNumber();
        }
    }

    void Update()
    {
        if (m_bSpawnWaves)
        {
            if (m_waveList[m_currentWaveIndex].m_waveState==WaveState.Inactive) //if current wave not yet started
            {
                Vector3 spawnpoint = new Vector3(0.0f, 0.0f, 0.0f); //Set Spawnpoint
                Vector3 spawnnormal = new Vector3(0.0f, 1.0f, 0.0f); //Set Spawnnormal
                m_waveList[m_currentWaveIndex].SetSpawnPointAndNormal(spawnpoint, spawnnormal);
                m_waveList[m_currentWaveIndex].StartWave();
            }
            else if (m_waveList[m_currentWaveIndex].m_waveState == WaveState.Ended) //if current wave ended
            {
                if (m_currentWaveIndex < m_waveList.Count - 1) //if still waves left
                {
                    m_currentWaveIndex++; //increase index for next wave
                }
                else //if all waves killed
                {
                    OnWin();
                }
            }
        }
    }

    public void AddEnemyToList(GameObject enemy) //add enemy to enemy list
    {
        m_enemyList.Add(enemy);
    }
    public void RemoveEnemyFromList(GameObject enemy) //remove enemy from enemy list
    {
        m_enemyList.Remove(enemy);
    }

    public void OnWin()
    {
        Debug.Log("Game won");
    }
}


