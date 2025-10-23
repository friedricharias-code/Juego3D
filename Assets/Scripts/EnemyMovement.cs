using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class EnemyMovement : MonoBehaviour
{
    [Header("Detección")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform transformPlayer;
    [SerializeField] private float detectionRadius = 5f;

    [Header("Movimiento")]
    [SerializeField] private float velocidad = 3f;
    [SerializeField] private Transform[] patrolPoints; // puntos de patrulla
    private int currentPoint = 0;

    private Animator animator;
    private bool persiguiendo = false;

    [Header("Audio")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip SiguiendoSound;
    [SerializeField] private AudioClip patrullandoSound;
    [Header("Vida")]
    public float vida = 10f;
    public GameObject enemyObject;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Detectar jugador
        persiguiendo = Physics.CheckSphere(transform.position, detectionRadius, playerLayer);

        if (persiguiendo)
        {
            // Perseguir al jugador
            transform.LookAt(transformPlayer);
            transform.position = Vector3.MoveTowards(transform.position, transformPlayer.position, velocidad * Time.deltaTime);
            audioSource.PlayOneShot(SiguiendoSound);
        }
        else
        {
            // Patrullar entre puntos
            if (patrolPoints.Length > 0)
            {
                Transform destino = patrolPoints[currentPoint];
                transform.LookAt(destino);
                transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidad * Time.deltaTime);
                audioSource.PlayOneShot(patrullandoSound);

                if (Vector3.Distance(transform.position, destino.position) < 0.2f)
                {
                    currentPoint = (currentPoint + 1) % patrolPoints.Length;
                }
            }
        }

        animator.SetBool("Running", true); // siempre en movimiento
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.green;
        if (patrolPoints != null)
        {
            foreach (var point in patrolPoints)
            {
                Gizmos.DrawSphere(point.position, 0.2f);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            vida -= 1f;
            if (vida <= 0f)
            {
                enemyObject.SetActive(false);
            }
        }
    }

}
