using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalStars = 4;
    private int currentStars = 0;

    public TextMeshProUGUI starText;

    void Awake()
    {
        // Patron Singleton para acceder facil desde la estrella
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddStar()
    {
        currentStars++;
        UpdateUI();

        if (currentStars >= totalStars)
        {
            // Aqui puedes agregar logica de victoria
            Debug.Log("Has reunido todas las estrellas!");
        }
    }

    void UpdateUI()
    {
        if (starText != null)
        {
            starText.text = currentStars + "/" + totalStars;
        }
    }
}