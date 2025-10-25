using UnityEngine;

public class PuertaLlaves : MonoBehaviour
{
    [Header("Configuraci�n")]
    [SerializeField] private int llavesNecesarias = 3;
    [SerializeField] private Transform puerta;
    [SerializeField] private Transform posicionFinal;
    [SerializeField] private Vector3 rotacionFinalEuler; // rotaci�n en grados
    [SerializeField] private float velocidad = 2f;

    private bool abrir = false;
    private Quaternion rotacionFinal;
    private EnemyMovement enemyMovementScriptCh30;
    private EnemyMovement enemyMovementScriptCh30_1;
    private EnemyMovement enemyMovementScriptParasite;

    private void Start()
    {
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
                enemyMovementScriptCh30.enabled = false;
                enemyMovementScriptParasite.enabled = false;
                enemyMovementScriptCh30_1.enabled = false;
            }
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
