using UnityEngine;

public class WANDERSteeringBehaviour : SteeringBehaviour
{
    public Vector3 m_offset= new Vector3(0.0f,0.0f,0.0f);
    public float m_radius = 2.0f;
    public float m_maxAngleChangeHorizontal = 45.0f;
    public float m_maxAngleChangeVertical = 45.0f;

    private float m_angleChangeHorizontal = 30.0f;
    private float m_angleChangeVertical = 70.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_enemy = gameObject.GetComponent<Enemy>();
        m_maxAngleChangeHorizontal *= Mathf.Deg2Rad;
        m_maxAngleChangeVertical *= Mathf.Deg2Rad;

        m_angleChangeHorizontal = Random.Range(0.0f,360.0f);
        m_angleChangeVertical = Random.Range(0.0f, 360.0f);
        m_angleChangeHorizontal *= Mathf.Deg2Rad;
        m_angleChangeVertical *= Mathf.Deg2Rad;
}
    

    public override Vector3 UpdateBehaviour()
    {
        Vector3 ergSteuerung = new Vector3(0.0f, 0.0f, 0.0f);

        m_angleChangeHorizontal += Random.Range(-1.0f, 1.0f) * m_maxAngleChangeHorizontal;
        m_angleChangeVertical += Random.Range(-1.0f, 1.0f) * m_maxAngleChangeVertical;
        Vector3 pointOnSphere = GetPointOnSphere(m_angleChangeHorizontal, m_angleChangeVertical);
        Vector3 m_position = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 sphereCenter = new Vector3(0.0f, 0.0f, 0.0f); //m_kinematik.orientierung * m_offset;
        
        m_position = sphereCenter + GetPointOnSphere(m_angleChangeHorizontal, m_angleChangeVertical);

        ergSteuerung = m_position * m_enemy.m_maxSteuerungsSpeed;

        ergSteuerung = ergSteuerung.normalized * m_enemy.m_maxSteuerungsSpeed;
        return ergSteuerung;
    }

    private Vector3 GetPointOnSphere(float angleHorizontal, float angleVertical)
    {
        //  x = r * cos(s) * sin(t)
        //  y = r * sin(s) * sin(t)
        //  z = r * cos(t)

        Vector3 pointOnSphere = new Vector3(0.0f, 0.0f, 0.0f);

        pointOnSphere.x = m_radius * Mathf.Cos(angleHorizontal) * Mathf.Sin(angleVertical);
        pointOnSphere.z = m_radius * Mathf.Sin(angleHorizontal) * Mathf.Sin(angleVertical);
        pointOnSphere.y = m_radius * Mathf.Cos(angleVertical);
        pointOnSphere.Normalize();
        return pointOnSphere;
    }
}
