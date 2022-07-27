using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMYSteeringBehaviour : SteeringBehaviour
{
    public WANDERSteeringBehaviour m_WANDERSteeringBehaviour;
    public COHERSIONSteeringBehaviour m_COHERSIONSteeringBehaviour;
    public SEPARATIONSteeringBehaviour m_SEPARATIONSteeringBehaviour;
    public PlayerCOHERSIONSteeringBehaviour m_PlayerCOHERSIONSteeringBehaviour;
    public COLLISIONSteeringBehaviour m_COLLISIONSteeringBehaviour;

    public float m_weightWANDER = 1.0f;
    public float m_weightCOHERSION = 1.0f;
    public float m_weightSEPARATION = 1.0f;
    public float m_weightPlayerCOHERSION = 1.0f;
    public float m_weightCOLLISION = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_enemy = gameObject.GetComponent<Enemy>();
    }

    public override Vector3 UpdateBehaviour()
    {
        Vector3 ergSteuerung = new Vector3(0.0f, 0.0f, 0.0f);

        ergSteuerung += m_WANDERSteeringBehaviour.UpdateBehaviour() * m_weightWANDER;
        ergSteuerung += m_COHERSIONSteeringBehaviour.UpdateBehaviour()* m_weightCOHERSION;
        ergSteuerung += m_SEPARATIONSteeringBehaviour.UpdateBehaviour()* m_weightSEPARATION;
        ergSteuerung += m_PlayerCOHERSIONSteeringBehaviour.UpdateBehaviour() * m_weightPlayerCOHERSION;
        ergSteuerung += m_COLLISIONSteeringBehaviour.UpdateBehaviour() * m_weightCOLLISION;


        ergSteuerung = ergSteuerung.normalized * m_enemy.m_maxSteuerungsSpeed;
        return ergSteuerung;
    }
}
