using TMPro;
using UnityEngine;

public class PuertaLlaves : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private int llavesNecesarias = 3;
    [SerializeField] private Transform puerta;
    [SerializeField] private Transform posicionFinal;
    [SerializeField] private Vector3 rotacionFinalEuler; // rotacion en grados
    [SerializeField] private float velocidad = 2f;

    private bool abrir = false;
    private Quaternion rotacionFinal;
    private EnemyMovement enemyMovementScriptCh30;
    private EnemyMovement enemyMovementScriptCh30_1;
    private EnemyMovement enemyMovementScriptParasite;

    [Header("Aviso Faltan Llaves")]
    [SerializeField] private GameObject avisoPanel;
    [SerializeField] private float avisoDuracion = 2f;
    [SerializeField] private TextMeshProUGUI avisoTexto;

    [Header("Audio")]
    [SerializeField] private AudioClip insertaLlaveSonido;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rotacionFinal = Quaternion.Euler(rotacionFinalEuler);
        enemyMovementScriptCh30 = GameObject.Find("Ch30_nonPBR").GetComponent<EnemyMovement>();
        enemyMovementScriptCh30_1 = GameObject.Find("Ch30_nonPBR1").GetComponent<EnemyMovement>();
        enemyMovementScriptParasite = GameObject.Find("Parasite L Starkie").GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectKeys recolector = other.GetComponent<CollectKeys>();
            if (recolector != null && recolector.KeysCollected >= llavesNecesarias)
            {
                abrir = true;
                audioSource.PlayOneShot(insertaLlaveSonido);
                enemyMovementScriptCh30.enabled = false;
                enemyMovementScriptParasite.enabled = false;
                enemyMovementScriptCh30_1.enabled = false;
            }
            else
            {
                // Mostrar aviso de que faltan llaves
                if (avisoPanel != null)
                {
                    avisoTexto.text = "Faltan " + (llavesNecesarias - recolector.KeysCollected) + " llaves para abrir la puerta.";
                    avisoPanel.SetActive(true);
                    Invoke("OcultarAviso", avisoDuracion);
                }
            }
        }
    }

    private void OcultarAviso()
    {
        if (avisoPanel != null)
        {
            avisoPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (abrir && puerta != null)
        {
            puerta.position = Vector3.MoveTowards(puerta.position, posicionFinal.position, velocidad * Time.deltaTime);
            puerta.rotation = Quaternion.RotateTowards(puerta.rotation, rotacionFinal, velocidad * 50f * Time.deltaTime);
        }
    }
}
