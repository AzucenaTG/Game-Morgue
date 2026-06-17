using UnityEngine;

public class EnemyStaminaDrain : MonoBehaviour
{
    public float staminaDrain = 0.15f; 

    private void OnTriggerStay(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            player.currentStamina -=
                staminaDrain * Time.deltaTime;

            player.currentStamina = Mathf.Clamp(
                player.currentStamina,
                0,
                player.maxStamina
            );
        }
    }
}