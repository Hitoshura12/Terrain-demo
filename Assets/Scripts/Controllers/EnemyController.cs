using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, ITakeDamage
{
    NavMeshAgent enemyNav;
    Animator enemyAnimator;
    GameObject player;
   public Enemy_SO stats;
    public float aggroRange=10f;
    public float attackRange;
   public List<Transform> waypoints;
    public float patrolTime = 10f;
    
    float speed, agentSpeed;
    int waypointIndex;
    
    // Start is called before the first frame update
    private void Awake()
    {
        enemyNav = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
         player = GameObject.FindGameObjectWithTag("Player");
      
        attackRange = aggroRange / 2;
       waypointIndex = Random.Range(0, waypoints.Count);
        agentSpeed = enemyNav.speed;
        InvokeRepeating("Tread", 1f, 1.2f);
        PlayerController.OnPlayerHit += Attack;
        if (waypoints.Count> 0)
        {
            InvokeRepeating("Scout", Random.Range(0, patrolTime), patrolTime);
        }
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerHit -= Attack;
    }
    // Update is called once per frame
    private void Update()
    {
        speed = Mathf.Lerp(speed, enemyNav.velocity.magnitude, Time.deltaTime * 10);
        enemyAnimator.SetFloat("Speed", speed);            

    }

    void Scout()
    {
        waypointIndex = waypointIndex == waypoints.Count - 1 ? 0 : waypointIndex +1;
    }

    void Tread()
    {
        enemyNav.SetDestination(waypoints[waypointIndex].position);
        enemyNav.speed = agentSpeed / 2;
        //enemyAnimator.SetBool("move_forward", true);
        if (player!=null && Vector3.Distance(transform.position, player.transform.position) < aggroRange)
        {
            enemyAnimator.SetBool("move_forward_fast", true);
            enemyAnimator.SetBool("move_forward", false);
            enemyNav.speed = agentSpeed;
            enemyNav.destination = player.transform.position;
            Attack(stats.attackPower);
        }
        else if (player != null && Vector3.Distance(transform.position, player.transform.position) > aggroRange)
        {

            enemyAnimator.SetBool("move_forward_fast", false);
            enemyAnimator.SetBool("move_forward",true);
           
        }
    }

    private void OnDrawGizmos()
    {
        Color color = new Color(234f,0,15f,0.2f);
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, aggroRange);
       
    }
    
    public void Attack(int dmg)
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            enemyAnimator.SetTrigger("attack_range");
            player.GetComponent<PlayerController>().playerStats.Health -= dmg;
         
           
        }
    }

    public void TakeDamage(int dmgTaken)
    {
        throw new System.NotImplementedException();
    }
}
