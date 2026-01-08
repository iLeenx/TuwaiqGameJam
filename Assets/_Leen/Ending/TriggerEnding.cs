using UnityEngine;

public class TriggerEnding : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EndingManager.Instance.DecideEnding();
    }
}
