using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject HUDPanel;
    [SerializeField] private GameObject ChoicePanel;
    [SerializeField] private TextMeshProUGUI CoinText;
    [SerializeField] private TextMeshProUGUI ShopNameText;
    [SerializeField] private TextMeshProUGUI BuyText;

    public static GameUIManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ShowHUDPanel();
    }
    public void ShowHUDPanel()
    {
        HUDPanel.SetActive(true);
    }

    public void HideHUDPanel()
    {
        HUDPanel.SetActive(false);
    }

    public void ShowChoicePanel()
    {
        ChoicePanel.SetActive(true);
    }

    public void HideChoicePanel()
    {
        ChoicePanel.SetActive(false);
    }

    public void SetCoinText(int coinAmount)
    {
        CoinText.text = coinAmount.ToString();
    }

    public void SetShopNameText(string shopName)
    {
        ShopNameText.text = shopName;
    }

    public void SetBuyAmount(int coinAmount)
    {
        BuyText.text = $"Buy?\n(Price: {coinAmount})";
    }
}
