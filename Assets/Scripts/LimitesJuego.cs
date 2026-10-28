using UnityEngine;

public class LimitesJuego : MonoBehaviour
{
    // Si el borde queda muy justo o muy suelto, ajusta esto (positivo = área más pequeña)
    public float recorteVertical = 0f;

    static Bounds area;
    static bool definido;

    // Margen para absorber pequeñas diferencias entre el fondo y la cuadrícula
    const float tolerancia = 0.05f;

    void Awake()
    {
        Bounds fondo = GetComponent<SpriteRenderer>().bounds;
        Camera cam = Camera.main;

        // Horizontal: el ancho del fondo. Vertical: lo que enfoca la cámara
        float mitadAlto = cam.orthographicSize - recorteVertical;
        float yMin = cam.transform.position.y - mitadAlto;
        float yMax = cam.transform.position.y + mitadAlto;

        area = new Bounds();
        area.SetMinMax(
            new Vector3(fondo.min.x, yMin, 0f),
            new Vector3(fondo.max.x, yMax, 0f));

        definido = true;
    }

    // true si la casilla centrada en 'centro' cabe completa dentro del área
    public static bool Dentro(Vector2 centro, float cellSize)
    {
        if (!definido) return true;

        float m = cellSize * 0.5f - tolerancia;
        return centro.x - m >= area.min.x && centro.x + m <= area.max.x
            && centro.y - m >= area.min.y && centro.y + m <= area.max.y;
    }
}