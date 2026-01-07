using UnityEngine;

public class Shop : MonoBehaviour, Iinteractable
{
    private bool isStealing = false;
    private bool canCollect = false;
    private Collider _colldier;
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
        _colldier = GetComponent<Collider>();
    }

    private void Start()
    {
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }
    private void Update()
    {
        if(canCollect)
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
                _colldier.enabled = false;
                item.isCollected = true;
                item.isStolen = true;
                _playerInventory.Additem(item);
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
                    _colldier.enabled = false;
                    item.isCollected = true;
                    _playerInventory.Additem(item);
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCollect = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canCollect = false;
        }
    }

}
