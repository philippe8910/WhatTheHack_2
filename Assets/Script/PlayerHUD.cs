using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject HUD;

    [SerializeField] private Text HintText;

    [SerializeField] private Image HintImage;

    [SerializeField] private Sprite ComputerRepairSprite , HelpAlleySprite;

    // Start is called before the first frame update
    void Start()
    {
        HintImage = transform.GetComponentInChildren<Image>();
        HintText = transform.GetComponentInChildren<Text>();
    }

    public void ShowRepairComputerHint()
    {
        ShowHint();
        SetHintImage(ComputerRepairSprite);
        SetHintText("Repair Computer");
    }

    public void ShowHelpAlleyHint()
    {
        ShowHint();
        SetHintImage(HelpAlleySprite);
        SetHintText("Help Alley");
    }

    private void ShowHint()
    {
        HUD.SetActive(true);
    }

    private void HideHint()
    {
        HUD.SetActive(false);
    }

    private void SetHintImage(Sprite _sprite)
    {
        HintImage.sprite = _sprite;
    }

    private void SetHintText(string _text)
    {
        HintText.text = _text;
    }
}