using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject opciones;
    [SerializeField] private GameObject creditos;
    [SerializeField] private GameObject cargandoPantalla;
    void Start()
    {
        menu.SetActive(true);
        opciones.SetActive(false);
        creditos.SetActive(false);
        cargandoPantalla.SetActive(false);
    }

    public void abrirOpciones()
    {
        menu.SetActive(false);
        creditos.SetActive(false);
        opciones.SetActive(true);
    }

    public void abrirMenu()
    {
        opciones.SetActive(false);
        creditos.SetActive(false);
        menu.SetActive(true);
    }

    public void abrirCreditos()
    {
        menu.SetActive(false);
        opciones.SetActive(false);
        creditos.SetActive(true);
    }

    public void salirJuego()
    {
        Application.Quit();
    }

    public void jugar()
    {
        menu.SetActive(false);
        cargandoPantalla.SetActive(true);
        SceneManager.LoadScene("SampleScene");
    }
}
