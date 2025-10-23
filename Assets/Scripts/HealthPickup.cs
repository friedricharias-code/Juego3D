using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Curaci�n")]
    public float healAmount = 20f;
    public float respawnTime = 5f;

    [Header("Animaci�n (opcional)")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private string healTriggerName = "Heal";
    [SerializeField] private float healAnimationDuration = 8.3f; // duraci�n estimada de la animaci�n

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameOver gameOverScript = other.GetComponent<GameOver>();
            if (gameOverScript != null)
            {
                gameOverScript.Curar(healAmount);
            }

            if (playerAnimator != null && !string.IsNullOrEmpty(healTriggerName))
            {
                playerAnimator.SetTrigger(healTriggerName);

                // Desactivar movimiento
                PlayerMovement movementScript = other.GetComponent<PlayerMovement>();
                if (movementScript != null)
                {
                    movementScript.enabled = false;
                    StartCoroutine(ReenableMovementAfterAnimation(movementScript));
                }
            }

            gameObject.SetActive(false);
        }
    }

    private System.Collections.IEnumerator ReenableMovementAfterAnimation(PlayerMovement movementScript)
    {
        yield return new WaitForSeconds(healAnimationDuration);
        movementScript.enabled = true;
    }
}