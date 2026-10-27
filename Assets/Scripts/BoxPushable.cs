using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPushable : MonoBehaviour
{
    public float pushSpeed = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        // Obtenemos el componente Rigidbody2D de esta caja
        rb = GetComponent<Rigidbody2D>();
    }

    // Usamos Stay2D para que la caja se mueva mientras el jugador la siga empujando
    private void OnCollisionStay2D(Collision2D other) 
    {
        // Comprobamos si el objeto que nos toca tiene el script del jugador
        if (other.gameObject.GetComponent<PlayerController>() != null) 
        {
            // Obtenemos las fisicas del jugador para saber hacia donde nos empuja
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            
            if (playerRb != null) 
            {
                // Calculamos la direccion del empuje y aplicamos la velocidad a la caja
                Vector2 pushDirection = playerRb.linearVelocity.normalized;
                rb.linearVelocity = pushDirection * pushSpeed;
            }
        }
    }
}