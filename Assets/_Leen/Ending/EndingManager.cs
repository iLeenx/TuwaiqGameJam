using UnityEngine;
using TMPro;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    public int totalRequiredItems = 3;
    public int boughtCount = 0;
    public int stolenCount = 0;

    bool endingDecided = false;

    public bool playerCaughtByGuard = false;


    // view results in ui
    //public TMP_Text boughtText;
    //public TMP_Text stolenText;
    //public TMP_Text endingResults;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (EscapeConfirmUI.Instance == null)
        {
            Debug.LogError("EscapeConfirmUI is missing! activate it");
        }
    }

    public void RegisterItem(ItemObtainType type)
    {
        if (type == ItemObtainType.Bought)
        {
            boughtCount++;
            Debug.Log("Bought count: " + boughtCount);
        }
            
        else
        {
            stolenCount++;
            Debug.Log("Stolen count: " + stolenCount);
        }
    }

    public void DecideEnding()
    {
        // to call this 
        // EndingManager.Instance.DecideEnding();
    

        // to avoid multiple calls
        if (endingDecided == true ) return;

        endingDecided = true;

        if (playerCaughtByGuard)
        {
            LoadCaughtEnding();
            return;
        }

        if (stolenCount > boughtCount)
        {
            LoadStealEnding();
        }
        else if (boughtCount > stolenCount)
        {
            LoadGoodEnding();
        }
        else
        {
            DecideRandomEnding();
        }
    }


    void DecideRandomEnding()
    {
        float chance = Random.value; // 0 to 1
        Debug.Log("Random chance: " + chance);

        if (chance < 0.5f)
        {
            LoadGoodEnding();
        }
        else
        {
            LoadStealEnding();
        }
    }

    public bool HasAllItems()
    {
        return (boughtCount + stolenCount) >= totalRequiredItems;
    }

    public void ResetEndingData()
    {
        // in main menu or restart
        boughtCount = 0;
        stolenCount = 0;
        playerCaughtByGuard = false;
        endingDecided = false;
        
        Debug.Log("Ending data reset.");
    }

    public void OnPlayerCaught()
    {
        playerCaughtByGuard = true;
    }

    public void TriggerEscapeEnding()
    {
        if (endingDecided == true) return;

        endingDecided = true;

        LoadEscapeEnding();
    }

    public void OnEscapeTrigger()
    {
        if (endingDecided) return;

        if (playerCaughtByGuard)
        {
            endingDecided = true;
            LoadCaughtEnding();
            return;
        }

        if (HasAllItems())
        {
            // show confirmation UI
            EscapeConfirmUI.Instance.Show();
        }
        else
        {
            // not complete ? escape directly
            endingDecided = true;
            LoadEscapeEnding();
        }
    }


    void LoadStealEnding() 
    { 
        Debug.Log("STEAL ENDING"); 
    }
    void LoadGoodEnding() 
    { 
        Debug.Log("GOOD ENDING");
    }
    void LoadCaughtEnding() 
    { 
        Debug.Log("CAUGHT ENDING"); 
    }
    void LoadEscapeEnding() 
    { 
        Debug.Log("ESCAPE ENDING");
    }

    // ending signals

    // if bought button is clicked
    // EndingManager.Instance.RegisterItem(ItemObtainType.Bought);
    // if stolen button is clicked
    // EndingManager.Instance.RegisterItem(ItemObtainType.Stolen);


    // house door trigger / interaction -> here it start counting endings
    // EndingManager.Instance.DecideEnding();


    // when the NavMesh guard catches the player using trigger or collision
    // EndingManager.Instance.OnPlayerCaught();
    // if we want an immediate ending on caught, we can call this also
    // EndingManager.Instance.DecideEnding();


    // if player leaves the village area without being caught - used in a trigger
    // EndingManager.Instance.TriggerEscapeEnding();


}
