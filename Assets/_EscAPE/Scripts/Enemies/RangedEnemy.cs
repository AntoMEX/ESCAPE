using UnityEngine;
using System.Collections;

public class RangedEnemy : AFuzzyEnemy
{
    public float AttackRange;
    [SerializeField] protected Gun myGun;
    public float badAquracymultiplyer = 5f;
    protected override void Start()
    {
        base.Start();
        alive = true;
        StartCoroutine(BehaviourEvaluationRoutine());
    }

    private void Update()
    {
        if (attacking)
        {
            elapsedAttack += Time.deltaTime;
            transform.LookAt(player.position);
            if (elapsedAttack >= attackDelay)
            {
                attackDelay = Random.Range(1f, 2.5f);
                elapsedAttack = 0f;
                Debug.Log("intento atacar");
                Attack();
            }
        }
    }

    // Update is called once per frame
    void EvaluateBehaviour()
    {
        float distToPlayer = Vector3.Distance(player.position, transform.position);

        float ruleStalkStrong = AND(HighHealth(healthComp.CURHP), CloseToPlayer(distToPlayer));
        float ruleStalkkMedium = AND(MediumHealth(healthComp.CURHP), CloseToPlayer(distToPlayer));
        float ruleFlee = AND(LowHealth(healthComp.CURHP), CloseToPlayer(distToPlayer));
        float rulePatrol = AND(HighHealth(healthComp.CURHP), FarToPlayer(distToPlayer));

        float stalk = (ruleStalkStrong * 1f) + (ruleStalkkMedium * 0.6f);
        float flee = ruleFlee * 0.7f;
        float patrol = rulePatrol * 1f;

        if (stalk > flee && stalk > patrol) Stalk();
        else if (flee > patrol) Flee();
        else Patrol();
    }
    public override void Stalk()
    {
        transform.LookAt(player.position);
        attacking = true;

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
    public override void Flee()
    {
        attacking = false;
        if (goal == null)
        {
            goal = tacticBehaviour.GetBestPoint(EnemyState.Fleeing);
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
    override public void Attack()
    {
        Vector3 shootDir = player.position - transform.position;
        shootDir.y *= Random.Range(-badAquracymultiplyer, badAquracymultiplyer);
        shootDir.z *= Random.Range(-badAquracymultiplyer, badAquracymultiplyer);

        myGun.DoAction();
    }
    override public void Patrol()
    {
        attacking = false;
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
    public override void OnRecibeDamage()
    {
        AudioManager.instance.PlaySound(hurtSound);
        EvaluateBehaviour();
    }
    public override void OnDeath()
    {
        alive = false;
        AudioManager.instance.PlaySound(deathSound);
        StopAllCoroutines();
        Object.Destroy(this.gameObject);
    }

    IEnumerator BehaviourEvaluationRoutine()
    {
        while (alive)
        {
            EvaluateBehaviour();
            float randDelay = 3f + Random.Range(0f, 2f); //de 3 a 5 segundos
            yield return new WaitForSeconds(randDelay);
        }
    }
}
//Script original hecho por Diego Jiménez Cruzado, para el proyecto final de Inteligencia Artifical Avanzada