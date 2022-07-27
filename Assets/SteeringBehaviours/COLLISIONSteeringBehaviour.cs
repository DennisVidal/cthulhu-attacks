using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COLLISIONSteeringBehaviour : SteeringBehaviour
{
    public float m_activationDistance = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_enemy = gameObject.GetComponent<Enemy>();
    }


    public override Vector3 UpdateBehaviour()
    {
        Vector3 ergSteuerung = new Vector3(0.0f, 0.0f, 0.0f);
        RaycastHit hitTarget;
        if (Physics.Raycast(transform.position, m_enemy.m_rigidbody.velocity, out hitTarget, m_activationDistance))
        {
            ergSteuerung = hitTarget.normal * m_enemy.m_maxSteuerungsSpeed;
        }

        ergSteuerung = ergSteuerung.normalized * m_enemy.m_maxSteuerungsSpeed;
        return ergSteuerung;
    }
}