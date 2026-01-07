using UnityEngine;

public class TabManager : MonoBehaviour
{
    // main menu ref
    public GameObject mainMenu;

    // all other tabs
    public GameObject[] tabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // activate main menu and deactivate all other tabs
        mainMenu.SetActive(true);
        foreach (GameObject tab in tabs)
        {
            tab.SetActive(false);
        }
    }
}
