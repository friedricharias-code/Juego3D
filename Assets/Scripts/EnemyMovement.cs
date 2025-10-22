using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform transformPlayer;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float velocidad;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var detectedPlayer = Physics.CheckSphere(transform.position, detectionRadius, playerLayer);
        if (detectedPlayer)
        {
            transform.LookAt(transformPlayer);
            transform.position = Vector3.MoveTowards(transform.position, transformPlayer.position, velocidad * Time.deltaTime);
        }
        animator.SetBool("Running", detectedPlayer);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
