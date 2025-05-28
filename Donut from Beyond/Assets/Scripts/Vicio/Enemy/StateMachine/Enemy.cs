using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;

[RequireComponent(typeof(Animator),typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private StateMachine<EnemyState, StateEvent> _enemyMachine;
    private Animator _animator;
    private NavMeshAgent _agent;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _enemyMachine = new StateMachine<EnemyState, StateEvent>();
        
        //_enemyMachine.AddState(EnemyState.Idle,new IdleState(false, true));
        //_enemyMachine.AddState(EnemyState.Chase, new ChaseState(true, false));
        //_enemyMachine.AddState(EnemyState.Attack, new AttackState(true, false));
       // _enemyMachine.AddState(EnemyState.Die);
       
       _enemyMachine.SetStartState(EnemyState.Idle);
       
       _enemyMachine.Init();
    }

    // Update is called once per frame
    void Update()
    {
        _enemyMachine.OnLogic();
    }
}
