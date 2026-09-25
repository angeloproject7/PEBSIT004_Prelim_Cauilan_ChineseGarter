using UnityEngine;
using UnityEngine.EventSystems;

public class MobileMoveButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler
{
    public PlayerMovement playerMovement;
    public float direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerMovement != null)
        {
            playerMovement.SetMobileMove(direction);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (playerMovement != null)
        {
            playerMovement.StopMobileMove();
        }
    }
}