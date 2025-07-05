using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource footstepsSource;
    [SerializeField] private float minWalkSpeed = 0.1f; // Umbral para considerar que está caminando

    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Calcula la velocidad real de desplazamiento (no rotación)
        float speed = ((transform.position - lastPosition) / Time.fixedDeltaTime).magnitude;
        bool isWalking = speed > minWalkSpeed;

        if (isWalking)
        {
            if (!footstepsSource.isPlaying)
                footstepsSource.Play();
        }
        else
        {
            if (footstepsSource.isPlaying)
                footstepsSource.Stop();
        }

        lastPosition = transform.position;
    }
}


