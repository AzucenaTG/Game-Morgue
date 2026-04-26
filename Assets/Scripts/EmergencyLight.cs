using UnityEngine;

public class EmergencyLight : MonoBehaviour
{
    [SerializeField] float oscillationPeriod = 7.0f;
    [SerializeField] Light emergencyLight;
    [SerializeField] bool isFlickering = false;
    [SerializeField] AudioClip FlickSFX;

    AudioSource audioSource;

    private float maxIntensity;
    private float randomFloat;
    void Start()
    {
        emergencyLight = GetComponent<Light>();
        maxIntensity = emergencyLight.intensity;
        audioSource = GetComponent<AudioSource>();
        if (isFlickering)
        {
            audioSource.clip = FlickSFX;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    void Update()
    {
        if (isFlickering)
        {
            randomFloat = Random.Range(0.1f, 1.0f);
        }
        else
        {
            randomFloat = 1;
        }
        emergencyLight.intensity = Mathf.PingPong(oscillationPeriod*Time.time*randomFloat, maxIntensity);
    }
}
