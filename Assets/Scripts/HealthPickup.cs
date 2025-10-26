using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Curacion")]
    public float healAmount = 20f;

    [Header("Animacion")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private string healTriggerName = "Heal";

    [Header("Audio")]
    [SerializeField] private AudioClip healSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            // Activar animacion de curacion
            if (playerAnimator != null && !string.IsNullOrEmpty(healTriggerName))
            {
                playerAnimator.SetTrigger(healTriggerName);
                audioSource.PlayOneShot(healSound);
            }
            
            // Curar al jugador
            GameOver gameOverScript = other.GetComponent<GameOver>();
            if (gameOverScript != null)
            {
                gameOverScript.Curar(healAmount);
            }

            // Desactivar el objeto del medicamento
            gameObject.SetActive(false);
        }
    }
}
