using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource[] audioBack;
    [SerializeField] private float maxBack0 = 1f;
    [SerializeField] private float maxBack1 = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioBack[0].volume = 1f*maxBack0;
        audioBack[1].volume = 0f;
    }

    public void TransitionBack(float timer)
    {
        float t = Mathf.Clamp01(timer);
        audioBack[0].volume = (1-t)* maxBack0;
        audioBack[1].volume = t* maxBack1;
    }
}
