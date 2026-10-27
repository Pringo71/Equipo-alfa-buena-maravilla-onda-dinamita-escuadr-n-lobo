using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Solo el jugador se teletransporta (las cajas y otros objetos lo ignoran)
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        // Si el jugador usa Rigidbody2D, movemos el cuerpo fÃsico y frenamos su velocidad
        if (collision.TryGetComponent(out Rigidbody2D rb))
        {
            rb.position = destination.position;
            rb.linearVelocity = Vector2.zero; // En Unity 2022 o anterior usa: rb.velocity
        }
        else
        {
            collision.transform.position = destination.position;
        }
    }
}