using UnityEngine;

public class StaminaPickUp : MonoBehaviour
{
    public float staminaAmount = 2f; 

    private void OnTriggerEnter(Collider other)
    {
        WASD player = other.GetComponent<WASD>();

        if (player != null)
        {
            player.RecoverStamina(staminaAmount);

            Destroy(gameObject);
        }
    }
}