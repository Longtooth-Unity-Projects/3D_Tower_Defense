using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] bool isPlaceable = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isPlaceable)
        {
            Debug.Log($"Pointer Down: {transform.name}");
        }
    }
}
