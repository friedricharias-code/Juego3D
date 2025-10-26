using UnityEngine;

public class CollectKeys : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMPro.TextMeshProUGUI keyText;
    private int keysCollected = 0;

    [Header("Audio")]
    [SerializeField] private AudioClip keyCollectSound;
    private AudioSource audioSource;

    public int KeysCollected => keysCollected; // propiedad pública

    private void Start()
    {
        keyText.text = "Llaves: " + keysCollected;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("llave"))
        {
            keysCollected++;
            keyText.text = "Llaves: " + keysCollected;
            audioSource.PlayOneShot(keyCollectSound);
            Destroy(collision.gameObject, keyCollectSound.length);
        }
    }
}
