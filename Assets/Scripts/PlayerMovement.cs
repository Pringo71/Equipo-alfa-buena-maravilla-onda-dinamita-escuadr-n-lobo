using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float cellSize = 0.5f;
    public bool juegoIniciado = false;
    
    Vector2 posicionInicial;

    void Start()
    {
        // Ajusta al jugador a la cuadrícula
        Vector2 p = transform.position;
        transform.position = new Vector2(
            Mathf.Round(p.x / cellSize) * cellSize,
            Mathf.Round(p.y / cellSize) * cellSize);

        posicionInicial = transform.position;
    }

    void Update()
    {
         if (!juegoIniciado) return;
        Keyboard k = Keyboard.current;
        if (k == null) return;

        Vector2 dir = Vector2.zero;

        if (k.wKey.wasPressedThisFrame || k.upArrowKey.wasPressedThisFrame) dir = Vector2.up;
        else if (k.sKey.wasPressedThisFrame || k.downArrowKey.wasPressedThisFrame) dir = Vector2.down;
        else if (k.aKey.wasPressedThisFrame || k.leftArrowKey.wasPressedThisFrame) dir = Vector2.left;
        else if (k.dKey.wasPressedThisFrame || k.rightArrowKey.wasPressedThisFrame) dir = Vector2.right;

        if (dir != Vector2.zero) Mover(dir);
    }

    void Mover(Vector2 dir)
    {
        Vector2 destino = (Vector2)transform.position + dir * cellSize;
        if (!LimitesJuego.Dentro(destino, cellSize)) return;
        Collider2D hit = ObtenerSolido(destino);

        if (hit != null)
        {
            BoxPushable caja = hit.GetComponent<BoxPushable>();

            // Pared, u otra cosa sólida, o caja que no se puede empujar: no avanzamos
            if (caja == null || !caja.Empujar(dir)) return;
        }

        transform.position = destino;
    }

    // Busca algo sólido en la casilla, ignorando al jugador y a los triggers
    Collider2D ObtenerSolido(Vector2 centro)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(centro, Vector2.one * cellSize * 0.8f, 0f);
        foreach (Collider2D h in hits)
        {
            if (h.gameObject == gameObject || h.isTrigger) continue;
            return h;
        }
        return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Portal"))
            transform.position = posicionInicial;
    }
}