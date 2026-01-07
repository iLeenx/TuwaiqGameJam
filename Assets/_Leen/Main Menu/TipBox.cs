using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // use RTLTMPro if you installed it

public class TipBox : MonoBehaviour
{
    public TMP_Text tipText; // assign your TMP text object here
    public float swapTime = 5f; // seconds

    [TextArea]
    public string[] tips;

    private int currentTip = 0;

    void Start()
    {
        if (tips.Length == 0 || tipText == null) return;

        tipText.text = tips[currentTip];
        StartCoroutine(SwapTipsRoutine());
    }

    IEnumerator SwapTipsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(swapTime);
            currentTip = (currentTip + 1) % tips.Length;
            tipText.text = tips[currentTip];
        }
    }
}
