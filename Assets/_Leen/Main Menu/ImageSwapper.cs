using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSwapper : MonoBehaviour
{
    public Image targetImage;
    public Sprite[] images;
    public float swapTime = 5f;

    private int index = 0;

    void Start()
    {
        if (images.Length > 0)
            StartCoroutine(SwapImages());
    }

    IEnumerator SwapImages()
    {
        while (true)
        {
            targetImage.sprite = images[index];
            index = (index + 1) % images.Length;
            yield return new WaitForSeconds(swapTime);
        }
    }
}
