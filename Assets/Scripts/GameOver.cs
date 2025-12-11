using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;     
using System.Collections;

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
    [SerializeField] AudioClip dolor;
    private AudioSource audioSource;

    [Header("Paneles")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject cargandoPanel;

    private PlayerMovement playerMovementScript;
    private EnemyMovement enemyMovementScriptCh30;
    private EnemyMovement enemyMovementScriptCh30_1;
    private EnemyMovement enemyMovementScriptParasite;
    private bool dolorSonando = false;
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

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            RecibirDaño(); // se llama cada frame mientras haya contacto
        }
    }

    private IEnumerator EsperarFinSonido(float duracion, System.Action accion)
    {
        yield return new WaitForSeconds(duracion);
        accion.Invoke();
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
        if (!dolorSonando && dolor != null)
        {
            audioSource.PlayOneShot(dolor);
            StartCoroutine(EsperarFinSonido(dolor.length, () => dolorSonando = false));
            dolorSonando = true;
        }
        audioSource.PlayOneShot(AttackEnemySound);

        if (vida <= 0)
        {
            Perder();
            return; // no dispares Hurt ni knockback
        }
    }

    void Perder()
    {
        animator.SetTrigger("Death");
    }

    public void Muerte()
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

    public void SalirAlMenu()
    {
        cargandoPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}
