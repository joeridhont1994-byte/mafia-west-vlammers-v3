using UnityEngine;
using UnityEngine.EventSystems;

public class TouchLookArea : MonoBehaviour,IDragHandler
{
    public float sensitivity=.12f;
    public void OnDrag(PointerEventData e){if(MobileInput.I)MobileInput.I.look=e.delta*sensitivity;}
}
