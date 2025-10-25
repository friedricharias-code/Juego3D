using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Animator animator;

    // === Cooldown de daño ===
    [SerializeField] private float damageCooldown = 0.3f; // en segundos
    private float nextDamageTime = 0f;

    [Header("Vida")]
    [SerializeField] private float vida = 100f;
    [SerializeField] Image barraVida;
    private float maxVida;

    [Header("Audio")]
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip AttackEnemySound;
    private AudioSource audioSource;

    [Header("Paneles")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gamePanel;

    private PlayerMovement playerMovementScript;
    private EnemyMovement enemyMovementScriptCh30;
    private EnemyMovement enemyMovementScriptCh30_1;
    private EnemyMovement enemyMovementScriptParasite;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        maxVida = vida;
        playerMovementScript = GameObject.Find("Ch22_nonPBR").GetComponent<PlayerMovement>();
        enemyMovementScriptCh30 = GameObject.Find("Ch30_nonPBR").GetComponent<EnemyMovement>();
        enemyMovementScriptCh30_1 = GameObject.Find("Ch30_nonPBR1").GetComponent<EnemyMovement>();
        enemyMovementScriptParasite = GameObject.Find("Parasite L Starkie").GetComponent<EnemyMovement>();
    }
    public void Curar(float cantidad)
    {
        vida += cantidad;
        if (vida > maxVida)
            vida = maxVida;

        // Opcional: reproducir animación de curación
        if (animator)
            animator.SetTrigger("Heal"); // asegúrate de tener una animación llamada "Heal"

        // Opcional: sonido de curación
        // audioSource.PlayOneShot(healSound); // si agregas un clip
    }

    void Update()
    {
        barraVida.fillAmount = vida / maxVida;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
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
        vida = Mathf.Max(vida - 10, 0);
        audioSource.PlayOneShot(AttackEnemySound);

        if (vida <= 0)
        {
            Perder();
            return; // no dispares Hurt ni knockback
        }

        audioSource.PlayOneShot(hitSound);
    }

    void Perder()
    {
        playerMovementScript.enabled = false;
        enemyMovementScriptCh30.enabled = false;
        enemyMovementScriptParasite.enabled = false;
        enemyMovementScriptCh30_1.enabled = false;
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);
        audioSource.PlayOneShot(gameOverSound);
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
