using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidbody;

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
    private AudioSource audioSource;

    [Header("Paneles")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gamePanel;

    private PlayerMovement playerMovementScript;
    private EnemyMovement enemyMovementScriptCh30;
    private EnemyMovement enemyMovementScriptParasite;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        maxVida = vida;
        playerMovementScript = GameObject.Find("Ch22_nonPBR").GetComponent<PlayerMovement>();
        enemyMovementScriptCh30 = GameObject.Find("Ch30_nonPBR").GetComponent<EnemyMovement>();
        enemyMovementScriptParasite = GameObject.Find("Parasite L Starkie").GetComponent<EnemyMovement>();
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
        playerMovementScript.enabled = false;
        enemyMovementScriptCh30.enabled = false;
        enemyMovementScriptParasite.enabled = false;
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);
        audioSource.PlayOneShot(gameOverSound);
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
