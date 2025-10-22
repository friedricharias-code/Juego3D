using UnityEngine;

public class CollectKeys : MonoBehaviour
{
    private bool collected = false;

    void OnTriggerEnter(Collider collision)
    {
        if (collected) return;
        if (collision.gameObject.CompareTag("llave"))
        {
            collected = true;
            Destroy(collision.gameObject);
        }
    }
}
