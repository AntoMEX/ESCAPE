using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public float MAXHP => m_maxHP;
    protected float m_maxHP;
    public float CURHP => m_curHP;
    protected float m_curHP;

    public UnityEvent OnRecibeDamage;
    public UnityEvent OnDeath;
    protected virtual void Start()
    {
        if (m_maxHP <= 0f)
        {
            m_maxHP = 100f;
        }
        if (m_curHP != m_maxHP)
        {
            m_curHP = m_maxHP;
        }
    } 

    public void RecibeDamage(float amount)
    {
        m_curHP -= amount;
        if (m_curHP <= 0f){
            m_curHP = 0f;
            OnDeath.Invoke();
        }
        else
            OnRecibeDamage.Invoke();
    }
}