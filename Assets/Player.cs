using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class Player : MonoBehaviour, IInputClickHandler
{
    public int m_health = 100;
    public GameObject m_playerBody;
    public PlayerSpellAttack m_currentAttack;

    
    void Start()
    {
        m_currentAttack = GetComponent<PlayerSpellAttack>();
        InputManager.Instance.PushModalInputHandler(this.gameObject);
    }
    
    void Update()
    {
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (m_currentAttack != null)
        {
            m_currentAttack.CastSpell();
        }
    }

    public void TakeDamage(int amount)
    {
        m_health -= amount;
        if (m_health <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        //switch to endscreen
        Debug.Log("player died");
    }
}
