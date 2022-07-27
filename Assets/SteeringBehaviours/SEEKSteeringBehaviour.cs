using UnityEngine;

public class SEEKSteeringBehaviour : SteeringBehaviour
{
    public GameObject m_target;

    void Start()
    {
        m_enemy = gameObject.GetComponent<Enemy>();
    }


    public override Vector3 UpdateBehaviour()
    {
        Vector3 ergSteuerung = new Vector3(0.0f, 0.0f, 0.0f);
        if (m_target == null)
        {
            return ergSteuerung;
        }
        ergSteuerung = m_target.transform.position - transform.position;
        return ergSteuerung;
    }
}
