using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerHealthManager : HealthComponent
{
    [Header("Healing")]
    public UnityEvent OnHeal;
    public UnityEvent OnStartHealing;
    [SerializeField] private float timeToStartHeal = 2.5f;
    [SerializeField] private float timeBetweenHeals = 0.15f;
    [SerializeField] private int healAmount = 10;

    bool healRutineActive = false;
    Coroutine healRutine;

    [SerializeField] const bool DEBUG_LIFE = true;
    protected override void Start()
    {
        base.Start();
        OnRecibeDamage.AddListener(RestartHealing);
        if (DEBUG_LIFE) OnRecibeDamage.AddListener(DamageDebug);
    }

    private void Update()
    {
        if (DEBUG_LIFE)
        {
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                Debug.Log("DETECTO INPUT");
                RecibeDamage(10);
            }
        }
    }
    void RestartHealing()
    {
        if (healRutineActive)
        {
            healRutineActive = false;
            StopCoroutine(healRutine);
        }

        healRutine = StartCoroutine(HealRutine());
    }
    void Heal(int _healAmount)
    {
        m_curHP += _healAmount;
        if (m_curHP > m_maxHP)
            m_curHP = m_maxHP;
        if (DEBUG_LIFE) Debug.Log("Healeed to: " + CURHP.ToString());
        OnHeal.Invoke();
    }

    public IEnumerator HealRutine()
    {
        healRutineActive = true;
        yield return new WaitForSeconds(timeToStartHeal);
        OnStartHealing.Invoke();
        while (CURHP < MAXHP)
        {
            yield return new WaitForSeconds(timeBetweenHeals);
            Heal(healAmount);
        }
        healRutineActive = false;
    }

    public void DamageDebug()
    {
        Debug.Log("Life at: " + CURHP.ToString());
    }
}
