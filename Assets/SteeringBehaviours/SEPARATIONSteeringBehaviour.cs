using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SEPARATIONSteeringBehaviour : SteeringBehaviour
{
    public float m_activationDistance = 1.0f;
    public List<GameObject> m_targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        m_enemy = gameObject.GetComponent<Enemy>();
        m_targets = GameManager.Instance.m_enemyList;
    }
    

    public override Vector3 UpdateBehaviour()
    {
        Vector3 ergSteuerung = new Vector3(0.0f, 0.0f, 0.0f);

        if (m_targets.Count == 0) {
            return ergSteuerung;
        }

        for (int i = 0; i < m_targets.Count; i++) {
            if (m_targets[i] != gameObject) {
                Vector3 directionFromTarget = transform.position - m_targets[i].transform.position;
                float distance = directionFromTarget.magnitude;
                if (distance < m_activationDistance && distance != 0.0f)
                {
                    float force = m_enemy.m_maxSteuerungsSpeed * (m_activationDistance - distance) / m_activationDistance;
                    ergSteuerung += directionFromTarget.normalized * force;
                }
            }
        }

        ergSteuerung = ergSteuerung.normalized * m_enemy.m_maxSteuerungsSpeed;
        return ergSteuerung;
    }
}
