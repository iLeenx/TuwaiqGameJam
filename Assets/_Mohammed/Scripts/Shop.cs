using UnityEngine;

public class Shop : MonoBehaviour, Iinteractable
{
    private bool isStealing = false;
    private bool canCollect = false;
    private PlayerInventory _playerInventory;

    [SerializeField] private string _actionName;
    public ShoppingItem item;

    public string ActionName
    {
        get { return _actionName; }
        set { _actionName = value; }
    }

    private void Awake()
    {
    }

    private void Start()
    {
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }
    private void Update()
    {
        if(canCollect && gameObject.layer == 7)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)){
                isStealing = false;
                Interact();
            }else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                isStealing = true;
                Interact();
            }
        }
    }

    public void Interact()
    {
        if (isStealing)
        {
            if (_playerInventory != null)
            {
                canCollect = false;
                item.isCollected = true;
                item.isStolen = true;
                _playerInventory.Additem(item);
                GameUIManager.instance.HideChoicePanel();
                gameObject.layer = 0;
            }
        }
        else
        {
            bool isBought = GameManager.instance.spendCoins(item.price);
            if (isBought)
            {
                if (_playerInventory != null)
                {
                    canCollect = false;
                    item.isCollected = true;
                    _playerInventory.Additem(item);
                    GameUIManager.instance.HideChoicePanel();
                    gameObject.layer = 0;
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.layer == 7)
        {
            canCollect = true;
            GameUIManager.instance.SetShopNameText(ActionName);
            GameUIManager.instance.SetBuyAmount(item.price);
            GameUIManager.instance.ShowChoicePanel();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.layer == 7)
        {
            canCollect = false;
            GameUIManager.instance.SetShopNameText("");
            GameUIManager.instance.SetBuyAmount(0);
            GameUIManager.instance.HideChoicePanel();
        }
    }

}
