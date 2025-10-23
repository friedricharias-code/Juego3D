using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Curaci�n")]
    public float healAmount = 20f;

    [Header("Animaci�n")]
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

            // Activar animaci�n de curaci�n
            if (playerAnimator != null && !string.IsNullOrEmpty(healTriggerName))
            {
                playerAnimator.SetTrigger(healTriggerName);
            }

            // Desactivar el objeto del medicamento
            gameObject.SetActive(false);
        }
    }
}
