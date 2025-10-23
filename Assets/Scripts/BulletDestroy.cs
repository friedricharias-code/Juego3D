using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public float lifeTime = 5f; // opcional: destruir después de X segundos aunque no choque

    void Start()
    {
        Destroy(gameObject, lifeTime); // destruye la bala después de 5 segundos
    }

    void OnCollisionEnter(Collision collision)
    {
        // Evita que choque consigo misma o con el jugador si quieres
        // if(collision.gameObject.CompareTag("Player")) return;

        // Destruye la bala al chocar con cualquier otro objeto
        Destroy(gameObject);
    }
}
