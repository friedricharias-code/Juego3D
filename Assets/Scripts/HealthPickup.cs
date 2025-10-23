using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Curación")]
    public float healAmount = 20f;       // Cuánto cura
    public float respawnTime = 5f;       // Tiempo para reaparecer

    [Header("Animación (opcional)")]
    [SerializeField] private Animator playerAnimator; // arrastra aquí el Animator del jugador
    [SerializeField] private string healTriggerName = "Heal"; // nombre del trigger en Animator

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Llamar al método Curar del GameOver del jugador
            GameOver gameOverScript = other.GetComponent<GameOver>();
            if (gameOverScript != null)
            {
                gameOverScript.Curar(healAmount); // Aumenta la vida
            }

            // Reproducir animación de curación
            if (playerAnimator != null && !string.IsNullOrEmpty(healTriggerName))
            {
                playerAnimator.SetTrigger(healTriggerName);
            }

            // Desactivar el remedio temporalmente
            gameObject.SetActive(false);

        }
    }

}
