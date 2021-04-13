using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{   

    [SerializeField] [Range(0.0f, 5.0f)] float movementSpeed = 1f;
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    string pathTagName = "Path";

    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }


    private void FindPath()
    {
        path.Clear();

        //Tag container that holds waypoints
        GameObject parent = GameObject.FindGameObjectWithTag(pathTagName);

        foreach (Transform child in parent.transform)
        {
            path.Add(child.GetComponent<Waypoint>());
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }


    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
            float travelPercent = 0;

            transform.LookAt(endPos);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        //put back in pool
        gameObject.SetActive(false);
    }

}