using UnityEngine;

public class MoverTexto : MonoBehaviour
{
    public float velocidadMovimiento = 1f; // Ajusta la velocidad de movimiento
    public float distanciaMovimiento = 1f; // Ajusta la distancia m�xima
    private float posicionInicialY;

    void Start()
    {
        // Guarda la posici�n inicial Y
        posicionInicialY = transform.localPosition.y;
    }

    void Update()
    {
        // Mueve el texto hacia arriba
        transform.localPosition += Vector3.up * velocidadMovimiento * Time.deltaTime;

        // Limita la distancia de movimiento
        if (transform.localPosition.y > posicionInicialY + distanciaMovimiento)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, posicionInicialY + distanciaMovimiento, transform.localPosition.z);
        }
    }
}