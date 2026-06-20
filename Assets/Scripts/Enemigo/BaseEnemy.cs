using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [Header("Base Stats")]
    public float walkSpeed = 3f;
    public float runSpeed = 4f;
    public int damage = 10;

    [Header("Vision Settings")]
    public float visionRange = 15f;
    public float visionAngle = 45f;
    public LayerMask obstacleLayer;
    public Transform player;

    [Header("Search and Memory")]
    public float searchTime = 5f;
    protected float searchTimer = 0f;
    protected bool isSearchingPlayer = false;
    protected Vector3 lastKnownPosition;

    [Header("Attack (Integrado)")]
    public float attackDistance = 3f; // Distancia para que el bicho frene y no te atraviese
    public float timeBetweenAttacks = 1.5f;
    protected float attackTimer = 0f;

    [Header("Fear Aura - Stamina Drain (Integrado)")]
    public float fearAuraRadius = 3f; // Distancia a la que te empieza a robar energía
    public float staminaDrainRate = 0.15f; // La velocidad de drenaje de tu compañero

    protected NavMeshAgent agent;
    protected bool seesPlayer = false;
    protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        attackTimer += Time.deltaTime;

        CheckVision();

        // Ejecutamos el drenaje de energía de tu compañero sin usar Triggers que rompan las físicas
        ApplyFearAura();

        if (seesPlayer)
        {
            isSearchingPlayer = true;
            searchTimer = searchTime;
            lastKnownPosition = player.position;

            Chase();
        }
        else if (isSearchingPlayer)
        {
            SearchLastPosition();
        }
        else
        {
            StopEnemy();
        }

        // Lógica de Animaciones
        if (agent != null && anim != null)
        {
            bool isMoving = !agent.isStopped && !agent.pathPending && agent.remainingDistance > agent.stoppingDistance;
            bool isWalking = isMoving && agent.speed == walkSpeed;
            bool isChasing = isMoving && agent.speed == runSpeed;

            anim.SetBool("Caminando", isWalking);
            anim.SetBool("Persiguiendo", isChasing);
        }
    }

    protected virtual void ApplyFearAura()
    {
        float currentDistance = Vector3.Distance(transform.position, player.position);

        if (currentDistance <= fearAuraRadius)
        {
            // La idea de tu compañero, adaptada a tu arquitectura
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.currentStamina -= staminaDrainRate * Time.deltaTime;
                playerController.currentStamina = Mathf.Clamp(playerController.currentStamina, 0, playerController.maxStamina);
            }
        }
    }

    protected virtual void Chase()
    {
        float currentDistance = Vector3.Distance(transform.position, player.position);

        if (currentDistance <= attackDistance)
        {
            // Clavamos los frenos para que no te atraviese
            agent.isStopped = true;

            // Lo hacemos rotar para que siempre mire al jugador mientras ataca
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);

            if (attackTimer >= timeBetweenAttacks)
            {
                Attack();
                attackTimer = 0f;
            }
        }
        else
        {
            agent.isStopped = false;
            agent.speed = runSpeed;
            agent.SetDestination(player.position);
        }
    }

    protected virtual void CheckVision()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 1.5f;
        Vector3 rayDestination = player.position + Vector3.up * 1.5f;
        Vector3 directionToPlayer = (rayDestination - rayOrigin).normalized;
        float distanceToPlayer = Vector3.Distance(rayOrigin, rayDestination);

        if (distanceToPlayer > visionRange)
        {
            seesPlayer = false;
            return;
        }

        bool wallBlocking = Physics.Raycast(rayOrigin, directionToPlayer, distanceToPlayer, obstacleLayer);

        if (!wallBlocking)
        {
            if (seesPlayer) return;

            float currentAngle = Vector3.Angle(transform.forward, (player.position - transform.position).normalized);
            if (currentAngle < visionAngle)
            {
                seesPlayer = true;
                return;
            }
        }

        seesPlayer = false;
    }

    protected virtual void SearchLastPosition()
    {
        agent.isStopped = false;
        agent.speed = walkSpeed;
        agent.SetDestination(lastKnownPosition);

        bool reachedDestination = agent.remainingDistance <= agent.stoppingDistance;
        bool pathBlocked = agent.pathStatus == NavMeshPathStatus.PathPartial;

        if (!agent.pathPending && (reachedDestination || pathBlocked))
        {
            searchTimer -= Time.deltaTime;

            if (searchTimer <= 0)
            {
                isSearchingPlayer = false;
            }
        }
    }

    protected virtual void StopEnemy()
    {
        agent.isStopped = true;
    }

    protected virtual void Attack()
    {
        // 1. Ejecutamos TU animación
        if (anim != null) anim.SetTrigger("Atacar");

        PlayerHealth healthScript = player.GetComponent<PlayerHealth>();

        if (healthScript != null)
        {
            // 2. Ejecutamos el daño estandarizado de tu compañero
            healthScript.TakeDamage(damage);
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        // Agregamos una esfera violeta para que veas el área del Aura de Miedo
        Gizmos.color = new Color(0.5f, 0f, 0.5f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, fearAuraRadius);

        Gizmos.color = Color.blue;
        Vector3 forward = transform.forward;
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle, 0) * forward;

        Gizmos.DrawRay(transform.position + Vector3.up * 1.5f, leftBoundary * visionRange);
        Gizmos.DrawRay(transform.position + Vector3.up * 1.5f, rightBoundary * visionRange);
    }
}