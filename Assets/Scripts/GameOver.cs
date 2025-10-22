using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidbody;
    [SerializeField] private int vida = 100;

    // === Cooldown de daño ===
    [SerializeField] private float damageCooldown = 0.3f; // en segundos
    private float nextDamageTime = 0f;
    
    [Header("Audio")]
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip hitSound;
    private AudioSource audioSource;

    [Header("Canvas")]
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameCanvas;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            RecibirDaño();
    }

    void RecibirDaño()
    {
        // Si aún estamos en cooldown, ignorar este golpe
        if (Time.time < nextDamageTime || vida <= 0)
            return;

        // Programar el siguiente instante en el que se puede recibir daño
        nextDamageTime = Time.time + damageCooldown;

        // ↓ primero baja vida
        vida = Mathf.Max(vida - 1, 0);

        if (vida <= 0)
        {
            Perder();
            return; // no dispares Hurt ni knockback
        }

        audioSource.PlayOneShot(hitSound);
        animator.SetTrigger("Hurt");
    }

    void Perder()
    {
        animator.ResetTrigger("Hurt"); // evita que Hurt bloquee GameOver
        rigidbody.linearVelocity = Vector2.zero; // detiene el movimiento
        gameOverCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        audioSource.PlayOneShot(gameOverSound);
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
