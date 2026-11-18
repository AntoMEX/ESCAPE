using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public int MAXHP => m_maxHP;
    protected int m_maxHP;
    public int CURHP => m_curHP;
    protected int m_curHP;

    public UnityEvent OnRecibeDamage;
    public UnityEvent OnDeath;
    protected virtual void Start()
    {
        if (m_maxHP <= 0)
        {
            m_maxHP = 100;
        }
        if (m_curHP != m_maxHP)
        {
            m_curHP = m_maxHP;
        }
    } 

    public void RecibeDamage(int amount)
    {
        m_curHP -= amount;
        if (m_curHP <= 0){
            m_curHP = 0;
            OnDeath.Invoke();
        }
        else
            OnRecibeDamage.Invoke();
    }

}