using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public BossChallenge bossChallenge;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossChallenge.StartChallenge();
        }
    }
}