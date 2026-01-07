using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject shopPanel;

    [Header("Refs")]
    public Enemy shopkeeperEnemy;   
    public Transform player;

    [Header("Settings")]
    public float interactDistance = 2.5f;

    bool isOpen;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        CloseShop();
    }

    void Update()
    {
        if (player == null || shopkeeperEnemy == null) return;

    
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            float d = Vector3.Distance(player.position, shopkeeperEnemy.transform.position);
            if (d <= interactDistance)
            {
                if (isOpen) CloseShop();
                else OpenShop();
            }
        }
    }

    public void OpenShop()
    {
        
        isOpen = true;
        if (shopPanel != null) shopPanel.SetActive(true);

    
    }

    public void CloseShop()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        isOpen = false;
        if (shopPanel != null) shopPanel.SetActive(false);

    }

  
    public void Buy()
    {
     
        CloseShop();
    }

    
    public void Steal()
    {
        shopkeeperEnemy.OnTheft();  
        CloseShop();
    }
}
