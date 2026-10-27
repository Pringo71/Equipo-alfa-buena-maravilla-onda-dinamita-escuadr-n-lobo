using UnityEngine;
using UnityEngine.InputSystem;

public class GridMovement : MonoBehaviour
{
    public float cellSize = 1f;

    void Update()
    {
        Vector2 movement = Vector2.zero;

        if (Keyboard.current == null)
            return;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame ||
            Keyboard.current.wKey.wasPressedThisFrame)
        {
            movement = Vector2.up;
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame ||
                 Keyboard.current.sKey.wasPressedThisFrame)
        {
            movement = Vector2.down;
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame ||
                 Keyboard.current.aKey.wasPressedThisFrame)
        {
            movement = Vector2.left;
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame ||
                 Keyboard.current.dKey.wasPressedThisFrame)
        {
            movement = Vector2.right;
        }

        Vector3 step = new Vector3(
            movement.x,
            movement.y,
            0f
        );

        transform.position += step * cellSize;
    }
}