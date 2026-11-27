using UnityEngine;
using UnityEngine.AI;

public abstract class AEnemy : MonoBehaviour
{
    [Header("Components reference")]
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Transform player;
    [SerializeField] protected HealthComponent healthComp;
    public TacticBehaviour tacticBehaviour;
    [SerializeField] Animator animator;

    [Header("Values")]
    [SerializeField] protected float patrolSpeed = 2.5f;
    [SerializeField] protected float normalSpeed = 3.5f;
    [SerializeField] protected float attackDelay = 1f;
    [SerializeField] protected AudioClip hurtSound;
    [SerializeField] protected AudioClip deathSound;

    [Header("Debug")]
    [SerializeField] protected float elapsedAttack = 0f;
    [SerializeField] protected bool attacking = false;
    [SerializeField] protected bool alive = true;
    public Point goal;
    protected virtual void Start()
    {
        if (!healthComp) healthComp = GetComponent<HealthComponent>();
        if (!agent) agent = GetComponent<NavMeshAgent>();
        //movement = GetComponent<FPS_EnemyMovement>();
        if (!player) player = GameObject.FindWithTag("Player").transform;
        if (!player) Debug.LogError("Se te olvido asignar el player, baboso");

        if (healthComp)
        {
            healthComp.OnDeath.AddListener(this.OnDeath);
            healthComp.OnRecibeDamage.AddListener(this.OnRecibeDamage);
        }
    }
    public abstract void Attack();
    public abstract void Patrol();
    public abstract void Stalk();
    public abstract void Flee();
    public abstract void OnDeath();
    public abstract void OnRecibeDamage();
}
//Script original hecho por Diego Jiménez Cruzado, para el proyecto final de Inteligencia Artifical Avanzada