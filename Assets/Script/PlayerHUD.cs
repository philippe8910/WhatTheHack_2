using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject HUD;

    [SerializeField] private Text HintText;
    
    [SerializeField] private GameObject PlayerNoramlUI;
    
    [SerializeField] private GameObject PlayerFreezeUI;
    
    [SerializeField] private GameObject PlayerDieUI;

    [SerializeField] private Image HintImage;

    [SerializeField] private Sprite ComputerRepairSprite , HelpAlleySprite;

    [SerializeField] public Slider ComputerSlider;

    // Start is called before the first frame update
    void Start()
    {
        HintImage = transform.GetComponentInChildren<Image>();
        HintText = transform.GetComponentInChildren<Text>();
        PlayerDieUI = transform.GetComponentInChildren<GameObject>();
        PlayerFreezeUI = transform.GetComponentInChildren<GameObject>();
        PlayerNoramlUI = transform.GetComponentInChildren<GameObject>();
        ComputerSlider = transform.GetComponentInChildren<Slider>();
    }

    public void ShowRepairComputerHint()
    {
        ShowHint();
        SetHintImage(ComputerRepairSprite);
        SetHintText("Repair Computer");
    }

    public void PlayerNormal()
    {
        PlayerNoramlUI.SetActive(true);
        PlayerFreezeUI.SetActive(false);
        PlayerDieUI.SetActive(false);
    }
    
    public void PlayerFreeze()
    {
        PlayerNoramlUI.SetActive(false);
        PlayerFreezeUI.SetActive(true);
        PlayerDieUI.SetActive(false);
    }

    public void PlayerDie()
    {
        PlayerNoramlUI.SetActive(false);
        PlayerFreezeUI.SetActive(false);
        PlayerDieUI.SetActive(true);
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