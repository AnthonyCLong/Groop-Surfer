using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class ToggleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public Sprite deselectSprite;
    public Sprite hoverSprite;
    public Sprite selectSprite;

    public bool selected;

    public UnityEvent onClick;

    public void Start() {
        transform.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.01f;

        if (selected) {
            transform.GetComponent<Image>().sprite = selectSprite;
        } else {
            transform.GetComponent<Image>().sprite = deselectSprite;
        }
    }

    //When pointer enters
    public void OnPointerEnter(PointerEventData pointerEventData) {
        if (pointerEventData.pointerEnter.gameObject == gameObject) {
            transform.GetComponent<Image>().sprite = hoverSprite;
        }
    }

    //When pointer exits
    public void OnPointerExit(PointerEventData pointerEventData) {
        if (selected) {
            transform.GetComponent<Image>().sprite = selectSprite;
        } else {
            transform.GetComponent<Image>().sprite = deselectSprite;
        }
    }

    //When pointer clicks
    public void OnPointerClick(PointerEventData pointerEventData) {
        onClick.Invoke();

        if (selected) {
            selected = false;
        } else {
            selected = true;
        }
    }

}
