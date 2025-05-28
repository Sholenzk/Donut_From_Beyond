using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;

public class EnemyStateBase : State<EnemyState, StateEvent>
{ 
    /*
    protected readonly Enemy Enemy;
    protected readonly Animator Animator;
    protected readonly NavMeshAgent Agent;
    protected bool RequestedExit;
    protected float ExitTime;
    
    protected readonly Action<State<EnemyState,StateEvent>> OnEnter;
    protected readonly Action<State<EnemyState,StateEvent>> OnLogic;
    protected readonly Action<State<EnemyState,StateEvent>> OnExit;
    protected readonly Action<State<EnemyState,StateEvent>, bool> canExit;

    public EnemyStateBase(bool needsExitTime, Enemy enemy, float exitTime = 0.1f,
        Action<State<EnemyState, StateEvent>> OnEnter = null, Action<State<EnemyState, StateEvent>> OnLogic = null,
        Action<State<EnemyState, StateEvent>> OnExit = null, Action<State<EnemyState, StateEvent>, bool> canExit = null)
    {
        this.Enemy = Enemy;
        this.OnEnter = OnEnter;
        this.OnLogic = OnLogic;
        this.OnExit = OnExit;
        this.canExit = canExit;
        this.ExitTime = exitTime;
        this.needsExitTime = needsExitTime;
        Agent = Enemy.GetComponent<NavMeshAgent>();
        Animator = Enemy.GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        RequestedExit = false;
        OnEnter?.Invoke(this);
    }

    public override void OnLogic()
    {
        base.OnLogic();
        if (RequestedExit && timer.Elapsed >= ExitTime)
        {
            fsm.StateCanExit();
        }
    }

    public override void OnExitRequest()
    {
        if (!needsExitTime || canExit != null && canExit(this))
        {
            fsm.StateCanExit();
        }
        else
        {
            RequestedExit = true;
        }
    }
    
    */
}
