using UnityEngine;
using UnityEngine.InputSystem;

public class GridMovement : MonoBehaviour
{
    public float cellSize = 1f;

    void Update()
    {
        if (Keyboard.current == null) return;

        Vector2 direction = Vector2.zero;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
            direction = Vector2.up;
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame)
            direction = Vector2.down;
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame)
            direction = Vector2.left;
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
            direction = Vector2.right;

        if (direction != Vector2.zero)
        {
            MovePlayer(direction);
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        // Alineación precisa a la cuadrícula
        Vector2 currentPos = new Vector2(
            Mathf.Round(transform.position.x / cellSize) * cellSize,
            Mathf.Round(transform.position.y / cellSize) * cellSize
        );

        Vector2 targetPos = currentPos + (direction * cellSize);

        // Desactivamos temporalmente el collider del jugador para que no bloquee el OverlapPoint
        Collider2D myCollider = GetComponent<Collider2D>();
        if (myCollider != null) myCollider.enabled = false;

        // Revisamos qué hay en la casilla a la que nos queremos mover
        Collider2D hit = Physics2D.OverlapPoint(targetPos);
        
        if (myCollider != null) myCollider.enabled = true;

        // CASO 1: Si no hay nada, o es un Trigger (como metas o portales) -> Nos movemos libremente
        if (hit == null || hit.isTrigger)
        {
            transform.position = targetPos;
        }
        // CASO 2: Si chocamos con una PARED (puedes identificarla por Tag o Layer, ej. tag "Wall")
        else if (hit.CompareTag("Wall"))
        {
            // No hacemos nada, la pared bloquea el paso
            return;
        }
        // CASO 3: Si chocamos con una CAJA
        else if (hit.GetComponent<BoxPushable>() != null)
        {
            Vector2 boxTargetPos = targetPos + (direction * cellSize);

            // Desactivamos el collider de la caja para revisar qué hay detrás de ella
            Collider2D boxCollider = hit.GetComponent<Collider2D>();
            if (boxCollider != null) boxCollider.enabled = false;

            Collider2D hitBehind = Physics2D.OverlapPoint(boxTargetPos);

            if (boxCollider != null) boxCollider.enabled = true;

            // Si detrás de la caja está libre, es un trigger o una meta, empujamos
            if (hitBehind == null || hitBehind.isTrigger)
            {
                hit.transform.position = boxTargetPos;
                transform.position = targetPos;
            }
            // Si detrás de la caja hay una pared u otra caja, no se mueve nada (bloqueado)
        }
    }
}