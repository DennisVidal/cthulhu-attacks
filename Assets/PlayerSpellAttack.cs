using UnityEngine;

public class PlayerSpellAttack : MonoBehaviour
{
    public int m_damage = 50;
    public float m_projectileSpeed = 10.0f;
    public float m_maxRange = 10.0f;

    public bool m_isOnCooldown = false;
    public float m_castCooldownTime = 2.0f;
    private float m_castCooldownTimer = 0.0f;

    public Camera m_camera;
    public GameObject m_origin;
    public GameObject m_projectile;

    // Start is called before the first frame update
    void Start()
    {
        if (m_camera == null)
        {
            m_camera = Camera.main;
        }

        m_castCooldownTimer = m_castCooldownTime;
    }

    void Update()
    {
        UpdateCooldown();
    }

    void UpdateCooldown()
    {
        if (m_isOnCooldown)
        {
            m_castCooldownTimer -= Time.deltaTime;
            if (m_castCooldownTimer <= 0.0f)
            {
                m_isOnCooldown = false;
                m_castCooldownTimer = m_castCooldownTime;
            }
        }
    }

    public void CastSpell()
    {
        if (!m_isOnCooldown)
        {
            SpawnSpellProjectile();
            m_isOnCooldown = true;
        }
    }

    void SpawnSpellProjectile()
    {
        if (m_origin != null)
        {
            if (m_projectile != null)
            {
                GameObject projectile = Instantiate(m_projectile, m_origin.transform.position, Quaternion.identity);
                RaycastHit hitTarget;
                Vector3 hitLocation;
                if (Physics.Raycast(m_camera.transform.position, m_camera.transform.forward, out hitTarget, m_maxRange))
                {
                    hitLocation = hitTarget.point;
                }
                else
                {
                    hitLocation = m_camera.transform.position + m_camera.transform.forward * m_maxRange;
                }

                Projectile projectileData = projectile.GetComponent<Projectile>();
                if (projectileData != null)
                {
                    Vector3 force = (hitLocation - m_origin.transform.position).normalized * m_projectileSpeed;
                    projectile.GetComponent<Rigidbody>().AddForce(force);
                    projectileData.m_damage = m_damage;
                    projectileData.m_lifetime = 10.0f;
                }
            }
        }
    }
}