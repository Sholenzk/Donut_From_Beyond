using System;
using UnityHFSM;

public class AttackState : EnemyStateBase
{
    public AttackState(bool needsExitTime, Enemy enemy, Action<State<EnemyState, StateEvent>> onEnter,
        float ExitTime = 0.33f) : base(needsExitTime, enemy, ExitTime, onEnter)
    { }

    public override void OnEnter()
    {
        Agent.isStopped = true;
        base.OnEnter();
        Animator.Play("Atack");
    }
}
