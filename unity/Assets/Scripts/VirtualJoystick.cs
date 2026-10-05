using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler
{
    public RectTransform knob;
    public float radius=70f;
    RectTransform rect;
    void Awake(){rect=transform as RectTransform;}
    public void OnPointerDown(PointerEventData e){OnDrag(e);}
    public void OnDrag(PointerEventData e){
        Vector2 p;if(!RectTransformUtility.ScreenPointToLocalPointInRectangle(rect,e.position,e.pressEventCamera,out p))return;
        Vector2 v=Vector2.ClampMagnitude(p/radius,1);if(knob)knob.anchoredPosition=v*radius;
        if(MobileInput.I)MobileInput.I.move=v;
    }
    public void OnPointerUp(PointerEventData e){if(knob)knob.anchoredPosition=Vector2.zero;if(MobileInput.I)MobileInput.I.move=Vector2.zero;}
}
