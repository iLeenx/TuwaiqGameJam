using UnityEngine;

public class EscapeConfirmUI : MonoBehaviour
{
    public static EscapeConfirmUI Instance;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // YES button
    public void OnConfirmEscape()
    {
        Hide();
        EndingManager.Instance.TriggerEscapeEnding();
    }

    // NO button
    public void OnCancelEscape()
    {
        Hide();
    }
}
