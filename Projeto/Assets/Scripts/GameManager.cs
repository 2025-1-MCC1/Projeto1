using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text cityNameText;
    public InputField cityNameInput;
    public GameObject cityNamePanel;

    void Start()
    {
        cityNamePanel.SetActive(true); // Ativa painel para escolher nome no começo
    }

    public void ConfirmCityName()
    {
        string name = cityNameInput.text;
        if (!string.IsNullOrEmpty(name))
        {
            cityNameText.text = name;
            cityNamePanel.SetActive(false);
        }
    }
}
