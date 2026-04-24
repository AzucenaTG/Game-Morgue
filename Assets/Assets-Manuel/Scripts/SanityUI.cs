using UnityEngine;
using UnityEngine.UI;

public class SanityUI : MonoBehaviour
{

    public Slider slider;
    public SanitySystem sanity;
    public Image fillImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float sanityPercent = sanity.currentSanity / sanity.maxSanity;
        slider.value = sanityPercent;

        if (sanityPercent > 0.5f) { 
        
            fillImage.color = Color.cyan;
        }
        else if(sanityPercent > 0.2f)
        {
            fillImage.color = new Color(0.5f, 0f, 1f);
        }
        else
        {
            fillImage.color = Color.red;
        }
    }
}
