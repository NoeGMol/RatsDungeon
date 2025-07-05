using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenaAlFinalAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public string nombreSiguienteEscena;

    void Update()
    {
        // Verifica si el AudioSource está reproduciendo o si hago click
        if (!audioSource.isPlaying || Input.GetMouseButton(0))
        
        {
            // Carga la siguiente escena
            SceneManager.LoadScene(nombreSiguienteEscena);
        }
    }
}