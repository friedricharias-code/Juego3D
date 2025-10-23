using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Curaci�n")]
    public float healAmount = 20f;       // Cu�nto cura
    public float respawnTime = 5f;       // Tiempo para reaparecer

    [Header("Animaci�n (opcional)")]
    [SerializeField] private Animator playerAnimator; // arrastra aqu� el Animator del jugador
    [SerializeField] private string healTriggerName = "Heal"; // nombre del trigger en Animator

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Llamar al m�todo Curar del GameOver del jugador
            GameOver gameOverScript = other.GetComponent<GameOver>();
            if (gameOverScript != null)
            {
                gameOverScript.Curar(healAmount); // Aumenta la vida
            }

            // Reproducir animaci�n de curaci�n
            if (playerAnimator != null && !string.IsNullOrEmpty(healTriggerName))
            {
                playerAnimator.SetTrigger(healTriggerName);
            }

            // Desactivar el remedio temporalmente
            gameObject.SetActive(false);

        }
    }

}
