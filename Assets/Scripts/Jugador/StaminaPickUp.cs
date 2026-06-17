using UnityEngine;

public class StaminaPickUp : MonoBehaviour
{
    public float staminaAmount = 2f; 

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.RecoverStamina(staminaAmount);

            Destroy(gameObject);
        }
    }
}