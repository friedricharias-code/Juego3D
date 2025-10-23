using UnityEngine;

public class CollectKeys : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMPro.TextMeshProUGUI keyText;
    private int keysCollected = 0;

    public int KeysCollected => keysCollected; // propiedad p�blica

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("llave"))
        {
            keysCollected++;
            keyText.text = "Llaves: " + keysCollected;
            Destroy(collision.gameObject);
        }
    }
}
