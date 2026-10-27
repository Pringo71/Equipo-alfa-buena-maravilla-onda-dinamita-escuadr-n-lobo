using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos si el personaje principal entra en la estrella
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddStar();
            }

            // Destruimos la estrella del escenario
            Destroy(gameObject);
        }
    }
}