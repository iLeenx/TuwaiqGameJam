using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += resetCoins;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= resetCoins;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addNumberCoins(int noOfCoins)
    {
        _noOfCoins += noOfCoins;
        GameUIManager.instance.SetCoinText(_noOfCoins);
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
            GameUIManager.instance.SetCoinText(_noOfCoins);
            return true;
        }
    }

    public void resetCoins(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            _noOfCoins = 0;
            _noOfCoins = _startingCoins;
            GameUIManager.instance.SetCoinText(_noOfCoins);

        }
    }

    }
