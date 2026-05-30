using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
     public float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
