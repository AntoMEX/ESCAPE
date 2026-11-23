using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class MeleeEnemy : AEnemy
{
    [SerializeField] float chaseSpeed;

    [Header("Ranges")]
    [SerializeField] float detectionRange;
    [SerializeField] float chaseRange;
    [SerializeField] float attackRange;
    [SerializeField] float lowHealthVal;

    private Coroutine attackCooldownRoutine;

    private void Update()
    {
        if (!PlayerInDetectRange())
            Patrol();
        else if (IsLowHealth() && PlayerInChaseRange())
            Chase();
        else if (!IsLowHealth())
            Stalk();
    }
    bool IsLowHealth()
    {
        return healthComp.CURHP <= lowHealthVal;
    }
    bool PlayerInDetectRange()
    {
        return Vector3.Distance(player.position, transform.position) < detectionRange;
    }
    bool PlayerInChaseRange()
    {
        return Vector3.Distance(player.position, transform.position) < chaseRange;
    }
    public override void Patrol()
    {
        if (goal == null)
        {
            goal = PointsBoard.Instance.ProvideRandomPoint();
            agent.SetDestination(goal.myTransform.position);
            agent.speed = patrolSpeed;
        }
        if (goal != null)
        {
            if (!agent.pathPending && agent.remainingDistance < 1.5f)
            {
                PointsBoard.Instance.UnBannPoint(goal);
                goal = null;
            }
        }
    }
    public override void Stalk()
    {
        transform.LookAt(player.position);
        if (goal == null)
        {
            goal = tacticBehaviour.GetBestPoint(EnemyState.Stalking);
            agent.SetDestination(goal.myTransform.position);
            agent.speed = normalSpeed;

        }
        if (goal != null)
        {
            if (!agent.pathPending && agent.remainingDistance < 1.5f)
            {
                PointsBoard.Instance.UnBannPoint(goal);
                goal = null;
            }
        }
    }
    public void Chase()
    {
        transform.LookAt(player.position);

        agent.SetDestination(player.position);
        agent.speed = chaseSpeed;
        if (Vector3.Distance(player.position, transform.position) < attackRange && !attacking)
            Attack();

    }
    public override void Attack()
    {
        //cosas de atacar
        Debug.Log("Ataque");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.Normalize(player.position - transform.position), out hit, attackRange))
        {
            HealthComponent hpcomp = hit.collider.GetComponent<HealthComponent>();
            if (hpcomp != null)
            {
                hpcomp.RecibeDamage(30);
            }
        }

        attacking = true;
        attackCooldownRoutine = StartCoroutine(AttackDelayRoutine());
    }
    public override void OnRecibeDamage()
    {
        Debug.Log("Auch");
    }
    public override void OnDeath()
    {
        alive = false;
        if(attackCooldownRoutine != null) 
            StopCoroutine(attackCooldownRoutine);
        Object.Destroy(this.gameObject);
    }
    public override void Flee() { }

    IEnumerator AttackDelayRoutine()
    {
        yield return new WaitForSeconds(attackDelay + Random.Range(0f, 1f));
        attacking = false;
    }
}
//Script original hecho por Diego Jiménez Cruzado, para el proyecto final de Inteligencia Artifical Avanzada