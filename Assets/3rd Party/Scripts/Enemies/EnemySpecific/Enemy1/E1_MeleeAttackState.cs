using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MeleeAttackState : MeleeAttackState
{
    private Enemy1 enemy;
    private EnemyHealth enemyHealth;
    private Combat enemyCombat;

    public E1_MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData, Enemy1 enemy) : base(etity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
        this.enemyHealth = enemy.GetComponent<EnemyHealth>();
        this.enemyCombat = enemy.transform.Find("Core/Combat").GetComponent<Combat>();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        if(enemyCombat != null)
        {
            enemyCombat.canDealDamage = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // public override void TriggerAttack()
    // {
    //     base.TriggerAttack();
    // }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        
        // Check for player within attack radius
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        // Deal damage to each player hit by the attack
        foreach (Collider2D player in hitPlayers)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(stateData.attackDamage);
            }
        }
    }
}
