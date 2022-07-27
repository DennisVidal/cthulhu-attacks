using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int m_damage = 10;
    public float m_lifetime = 10.0f;
    
    void Update()
    {
        //transform.position += m_speed * m_direction * Time.deltaTime;

        m_lifetime -= Time.deltaTime;
        if (m_lifetime < 0.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(m_damage);
            }
        }
        else if (col.gameObject.CompareTag("PlayerBody"))
        {
            Player player = GameManager.Instance.m_player.GetComponent<Player>();
            if (player != null)
            {
                if (player.m_playerBody == col.gameObject)
                {
                    player.TakeDamage(m_damage);
                }
            }
        }
        else if (col.gameObject.CompareTag("Boss"))
        {
            BossHealth bossHealth = col.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(m_damage);
            }
        }

        Destroy(gameObject);
    }
}

