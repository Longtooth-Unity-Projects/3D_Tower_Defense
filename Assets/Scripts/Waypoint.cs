using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool isPlaceable = true;
    public bool IsPlaceable    { get { return isPlaceable; } }


    [SerializeField] private GameObject defenderPrefab;

    private string containerName = "ContainerForRuntimeSpawns";
    private GameObject container;

    private void Start()
    {
        container = GameObject.Find(containerName);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isPlaceable)
        {
            Instantiate(defenderPrefab, transform.position, Quaternion.identity, container.transform);
            isPlaceable = false;
        }
    }
}
