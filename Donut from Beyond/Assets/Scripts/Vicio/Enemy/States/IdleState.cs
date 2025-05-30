using UnityEngine;

public class IdleState : EnemyStateBase
{
    private float AnimationLooCount = 0;
    public IdleState(bool needsExitTime, Enemy enemy) : base(needsExitTime, enemy)
    { }

    public override void OnEnter()
    {
        base.OnEnter();
        Agent.isStopped = false;
        Animator.Play("Idle");
    }

}
