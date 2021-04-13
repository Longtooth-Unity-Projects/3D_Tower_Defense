using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool isPlaceable = true;
    public bool IsPlaceable    { get { return isPlaceable; } }


    [SerializeField] private Defender defenderPrefab;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isPlaceable)
        {
            bool isPlaced = defenderPrefab.SpawnDefender(defenderPrefab, transform.position);
            // we want to make a tile nonplaceable if there is already a defender on it
            isPlaceable = !isPlaced;
        }
    }
}
