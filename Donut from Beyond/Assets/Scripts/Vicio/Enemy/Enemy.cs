using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;

[RequireComponent(typeof(Animator),typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerSensor followPlayerSensor;
    [SerializeField] private PlayerSensor meleePlayerSensor;

    [Header("Parametros")]
    [SerializeField] [Range(0.1f,5f)] private float attackCooldown = 2f;
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
        _enemyMachine = new ();
        
        _enemyMachine.AddState(EnemyState.Idle,new IdleState(false, this));
        _enemyMachine.AddState(EnemyState.Chase, new ChaseState(true, this, player.transform));
        _enemyMachine.AddState(EnemyState.Attack, new AttackState(true, this, OnAttack));
        
        // Chasing
        _enemyMachine.AddTriggerTransition(StateEvent.DetectPlayer, new Transition<EnemyState>(EnemyState.Idle, EnemyState.Chase));
        _enemyMachine.AddTriggerTransition(StateEvent.LostPlayer, new Transition<EnemyState>(EnemyState.Chase, EnemyState.Idle));
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Chase, transition => isInChaseRange && Vector3.Distance(player.transform.position, transform.position) > _agent.stoppingDistance));
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Idle, transition => !isInChaseRange || Vector3.Distance(player.transform.position, transform.position) <= _agent.stoppingDistance));
        
        // Attack
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Attack, ShouldMelee, forceInstantly:true));
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Attack, ShouldMelee, forceInstantly:true));
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Attack, EnemyState.Chase, IsNotWithinIdleRange));
        _enemyMachine.AddTransition(new Transition<EnemyState>(EnemyState.Attack, EnemyState.Idle, IsWithingIdleRange));
       
       _enemyMachine.SetStartState(EnemyState.Idle);
       
       _enemyMachine.Init();
    }

    private void Start()
    {
        followPlayerSensor.OnPlayerEnter += FollowPlayerSensor_OnPlayerEnter;
        followPlayerSensor.OnPlayerExit += FollowPlayerSensor_OnPlayerExit;
        meleePlayerSensor.OnPlayerEnter += MeleePlayerSensor_OnPlayerEnter;
        meleePlayerSensor.OnPlayerExit += MeleePlayerSensor_OnPlayerExit;
    }
    
    private void FollowPlayerSensor_OnPlayerExit(Vector3 lastKnownPosition)
    {
        Debug.Log("player exited");
        _enemyMachine.Trigger(StateEvent.LostPlayer);
        isInChaseRange = false;
    }
    private void FollowPlayerSensor_OnPlayerEnter(Transform player)
    {
        Debug.Log("Player entered");
        _enemyMachine.Trigger(StateEvent.DetectPlayer);
        isInChaseRange = true;
    }
    
    private bool ShouldMelee(Transition<EnemyState> transition) => lastAttackTime + attackCooldown <= Time.time && isInMeleeRange;
    private bool IsWithingIdleRange(Transition<EnemyState> transition) => _agent.remainingDistance <= _agent.stoppingDistance;
    private bool IsNotWithinIdleRange(Transition<EnemyState> transition) => !IsWithingIdleRange(transition);
    private void MeleePlayerSensor_OnPlayerExit(Vector3 lastKnownPosition) => isInMeleeRange = false;
    private void MeleePlayerSensor_OnPlayerEnter(Transform player)
    {
        Debug.Log("Starting attack");
        isInMeleeRange = true;
    }
/*
    public void Attack()
    {
        Debug.Log("Ola k ase atacando o k ase");
    }

    public void FinishAttack()
    {
        Debug.Log("Deje de atacar");
    }
   */ 
    void OnAttack(State<EnemyState, StateEvent> state)
    {
        transform.LookAt(player.transform.position);
        lastAttackTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        _enemyMachine.OnLogic();
    }
}
