using UnityEngine;

public class PlayerCOHERSIONSteeringBehaviour : SteeringBehaviour
{
    public float m_minDistance = 1.0f;
    public float m_maxDistance = 4.0f;
    public float m_maxHeightDifference = 1.0f;
    public Camera m_target;

    // Start is called before the first frame update
    void Start()
    {
        m_enemy = gameObject.GetComponent<Enemy>();
        if (m_target == null)
        {
            m_target = Camera.main;
        }
    }

    public override Vector3 UpdateBehaviour()
    {
        Vector3 ergSteuerung = new Vector3(0.0f, 0.0f, 0.0f);

        if (m_target == null)
        {
            return ergSteuerung;
        }

        Vector3 directionToPlayer = m_target.transform.position - transform.position;
        float distance = directionToPlayer.magnitude;
        float force = 0.0f;

        if (distance > m_maxDistance)
        {
            force = m_enemy.m_maxSteuerungsSpeed * (distance / m_maxDistance);
            ergSteuerung = directionToPlayer.normalized * force - m_enemy.m_rigidbody.velocity;
        }
        else if (distance < m_minDistance)
        {
            force = m_enemy.m_maxSteuerungsSpeed / (distance / m_minDistance);
            ergSteuerung = -1.0f*directionToPlayer.normalized * force - m_enemy.m_rigidbody.velocity;
        }

        float differenceY = transform.position.y - m_target.gameObject.transform.position.y;
        bool bAbove = true;
        if (differenceY < 0.0f) {
            differenceY *= -1;
            bAbove = false;
        }
        if (differenceY > m_maxHeightDifference) {
            if (bAbove)
            {
                ergSteuerung += new Vector3(0.0f, -1.0f, 0.0f);
            }
            else {
                ergSteuerung += new Vector3(0.0f, 1.0f, 0.0f);
            }
        }

        ergSteuerung = ergSteuerung.normalized * m_enemy.m_maxSteuerungsSpeed;
        return ergSteuerung;
    }

}
