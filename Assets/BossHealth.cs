using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

    public static int m_maxHealth { get; private set; }
    public static int m_currentHealth;//{ get; private set; }
    private static bool m_isHealthSet = false;
    public static bool m_isBossDead { get; private set; }
    public bool m_reachedThreshold = false;
    public int m_healthThreshold = -1;


    // Use this for initialization
    void Start () {
        if (!m_isHealthSet)
        {
            m_isHealthSet = true;
            m_maxHealth = GameManager.Instance.m_maxBossHealth;
            m_currentHealth = m_maxHealth;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int damage)
    {
        if (m_currentHealth > m_healthThreshold) //if threshold isn't reached yet
        {
            m_currentHealth -= damage;
        }

        if (m_currentHealth <= m_healthThreshold)//if threshold is reached
        {
            m_currentHealth = m_healthThreshold;
            m_reachedThreshold = true;
        }

        if (m_currentHealth <= 0)
        {
            m_isBossDead = true;
        }
    }

    public void SetHealthThreshold(int threshold)
    {
        m_healthThreshold = threshold;
    }
    public static void SetMaxHealth(int maxHealth)
    {
        m_maxHealth = maxHealth;
    }
}
