using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Curación")]
    public float healAmount = 20f;

    [Header("Animación")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private string healTriggerName = "Heal";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Curar al jugador
            GameOver gameOverScript = other.GetComponent<GameOver>();
            if (gameOverScript != null)
            {
                gameOverScript.Curar(healAmount);
            }

            // Activar animación de curación
            if (playerAnimator != null && !string.IsNullOrEmpty(healTriggerName))
            {
                playerAnimator.SetTrigger(healTriggerName);
            }

            // Desactivar el objeto del medicamento
            gameObject.SetActive(false);
        }
    }
}
