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
        Vector2 targetPos = (Vector2)transform.position + (direction * cellSize);

        // Apagamos el collider del jugador un instante
        Collider2D myCollider = GetComponent<Collider2D>();
        myCollider.enabled = false;

        // Revisamos EXACTAMENTE el centro de la siguiente casilla
        Collider2D hit = Physics2D.OverlapPoint(targetPos);
        myCollider.enabled = true;

        // Si no hay nada o es un portal/estrella
        if (hit == null || hit.isTrigger)
        {
            transform.position = targetPos;
        }
        // Si chocamos con una caja
        else if (hit.GetComponent<BoxPushable>() != null)
        {
            Vector2 boxTargetPos = targetPos + (direction * cellSize);

            // Apagamos la caja para ver que hay detras de ella
            hit.enabled = false;
            Collider2D hitBehind = Physics2D.OverlapPoint(boxTargetPos);
            hit.enabled = true;

            // Si detras de la caja esta libre o hay un portal
            if (hitBehind == null || hitBehind.isTrigger)
            {
                // Movemos la caja y al jugador
                hit.transform.position = boxTargetPos;
                transform.position = targetPos;
            }
        }
    }
}