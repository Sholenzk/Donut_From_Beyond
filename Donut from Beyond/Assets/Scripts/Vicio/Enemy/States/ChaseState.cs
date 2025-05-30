using UnityEngine;

public class ChaseState : EnemyStateBase
{
    
    private readonly Transform _target;
    public ChaseState(bool needsExitTime, Enemy enemy, Transform target) : base(needsExitTime, enemy)
    {
        this._target = target;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Agent.enabled = false;
        Agent.isStopped = false;
        Animator.Play("Idle");
    }

    public override void OnLogic()
    {
        base.OnLogic();
        if (!RequestedExit)
        {
            Agent.SetDestination(_target.position);
        }
        else if (Agent.remainingDistance <= Agent.stoppingDistance)
        {
            fsm.StateCanExit();
        }
    }
}
