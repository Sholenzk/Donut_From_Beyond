using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;

[RequireComponent(typeof(Animator),typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player_Movement playerMovement;
    
    [SerializeField] private PlayerSensor followPlayerSensor;
    [SerializeField] private PlayerSensor meleePlayerSensor;

    [SerializeField] private float attackCooldown = 1f;
    
    [SerializeField] private bool isInChaseRange;
    [SerializeField] private bool isInMeleeRange;
    [SerializeField] private float lastAttackTime;
    
    private StateMachine<EnemyState, StateEvent> _enemyMachine;
    private Animator _animator;
    private NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _enemyMachine = new StateMachine<EnemyState, StateEvent>();
        
        _enemyMachine.AddState(EnemyState.Idle,new IdleState(false, this));
        _enemyMachine.AddState(EnemyState.Chase, new ChaseState(true, this, playerMovement.transform));
        _enemyMachine.AddState(EnemyState.Attack, new AttackState(true, this, OnAttack));
        
        _enemyMachine.AddTriggerTransition(StateEvent.DetectPlayer, new Transition<EnemyState>(EnemyState.Idle, EnemyState.Chase));
        _enemyMachine.AddTriggerTransition(StateEvent.LostPlayer, new Transition<EnemyState>(EnemyState.Chase, EnemyState.Idle));
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Chase, transition => isInChaseRange && Vector3.Distance(playerMovement.transform.position, transform.position) > _agent.stoppingDistance));
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Idle, transition => !isInChaseRange || Vector3.Distance(playerMovement.transform.position, transform.position) <= _agent.stoppingDistance));
        
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Attack, ShouldMelee));
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Attack, ShouldMelee));
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Attack, EnemyState.Chase, IsNotWithinIdleRange));
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Attack, EnemyState.Idle, IsWithingIdleRange));
       
    //   _enemyMachine.SetStartState(EnemyState.Idle);
       
       _enemyMachine.Init();
    }

    private void Start()
    {
        followPlayerSensor.OnPlayerEnter += FollowPlayerSensor_OnPlayerEnter;
        followPlayerSensor.OnPlayerExit += FollowPlayerSensor_OnPlayerExit;
        meleePlayerSensor.OnPlayerEnter += MelleePlayerSensor_OnPlayerEnter;
        meleePlayerSensor.OnPlayerExit += MelleePlayerSensor_OnPlayerExit;
    }
    
    private bool ShouldMelee(Transition<EnemyState> transition) => lastAttackTime + attackCooldown <= Time.time && isInMeleeRange;

    private bool IsWithingIdleRange(Transition<EnemyState> transition) => _agent.remainingDistance <= _agent.stoppingDistance;
    private bool IsNotWithinIdleRange(Transition<EnemyState> transition) => !IsWithingIdleRange(transition);
    private void FollowPlayerSensor_OnPlayerExit(Vector3 lastKnownPosition) => isInChaseRange = false;
    private void FollowPlayerSensor_OnPlayerEnter(Transform player) => isInChaseRange = true;
    private void MelleePlayerSensor_OnPlayerExit(Vector3 lastKnownPosition) => isInMeleeRange = false;
    private void MelleePlayerSensor_OnPlayerEnter(Transform player) => isInMeleeRange = true;

    void OnAttack(State<EnemyState, StateEvent> state)
    {
        transform.LookAt(playerMovement.transform.position);
        lastAttackTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        _enemyMachine.OnLogic();
    }
}
