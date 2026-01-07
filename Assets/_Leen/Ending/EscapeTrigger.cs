using UnityEngine;

public class EscapeTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTERED ESCAPE TRIGGER");
            EndingManager.Instance.OnEscapeTrigger();
        }
    }

}
