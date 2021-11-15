using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField]
    internal float health;

    internal static int damage = 10;

    private Transform player;

    [SerializeField]
    private LayerMask isGround, isPlayer;

    public bool pauseNow;

    internal bool playerHunter;

    [SerializeField]
    private float sightRange, attackRange;

    private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    private void Start()
    {
        PlayerControls.OnPlayerHunter += CheckHunter;
        GameManager.OnPauseNow += CheckPause;
    }

    private void OnDestroy()
    {
        PlayerControls.OnPlayerHunter -= CheckHunter;
        GameManager.OnPauseNow -= CheckPause;
    }

    private void CheckPause(bool pause)
    {
        if (pause)
        {
            pauseNow = true;
            agent.enabled = false;
        }
        else
        {
            pauseNow = false;
            agent.enabled = true;
        }
    }

    private void CheckHunter(bool hunter)
    {
        if (hunter)
            playerHunter = true;
        else
            playerHunter = false;
    }

    private void Update()
    {
        if (!pauseNow)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        if (playerHunter)
            agent.SetDestination(-player.position);
        else
            agent.SetDestination(player.position);
    }
}
