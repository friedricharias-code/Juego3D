using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform transformPlayer;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float velocidad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var detectedPlayer = Physics.CheckSphere(transform.position, detectionRadius, playerLayer);
        if (detectedPlayer)
        {
            transform.LookAt(transformPlayer);
            transform.position = Vector3.MoveTowards(transform.position, transformPlayer.position, velocidad * Time.deltaTime);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
