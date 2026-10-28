using UnityEngine;

public class MenuInicio : MonoBehaviour
{
    public GameObject menuInicio;
    public PlayerMovement playerMovement;

    public void IniciarJuego()
    {
        menuInicio.SetActive(false);
        playerMovement.juegoIniciado = true;
    }
}