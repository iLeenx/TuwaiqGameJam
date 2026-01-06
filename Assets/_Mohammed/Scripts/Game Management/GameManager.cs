using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _noOfCoins;
    [SerializeField] private int _startingCoins;



    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _noOfCoins = _startingCoins;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addNumberCoins(int noOfCoins)
    {
        _noOfCoins += noOfCoins;
    }

    public bool spendCoins(int coinsToSpend)
    {
        if (coinsToSpend > _noOfCoins)
        {
            return false;
        }
        else
        {
            _noOfCoins -= coinsToSpend;
            if (coinsToSpend < 0)
            {
                _noOfCoins = 0;
            }
            return true;
        }
    }


}
