using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COHERSIONSteeringBehaviour : SteeringBehaviour
{
    public float m_activationDistance = 3.0f;
    public List<GameObject> m_targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_enemy = gameObject.GetComponent<Enemy>();
        m_targets = GameManager.Instance.m_enemyList;
    }
    
    public override Vector3 UpdateBehaviour()
    {
        Vector3 ergSteuerung = new Vector3();

        if (m_targets.Count == 0)
        {
            return ergSteuerung;
        }

        Vector3 schwerpunkt = new Vector3(0.0f, 0.0f, 0.0f);
        int activatingObjectCount = 0;
        for (int i = 0; i < m_targets.Count; i++)
        {
            if (m_targets[i] != gameObject)
            {
                Vector3 directionFromTarget = transform.position - m_targets[i].transform.position;
                float distance = directionFromTarget.magnitude;
                if (distance < m_activationDistance)
                {
                    schwerpunkt += m_targets[i].transform.position;
                    activatingObjectCount++;
                }
            }
        }
        if (activatingObjectCount > 0)
        {
            schwerpunkt /= activatingObjectCount;
            
            Vector3 direction = schwerpunkt - transform.position;
            float force = m_enemy.m_maxSteuerungsSpeed * direction.magnitude / m_activationDistance;
            direction = direction.normalized * force;
            ergSteuerung = direction - m_enemy.m_rigidbody.velocity;
        }

        ergSteuerung = ergSteuerung.normalized * m_enemy.m_maxSteuerungsSpeed;
        return ergSteuerung;
    }

}
