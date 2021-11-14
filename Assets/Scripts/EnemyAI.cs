using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    [SerializeField]
    private Transform player;

    [SerializeField]
    LayerMask isGround, isPlayer;

    [SerializeField]
    private float health = 100f;

    public static int damage = 9;

    public bool pauseNow;

    private bool playerHunter;

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

    private void CheckPause(bool pause)
    {
        if (pause)
            pauseNow = true;
        else
            pauseNow = false;
    }

    private void OnDestroy()
    {
        PlayerControls.OnPlayerHunter -= CheckHunter;
        GameManager.OnPauseNow -= CheckPause;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && playerHunter)
            TakeDamage(PlayerControls.attack);
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Destroy(gameObject);
    }
}
