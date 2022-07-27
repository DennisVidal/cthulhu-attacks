using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellAttack : MonoBehaviour
{
    public int m_damage = 50;
    public float m_projectileSpeed = 10.0f;
    public float m_maxRange = 10.0f;

    public bool m_isOnCooldown = false;
    public float m_minCastCooldownTime = 5.0f;
    public float m_maxCastCooldownTime = 10.0f;
    private float m_castCooldownTimer = 0.0f;

    public GameObject m_origin;
    public GameObject m_projectile;
    

    void Update()
    {
        UpdateCooldown();
        CastSpell();
    }

    void UpdateCooldown()
    {
        if (m_isOnCooldown)
        {
            m_castCooldownTimer -= Time.deltaTime;
            if (m_castCooldownTimer <= 0.0f)
            {
                m_isOnCooldown = false;
            }
        }
    }

    void CastSpell()
    {
        if (!m_isOnCooldown)
        {
            SpawnEnemyProjectile();
            m_castCooldownTimer = Random.Range(m_minCastCooldownTime, m_maxCastCooldownTime);
            m_isOnCooldown = true;
        }
    }

    void SpawnEnemyProjectile()
    {
        if (m_origin != null)
        {
            if (m_projectile != null)
            {
                GameObject projectile = Instantiate(m_projectile, m_origin.transform.position, Quaternion.identity);
                Projectile projectileData = projectile.GetComponent<Projectile>();
                if (projectileData != null)
                {
                    Vector3 force = (GameManager.Instance.m_player.GetComponent<Player>().m_playerBody.transform.position - m_origin.transform.position).normalized * m_projectileSpeed;
                    projectile.GetComponent<Rigidbody>().AddForce(force);
                    projectileData.m_damage = m_damage;
                    projectileData.m_lifetime = 10.0f;
                }
            }
        }
    }
}
