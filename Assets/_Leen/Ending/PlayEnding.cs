using UnityEngine;

public class PlayEnding : MonoBehaviour
{
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

    public void _1BoughtItem()
    {
        EndingManager.Instance.RegisterItem(ItemObtainType.Bought);
    }

    public void _2StolenItem()
    {
        EndingManager.Instance.RegisterItem(ItemObtainType.Stolen);
    }

    public void _3DecideEnding()
    {
        EndingManager.Instance.DecideEnding();
    }

    public void _4PlayerCaught()
    {
        EndingManager.Instance.OnPlayerCaught();
        EndingManager.Instance.DecideEnding();
    }

    public void _5TriggerEscapeEnding()
    {
        EndingManager.Instance.TriggerEscapeEnding();
    }

}
