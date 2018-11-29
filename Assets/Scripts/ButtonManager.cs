using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour, IPointerDownHandler// required interface when using the OnPointerDown method.
{
    private Button button;
    private void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //play sound?
        Utilities.Vibrate();
    }
}
