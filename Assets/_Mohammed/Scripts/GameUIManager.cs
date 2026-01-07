using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{

    [SerializeField] private GameObject HUDPanel;
    [SerializeField] private GameObject ChoicePanel;

    [SerializeField] private TextMeshProUGUI CoinText;
    [SerializeField] private TextMeshProUGUI ShopNameText;
    [SerializeField] private TextMeshProUGUI BuyText;
    [SerializeField] private GameObject PromptTextObj;

    [SerializeField] private GameObject BagContainer;
    [SerializeField] private GameObject ListContainer;
    [SerializeField] private Image[] ItemCheckList;

    public static GameUIManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowHUDPanel();
        ShowBagContainer();
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

    public void ShowBagContainer()
    {
        BagContainer.SetActive(true);
    }

    public void HideBagContainer()
    {
        BagContainer.SetActive(false);
    }

    public void ShowListContainer()
    {
        ListContainer.SetActive(true);
    }

    public void HideListContainer()
    {
        ListContainer.SetActive(false);
    }

    public void SetPromptText(string promptText)
    {
        PromptTextObj.GetComponent<TextMeshProUGUI>().text = promptText;
    }

    public void ShowPromptText()
    {
        Animator promptAnimator = PromptTextObj.GetComponent<Animator>();
        if (promptAnimator != null)
        {
            promptAnimator.SetTrigger("ToggleShow");
        }


    }

    public void ToggleCheckList(ShoppingItem item)
    {
        ItemCheckList[item.id].enabled = true;
        ItemCheckList[item.id].color = (item.isStolen) ? Color.red : Color.green;

    }

    public void HidePromptText()
    {
        PromptTextObj.SetActive(false);
    }
}
