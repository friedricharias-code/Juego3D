using UnityEngine;
using System.Collections;
using static System.Collections.IEnumerator;

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
            // Activar animación
            if (playerAnimator != null && !string.IsNullOrEmpty(healTriggerName))
                playerAnimator.SetTrigger(healTriggerName);

            // Reproducir sonido
            AudioSource.PlayClipAtPoint(healSound, transform.position);

            // Curar al jugador
            GameOver gameOverScript = other.GetComponent<GameOver>();
            if (gameOverScript != null)
                gameOverScript.Curar(healAmount);

            // Hacer invisible el objeto
            foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                rend.enabled = false;

            // Destruir al terminar el sonido
            StartCoroutine(DestruirDespuesDeSonido(healSound.length));
        }
    }

    private IEnumerator DestruirDespuesDeSonido(float duracion)
    {
        yield return new WaitForSeconds(duracion);
        Destroy(gameObject);
    }

}
