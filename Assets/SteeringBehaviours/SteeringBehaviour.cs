using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    protected Enemy m_enemy;

    public virtual Vector3 UpdateBehaviour()
    {
        return new Vector3(0.0f, 0.0f, 0.0f);
    }
}