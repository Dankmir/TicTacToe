using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) => OnClick();

    public void OnPointerEnter(PointerEventData eventData) => OnHoverStateChanged(true);

    public void OnPointerExit(PointerEventData eventData) => OnHoverStateChanged(false);


    public virtual void OnClick() { }
    public virtual void OnHoverStateChanged(bool isHovered) { }
}
