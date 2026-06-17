using UnityEngine;
using System.Collections;

public class ZoneController : MonoBehaviour
{
    [SerializeField] private float timerTransition = 1f;
    public void TransitionInBack()
    {
        StartCoroutine(Interpolate());
    }
    IEnumerator Interpolate()
    {
        float t;
        float elapsedTime = 0f;
        while (elapsedTime < timerTransition)
        {
            elapsedTime += Time.deltaTime;
            t = elapsedTime / timerTransition;
            AudioManager.instance.TransitionBack(t);
            yield return null;
        }
    }
}