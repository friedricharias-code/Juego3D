using UnityEngine;
using UnityEngine.Audio;

public class Win : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gamePanel;
    [Header("Audio")]
    [SerializeField] AudioClip winSound;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(winSound);
            MostrarPanelVictoria();
        }
    }

    private void MostrarPanelVictoria()
    {
        winPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
}
