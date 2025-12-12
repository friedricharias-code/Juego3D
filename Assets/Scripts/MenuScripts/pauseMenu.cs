using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    [Header("Pausa Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject opcionesPanel; // si quieres opciones dentro del pause
    [SerializeField] private GameObject fondoOscuro;   // opcional
    [SerializeField] private GameObject cargandoPanel; // opcional

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        if (opcionesPanel != null) opcionesPanel.SetActive(false);
        if (fondoOscuro != null) fondoOscuro.SetActive(false);
        if (cargandoPanel != null) cargandoPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;

        pausePanel.SetActive(true);
        if (fondoOscuro != null) fondoOscuro.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;

        pausePanel.SetActive(false);
        if (opcionesPanel != null) opcionesPanel.SetActive(false);
        if (fondoOscuro != null) fondoOscuro.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        if (fondoOscuro != null) fondoOscuro.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
        if (cargandoPanel != null) cargandoPanel.SetActive(true);
        SceneManager.LoadScene("Menu"); // pon el nombre real de tu menú
    }

    public void abrirOpcionesDePausa()
    {
        pausePanel.SetActive(false);
        opcionesPanel.SetActive(true);
    }

    public void cerrarOpciones()
    {
        opcionesPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
