using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    public AudioClip sonidoEstrella;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddStar();
            }

            // Reproducir sonido al recolectar
            if (sonidoEstrella != null)
            {
                AudioSource.PlayClipAtPoint(
                    sonidoEstrella,
                    Camera.main.transform.position
                );
            }

            Destroy(gameObject);
        }
    }
}