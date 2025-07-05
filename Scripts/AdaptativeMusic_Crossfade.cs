
// AdaptiveMusic_Crossfade.cs
using UnityEngine;
using System.Collections; // Necesario para las Corrutinas




public class AdaptiveMusic_Crossfade : MonoBehaviour
{
    [Header("Audio Sources (Asignar en Inspector)")]
    public AudioSource trackA_Source; // Pista principal, ej: Exploración
    public AudioSource trackB_Source; // Pista secundaria, ej: Combate


    [Header("Parámetros de Crossfade")]
    [Range(0.5f, 10.0f)]
    public float crossfadeDuration = 3.0f; // Duración del fundido en segundos


    private AudioSource currentActiveTrack;   // Referencia a la pista que está sonando actualmente
    private AudioSource nextTrackToFadeIn;    // Referencia a la pista que va a entrar
    private Coroutine activeCrossfadeCoroutine; // Para gestionar la corrutina de crossfade


    void Start()
    {
        // Asumimos que la Pista A (trackA_Source) es la que comienza sonando
        if (trackA_Source != null)
        {
            trackA_Source.volume = 1f;
            trackA_Source.Play(); // Inicia la pista A
            currentActiveTrack = trackA_Source;
        }
        else { Debug.LogError("Track A Source no asignado!"); return; }




        if (trackB_Source != null)
        {
            trackB_Source.volume = 0f; // Pista B inicia en silencio
            trackB_Source.Play();      // Inicia la pista B en silencio (lista para el fade-in)
            nextTrackToFadeIn = trackB_Source;
        }
        else { Debug.LogError("Track B Source no asignado!"); return; }
    }


    // Método público para iniciar la transición a la Pista B
    public void SwitchToTrackB()
    {
        // Solo realizar el cambio si la Pista A es la que está activa
        if (currentActiveTrack == trackA_Source && trackB_Source != null)
        {
            if (activeCrossfadeCoroutine != null) StopCoroutine(activeCrossfadeCoroutine);
            activeCrossfadeCoroutine = StartCoroutine(PerformCrossfade(trackB_Source, trackA_Source, crossfadeDuration));
            currentActiveTrack = trackB_Source;
            nextTrackToFadeIn = trackA_Source; // La próxima en entrar sería la A
        }
    }




    // Método público para iniciar la transición de vuelta a la Pista A
    public void SwitchToTrackA()
    {
        // Solo realizar el cambio si la Pista B es la que está activa
        if (currentActiveTrack == trackB_Source && trackA_Source != null)
        {
            if (activeCrossfadeCoroutine != null) StopCoroutine(activeCrossfadeCoroutine);
            activeCrossfadeCoroutine = StartCoroutine(PerformCrossfade(trackA_Source, trackB_Source, crossfadeDuration));
            currentActiveTrack = trackA_Source;
            nextTrackToFadeIn = trackB_Source; // La próxima en entrar sería la B
        }
    }




    // Corrutina que ejecuta el fundido cruzado
    private IEnumerator PerformCrossfade(AudioSource trackToFadeIn, AudioSource trackToFadeOut, float duration)
    {
        float currentTime = 0f;
        // No es estrictamente necesario guardar los volúmenes iniciales si siempre partimos de 0 para fadeIn y 1 para fadeOut
        // float startVolumeFadeIn = trackToFadeIn.volume; 
        // float startVolumeFadeOut = trackToFadeOut.volume;




        // Asegurarse de que la pista que va a entrar esté reproduciéndose (si no lo estaba ya)
        // if (!trackToFadeIn.isPlaying) trackToFadeIn.Play(); 




        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            // Interpolar el volumen de la pista que entra de 0 a 1
            trackToFadeIn.volume = Mathf.Lerp(0f, 1f, currentTime / duration);
            // Interpolar el volumen de la pista que sale de 1 a 0
            trackToFadeOut.volume = Mathf.Lerp(1f, 0f, currentTime / duration);
            yield return null; // Esperar al siguiente frame
        }




        trackToFadeIn.volume = 1f;   // Asegurar volumen final para la pista que entra
        trackToFadeOut.volume = 0f;  // Asegurar volumen final para la pista que sale

        // Opcional: Detener la pista que se desvaneció para ahorrar recursos,
        // aunque si está en loop y con volumen 0, el impacto es mínimo.
        // trackToFadeOut.Stop(); 
        activeCrossfadeCoroutine = null;
    }
}