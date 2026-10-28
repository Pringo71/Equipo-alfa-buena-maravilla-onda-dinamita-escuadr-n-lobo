using UnityEngine;

public class BoxPushable : MonoBehaviour
{
    public float cellSize = 0.5f;

    void Start()
    {
        Vector2 p = transform.position;
        transform.position = new Vector2(
            Mathf.Round(p.x / cellSize) * cellSize,
            Mathf.Round(p.y / cellSize) * cellSize);
    }

    // Devuelve true si la caja se movió
    public bool Empujar(Vector2 dir)
    {
        Vector2 destino = (Vector2)transform.position + dir * cellSize;
        if (!LimitesJuego.Dentro(destino, cellSize)) return false;
        Collider2D[] hits = Physics2D.OverlapBoxAll(destino, Vector2.one * cellSize * 0.8f, 0f);
        foreach (Collider2D h in hits)
        {
            if (h.gameObject == gameObject || h.isTrigger) continue;
            return false; // hay algo sólido detrás: no se mueve
        }

        transform.position = destino;
        return true;
    }
}