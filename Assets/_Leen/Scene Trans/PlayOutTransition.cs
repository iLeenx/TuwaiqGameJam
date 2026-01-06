using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class PlayOutTransition : MonoBehaviour
{
    public Animator transitionAnimator; // assign your animator here
    public float transitionTime = 1f;   // fallback if animation length is unknown

    // call this when you want to load a new scene
    public void LoadScene(string sceneName)
    {
        StartCoroutine(PlayTransition(sceneName));
    }

    private IEnumerator PlayTransition(string sceneName)
    {
        // play the animation
        transitionAnimator.SetTrigger("Start"); // make sure you have a trigger called "Start"

        // wait until animation finishes
        if (transitionAnimator != null)
        {
            // wait for the length of the current animation clip
            AnimatorStateInfo stateInfo = transitionAnimator.GetCurrentAnimatorStateInfo(0);
            float clipLength = transitionAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            yield return new WaitForSeconds(clipLength);
        }
        else
        {
            yield return new WaitForSeconds(transitionTime);
        }

        // now load the scene
        SceneManager.LoadScene(sceneName);
    }
}
