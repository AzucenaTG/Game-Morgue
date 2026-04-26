using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [Header("Base Stats")]
    public float health = 100f;
    public float walkSpeed = 3f; // Velocidad para patrullar/buscar
    public float runSpeed = 4f;  // Velocidad para perseguir a muerte
    public int damage = 10;

    [Header("Vision Settings")]
    public float visionRange = 15f;
    public float visionAngle = 45f;
    public LayerMask obstacleLayer;
    public Transform player;

    [Header("Search and Memory")]
    public float searchTime = 5f; // Cuánto tiempo se queda buscando
    protected float searchTimer = 0f;
    protected bool isSearchingPlayer = false;
    protected Vector3 lastKnownPosition;

    [Header("Attack")]
    public float attackDistance = 1f;
    public float timeBetweenAttacks = 1.5f;
    protected float attackTimer = 0f;

    [Header("Animations and Pauses")]
    public float roarTime = 2.5f; // Tiempo que dura el rugido
    protected bool isRoaring = false;
    protected float roarTimer = 0f;

    protected NavMeshAgent agent;
    protected bool seesPlayer = false;
    protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        if (isRoaring)
        {
            roarTimer -= Time.deltaTime;
            agent.isStopped = true;

            if (agent != null && anim != null)
            {
                anim.SetFloat("VelX", 0f);
                anim.SetFloat("VelZ", 0f);
            }

            if (roarTimer <= 0)
            {
                isRoaring = false;
            }
            return;
        }

        attackTimer += Time.deltaTime;

        CheckVision();

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

        if (agent != null && anim != null)
        {
            Vector3 localVelocity = Vector3.zero;

            if (!agent.isStopped && !agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
            {
                localVelocity = transform.InverseTransformDirection(agent.desiredVelocity);
            }

            anim.SetFloat("VelX", localVelocity.x);
            anim.SetFloat("VelZ", localVelocity.z);
        }
    }

    protected virtual void Chase()
    {
        agent.isStopped = false;
        agent.speed = runSpeed;
        agent.SetDestination(player.position);

        float currentDistance = Vector3.Distance(transform.position, player.position);

        if (currentDistance <= attackDistance)
        {
            if (attackTimer >= timeBetweenAttacks)
            {
                Attack();
                attackTimer = 0f;
            }
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
                if (!isSearchingPlayer)
                {
                    if (anim != null) anim.SetTrigger("SawPlayer");
                    isRoaring = true;
                    roarTimer = roarTime;
                }

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
        Debug.Log("¡El enemigo te atacó!");
        if (anim != null) anim.SetTrigger("Attack");

        // Buscamos el script de salud 
        PlayerHealth healthScript = player.GetComponent<PlayerHealth>();

        if (healthScript != null)
        {
            // Le restamos el daño del enemigo a la vida actual del jugador
            healthScript.currentHealth -= damage;

            // Para que la vida no baje de cero
             //if (healthScript.currentHealth < 0): Le pregunta a la computadora: "¿La vida del jugador se pasó para abajo del cero?".
              //healthScript.currentHealth = 0;: Si la respuesta es "sí", la computadora lo corrige a la fuerza y lo deja clavado en 0.
            
            if (healthScript.currentHealth < 0) healthScript.currentHealth = 0;

            Debug.Log("Vida restante del jugador: " + healthScript.currentHealth);
        }
        else
        {
            Debug.LogWarning("No se encontró el script PlayerHealth en el objeto asignado.");
        }
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (anim != null) anim.SetTrigger("TakeDamage");

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("El enemigo ha muerto");
        if (anim != null) anim.SetBool("IsDead", true);

        agent.isStopped = true;

        // Chequear si tiene collider antes de apagarlo
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        this.enabled = false;
    }

    //  Esto dibuja líneas en la escena para que configures los rangos visualmente
    protected virtual void OnDrawGizmosSelected()
    {
        // Dibuja el rango de visión (Amarillo)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        // Dibuja el rango de ataque (Rojo)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        // Dibuja las líneas del ángulo de visión (Azul)
        Gizmos.color = Color.blue;
        Vector3 forward = transform.forward;
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle, 0) * forward;

        Gizmos.DrawRay(transform.position + Vector3.up * 1.5f, leftBoundary * visionRange);
        Gizmos.DrawRay(transform.position + Vector3.up * 1.5f, rightBoundary * visionRange);
    }
}