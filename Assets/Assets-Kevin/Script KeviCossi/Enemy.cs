using UnityEngine;
using UnityEngine.AI;

// Heredamos de BaseEnemy 
public class Enemy : BaseEnemy
{
    [Header("Configuración de Patrullaje")]
    public Transform[] waypoints; // Puntos por los que va a caminar
    public float waitAtWaypointTime = 2f; // Tiempo que espera en cada punto

    private int currentWaypointIndex = 0;
    private float waitTimer = 0f;

    // Sobrescribimos el método StopEnemy de la clase padre
    protected override void StopEnemy()
    {
        if (waypoints.Length == 0)
        {
            base.StopEnemy();
            return;
        }

        agent.isStopped = false;
        agent.speed = walkSpeed;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;

            // Obtenemos el punto actual en el que estamos parados
            Transform puntoActual = waypoints[currentWaypointIndex];

            // Quaternion.Slerp hace que la rotación sea suave y no un giro robótico de golpe.
            // El '5f' es la velocidad de giro, subimos o bajamos, para que gire más rápido o más lento.
            transform.rotation = Quaternion.Slerp(transform.rotation, puntoActual.rotation, Time.deltaTime * 5f);
            // -----------------------------------------

            if (waitTimer >= waitAtWaypointTime)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                waitTimer = 0f;
            }
        }

        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}