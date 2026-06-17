using UnityEngine;
using UnityEngine.UI;

public class EyeScaryUi : MonoBehaviour
{
    public Image eyeImage;
    public float miedo; 

    public Sprite normal;
    public Sprite medio;
    public Sprite loco;

    void Update()
    {
        if (miedo < 20)
            eyeImage.sprite = normal;
        else if (miedo < 50)
            eyeImage.sprite = medio;
        else
            eyeImage.sprite = loco;
    }
}