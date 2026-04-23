using UnityEngine;
using UnityEngine.UI;

public class SanitySystem : MonoBehaviour
{

    public float maxSanity = 1000f;
    public float currentSanity;

    public float drainSpeed = 0.1f;

    public Light flashLight;
    public float darkDrain = 4f;
    public float normalDrain = 2f;

    public Image overlay;

    public Transform cameraTransform;
    public float shakeIntensity = 1f;

    private bool sanityActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSanity = maxSanity;
    }

    // Update is called once per frame
    void Update()
    {
        //testing de sanity
        //if (Input.GetKeyDown(KeyCode.X)) {

        //    sanityActive = !sanityActive;
        //}

        if (!sanityActive)
        {
            return;
        }

        float sanityPercent = currentSanity / maxSanity;

            //Color color = overlay.color;
            //color.a = 1f - sanityPercent;
            //overlay.color = color;

        float currentDrain = drainSpeed;
        float drain;

        if (flashLight != null && flashLight.enabled) {

            drain = normalDrain;
        }
        else
        {
            drain = darkDrain;
        }

        currentSanity -= drain * Time.deltaTime;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);

        //if(currentSanity < 500f)
        //{
        //    float shake = (10000f - (currentSanity / 500f)) * shakeIntensity;

        //    cameraTransform.localPosition += new Vector3(
        //        Random.Range(-shake, shake),
        //        Random.Range(-shake, shake),
        //        0
        //        );
        //}

    }
}
