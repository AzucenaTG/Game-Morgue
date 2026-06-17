using UnityEngine;
using System.Collections;

public class BossIntro : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerController playerMovement;
    public Transform boss;
    public Transform playerCamera;
    public BossPuzzle puzzle;

    [Header("Configuración")]
    public float bossSpeed = 3f;

    private bool bossAlreadyMoved = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Detener sonido de pasos
        if (playerMovement.footstepsAudio != null)
        {
            playerMovement.footstepsAudio.Stop();
        }

        // Animación Idle
        if (playerMovement.anim != null)
        {
            playerMovement.anim.Play("Breathing Idle");
        }

        // Bloquear movimiento
        playerMovement.enabled = false;

        // Primera vez: el jefe baja
        if (!bossAlreadyMoved)
        {
            StartCoroutine(BossEntrance());
        }
        else
        {
            // Si ya bajó antes, mostrar directamente el desafío
            puzzle.StartPuzzle();
        }
    }

    IEnumerator BossEntrance()
    {
        Vector3 targetPos = boss.position;
        targetPos.y = playerCamera.position.y;

        while (Mathf.Abs(boss.position.y - targetPos.y) > 0.01f)
        {
            boss.position = Vector3.MoveTowards(
                boss.position,
                targetPos,
                bossSpeed * Time.deltaTime
            );

            yield return null;
        }

        boss.position = targetPos;

        bossAlreadyMoved = true;

        puzzle.StartPuzzle();
    }
}