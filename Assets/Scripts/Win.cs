using UnityEngine;

public class Win : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gamePanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MostrarPanelVictoria();
        }
    }

    private void MostrarPanelVictoria()
    {
        winPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
}
