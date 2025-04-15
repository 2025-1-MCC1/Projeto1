using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text popularityText;
    public Text economyText;
    public Text populationText;
    public Text pollutionText;
    public Text happinessEmojiText;

    private float popularity = 50f;
    private float economy = 1000f;
    private int population = 10000;
    private float pollution = 20f;

    void Update()
    {
        UpdateHUD();
    }

    void UpdateHUD()
    {
        popularityText.text = popularity.ToString("F1") + "%";
        economyText.text = "£ " + economy.ToString("F0");
        populationText.text = population + " hab";
        pollutionText.text = pollution + "%";

        // Emojis de felicidade
        if (popularity >= 70)
            happinessEmojiText.text = "😃";
        else if (popularity >= 40)
            happinessEmojiText.text = "🙁";
        else
            happinessEmojiText.text = "😠";
    }
}
