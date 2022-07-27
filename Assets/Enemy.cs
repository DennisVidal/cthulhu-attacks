using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody m_rigidbody { get; private set; }
    public int m_health = 100;
    public float m_maxMovementSpeed = 0.2f;
    public float m_maxSteuerungsSpeed = 2f;
    private Vector3 m_steuerung = new Vector3();
    public SteeringBehaviour m_currentBehaviour;

    private bool m_isSpawning = true;
    private bool m_hasSpawnCollision = false;
    public float m_spawnTimer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        GetComponent<Collider>().isTrigger = true;
        GameManager.Instance.AddEnemyToList(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_isSpawning)
        {
            if(m_hasSpawnCollision == false)
            {
                if (m_spawnTimer <= 0.0f)
                {
                    GetComponent<Collider>().isTrigger = false;
                }
                m_spawnTimer -= Time.deltaTime;
            }
        }
        //update position
        if (m_currentBehaviour != null) {
            m_steuerung = m_currentBehaviour.UpdateBehaviour();
            if (m_steuerung.magnitude > m_maxSteuerungsSpeed)
            {
                m_steuerung = m_steuerung.normalized * m_maxSteuerungsSpeed;
            }
            if (m_rigidbody.velocity.magnitude > m_maxMovementSpeed)
            {
                m_rigidbody.velocity = m_rigidbody.velocity.normalized * m_maxMovementSpeed;
            }

            m_rigidbody.AddForce(m_steuerung*100*Time.deltaTime);
        }
        //rotation to player
        transform.rotation = Quaternion.LookRotation(GameManager.Instance.m_player.GetComponent<Player>().m_playerBody.transform.position- transform.position, Vector3.up);
    }

    public void TakeDamage(int amount)
    {
        m_health -= amount;
        if (m_health <= 0) {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        GameManager.Instance.RemoveEnemyFromList(gameObject); //remove enemy from enemylist
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 31)
        {
            ContactPoint[] cp = collision.contacts;
            m_rigidbody.velocity = cp[0].normal* 10 * Time.deltaTime;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 31)
        {
            ContactPoint[] cp = collision.contacts;
            m_rigidbody.velocity += cp[0].normal * m_maxSteuerungsSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_isSpawning)
        {
            m_hasSpawnCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<Collider>().isTrigger = false;
    }
}
