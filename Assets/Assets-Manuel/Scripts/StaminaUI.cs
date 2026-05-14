using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public Image staminaImage;
    public WASD playerMovement;

    public Sprite stamina100;
    public Sprite stamina75;
    public Sprite stamina50;
    public Sprite stamina25;
    public Sprite stamina10;
    public Sprite stamina0;

    void Update()
    {
        float staminaPercent =
            (playerMovement.currentStamina /
            playerMovement.maxStamina) * 100f;

        if (staminaPercent > 75)
            staminaImage.sprite = stamina100;

        else if (staminaPercent > 50)
            staminaImage.sprite = stamina75;

        else if (staminaPercent > 25)
            staminaImage.sprite = stamina50;

        else if (staminaPercent > 10)
            staminaImage.sprite = stamina25;

        else if (staminaPercent > 0)
            staminaImage.sprite = stamina10;

        else
            staminaImage.sprite = stamina0;
    }
}