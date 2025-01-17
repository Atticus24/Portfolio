using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasing : MonoBehaviour
{
    public float chaseRange = 15f;
    public float stopDistance = 5f;
    public float fieldOfViewAngle = 110f;
    public float allyCheckRange = 10f;
    public float searchDuration = 5f;
    public float rotationSpeed = 2f;
    public float distanceToTarget;
    public float eyeHeight = 0.8f;
    public float reloadTime = 2f;
    public float timer = 0;
    public int Range = 100;
    public float maxWaitTime = 0.5f;

    public Transform firePoint;
    public int shotCount;
    public float deltaTime;
    public AudioClip gunFireSound;
    private AudioSource gunFire;
    public GameObject target;
    private EnemyStatistics enemyStats;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Vector3 lastKnownPosition;
    private bool isPlayerInSight;
    private bool isReloading;
    private bool isWithinShootingRange;
    public bool canShoot;
    private bool isColliding;
    public bool isDead;
    public PlayerHealth playerHealth;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<EnemyStatistics>();
        lastKnownPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player capsule");
        playerHealth = GameObject.FindGameObjectWithTag("Player capsule").GetComponent<PlayerHealth>();

        isReloading = false;
        isPlayerInSight = false;
        canShoot = true;
        isColliding = false;
        isDead = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
        StartCoroutine(ResetCollision());
    }

    IEnumerator ResetCollision()
    {
        yield return new WaitForSeconds(1f);
        isColliding = false;
    }

    void Update()
    {
        
        if (isColliding || isDead)
        {
            return;
        }

        if (enemyStats.health <= 0)
        {
            DeadNPC();
            return;
        }

        // Directly look at the target on the same horizontal plane
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (ShouldReload())
        {
            canShoot = false;
            StartCoroutine(Reload());
            return;
        }

        DetectPlayer();
        UpdateState();
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer = target.transform.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            Debug.DrawRay(transform.position + Vector3.up * eyeHeight, directionToPlayer.normalized * chaseRange, Color.red, 1.0f);
            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.up, directionToPlayer.normalized, out hit, chaseRange))
            {
                if (hit.collider.gameObject == target.gameObject && distanceToTarget < chaseRange)
                {
                    isPlayerInSight = true;
                    lastKnownPosition = target.transform.position;
                }
                else
                {
                    isPlayerInSight = false;
                }
            }
        }
        else
        {
            isPlayerInSight = false;
        }
    }

    void UpdateState()
    {
        if (isReloading)
            return;

        if (isPlayerInSight)
        {
            isWithinShootingRange = distanceToTarget <= stopDistance;
            if (isWithinShootingRange)
            {
                ShootAtPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            GoToIdleState();
        }
    }

    void ShootAtPlayer()
    {
        animator.SetBool("IsRunning", false);
        navMeshAgent.isStopped = true;

        if (canShoot)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                return;

            animator.SetTrigger("Shoot");
        }
    }

    void ChasePlayer()
    {
        // Directly look at the target on the same horizontal plane
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

        navMeshAgent.SetDestination(target.transform.position);
        navMeshAgent.isStopped = false;
        animator.SetBool("IsRunning", true);
    }

    void GoToIdleState()
    {
        animator.SetBool("IsRunning", false);
        navMeshAgent.isStopped = true;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            animator.ResetTrigger("Shoot");
        }
    }

    bool ShouldReload()
    {
        return enemyStats.currentAmmo <= 0 && !isReloading;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        print("Reloading...");

        animator.SetTrigger("Reload");

        yield return new WaitForSeconds(reloadTime);

        enemyStats.currentAmmo = enemyStats.maxAmmo;
        isReloading = false;
        canShoot = true;
        print("Finished reloading");
    }

    public void TryShoot()
    {
        if (enemyStats.currentAmmo > 0 && !isReloading)
        {
            npcShoot();
        }
    }

    private void npcShoot()
    {
        RaycastHit hit;
        Vector3 rayDirection = firePoint.TransformDirection(Vector3.forward);

        if (Physics.Raycast(firePoint.position, rayDirection, out hit, Range))
        {
            Debug.DrawRay(firePoint.GetComponent<Transform>().position, rayDirection * hit.distance, Color.yellow);
            print("Hit: " + hit.transform.name);

            GameObject fire = animator.gameObject.GetComponent<GameobjectsForUseInStateMachineScripts>().fire;
            GameObject hitPoint = animator.gameObject.GetComponent<GameobjectsForUseInStateMachineScripts>().hitPoint;

            fire.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
            hitPoint.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);

            GameObject Fireparticle = Instantiate(fire, firePoint.GetComponent<Transform>().position, Quaternion.identity);
            GameObject Hitparticle = Instantiate(hitPoint, hit.point, Quaternion.identity);

            gunFire = Fireparticle.AddComponent<AudioSource>();
            gunFire.clip = gunFireSound;
            gunFire.volume = 0.5f;
            gunFire.Play();

            if (hit.transform.CompareTag("Player capsule"))
            {

                print("Player was hit");
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                    
                }
                else
                {
                    Debug.LogError("playerStats is null. Make sure the player object with the 'Player' tag exists.");
                }
            }

            Destroy(Fireparticle, 1);
            Destroy(Hitparticle, 1);

            shotCount++;
            animator.GetComponent<EnemyStatistics>().currentAmmo--;
        }

        else
        {
            print("Missed!");
        }
    }

    public void DeadNPC()
    {
        if (!isDead)
        {
            isDead = true;
            animator.SetTrigger("Dead");
            Destroy(gameObject, 10f);
            // Add any additional logic you may need for the death state
        }
    }
}
    //private void RotateTowardsTarget()
   //{
        //if (isColliding)
        //{
           // print("Collision!");
          //  return;
        //}

    
        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
       // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    //}

