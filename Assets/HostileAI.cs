using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HostileAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Layers")]
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private LayerMask playerLayerMask;

    [Header("Patrol Settings")]
    [SerializeField] private float patrolRadius = 10f;
    private Vector3 currentPatrolPoint;
    private bool hasPatrolPoint;

    [Header("Combat settings")]
    [SerializeField] private float attackCooldown = 1f;
    private bool isOnAttackCooldown;
    [SerializeField] private float forwardShotForce = 10f;
    [SerializeField] private float verticalShotForce = 5f;

    [Header("Detection Ranges")]
    [SerializeField] private float visionRange = 20f;
    [SerializeField] private float engagementRange = 10f;

    private bool isPlayerVisible;
    private bool isPlayerInRange;
    private Vector3 lastKnownPosition;
    private bool wasPlayerSeen = false; // the AI didn't see the player yet
    Vector3 directionToPlayer;

    private void Awake()
    {
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }

        if (navMesh == null)
        {
            navMesh = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        //Debug.Log(wasPlayerSeen);
        DetectPlayer();
        UpdateBehaviourState();
    }
    private void OnDrawGizmosSelected()
    {
        directionToPlayer = (playerTransform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, visionRange*5))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, hit.point);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, hit.point);
            }
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, engagementRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);        
    }
    private void DetectPlayer()
    {
        directionToPlayer = (playerTransform.position - transform.position).normalized;
        //Raycast allows the bot to "see" the player and ignore him when he has hidden
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, visionRange*5))
        {
            if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Player")
            {
                isPlayerVisible = Physics.CheckSphere(transform.position, visionRange, playerLayerMask);
                isPlayerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask);
                lastKnownPosition = playerTransform.position; // Enemy remembers Player's last place
                wasPlayerSeen = true;
                
            }
            else
            {
                isPlayerVisible = false;
                isPlayerInRange = false;
                //don't forget lastknownposition yet
                //Debug.Log("collider: " + hit.collider.name + " layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
            }
        }


    }

    private void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;

        Rigidbody projectileRb = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        projectileRb.AddForce(transform.forward * forwardShotForce, ForceMode.Impulse);
        projectileRb.AddForce(transform.up * verticalShotForce, ForceMode.Impulse);

        Destroy(projectileRb.gameObject, 3f);
    }

    private void FindPatrolPoint()
    {
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);

        Vector3 potentialPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(potentialPoint, -transform.up, 2f, terrainLayer))
        {
            currentPatrolPoint = potentialPoint;
            hasPatrolPoint = true;
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }

    private void PerformPatrol()
    {
        if (wasPlayerSeen)
        {
            // We don't see the player, let's check his last known position
            navMesh.SetDestination(lastKnownPosition);
            
            // If we didn't find him, forget him
            if (Vector3.Distance(transform.position, lastKnownPosition) < 1f)
            {
                wasPlayerSeen = false; 
                //Debug.Log("I've lost him!");
            }
        }
        else
        {
            //Debug.Log("Patrolling..");
            if (!hasPatrolPoint) FindPatrolPoint();

            if (hasPatrolPoint) navMesh.SetDestination(currentPatrolPoint);

            if (Vector3.Distance(transform.position, currentPatrolPoint) < 1f) hasPatrolPoint = false;
        }

    }

    private void PerformChase()
    {
        //Debug.Log("Chasing..");
        if (playerTransform != null) navMesh.SetDestination(playerTransform.position);

    }

    private void PerformAttack()
    {
        //Debug.Log("Attacking..");
        navMesh.SetDestination(transform.position);

        if (playerTransform != null) transform.LookAt(playerTransform);

        if(!isOnAttackCooldown)
        {
            FireProjectile();
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private void UpdateBehaviourState()
    {
        if (!isPlayerVisible && !isPlayerInRange) PerformPatrol();
        else if (isPlayerVisible && !isPlayerInRange) PerformChase();
        else if (isPlayerVisible && isPlayerInRange) PerformAttack();
    }
}