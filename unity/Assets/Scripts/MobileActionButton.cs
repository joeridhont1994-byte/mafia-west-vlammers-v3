using UnityEngine;
using UnityEngine.EventSystems;

public class MobileActionButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public enum Action{Interact,Sprint}
    public Action action;
    public void OnPointerDown(PointerEventData e){
        if(!MobileInput.I)return;
        if(action==Action.Interact)MobileInput.I.Interact();
        if(action==Action.Sprint)MobileInput.I.SetSprint(true);
    }
    public void OnPointerUp(PointerEventData e){if(MobileInput.I&&action==Action.Sprint)MobileInput.I.SetSprint(false);}
}
