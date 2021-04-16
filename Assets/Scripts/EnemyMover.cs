using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ORDER: dependent on Pathfinder executing first

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{   

    [SerializeField] [Range(0.0f, 5.0f)] float movementSpeed = 1f;
    
    
    [SerializeField] List<Node> path = new List<Node>();    //TODO serialized for testing

    private Enemy enemyMe;
    private string pathTagName = "Path";

    private GridManager gridManager;
    private Pathfinder pathfinder;



    private void Awake()
    {
        enemyMe = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }


    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }




    //custom methods
    private void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
            coordinates = pathfinder.StartCoordinates;
        else
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

        //StopCoroutine(FollowPath());
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    private void ReturnToStart()
    {
        transform.position = gridManager.GetPostionFromCoordinates(pathfinder.StartCoordinates);
    }

    private void FinishPath()
    {
        GetComponent<Enemy>().TakeGold();

        //put back in pool
        gameObject.SetActive(false);
    }

    private IEnumerator FollowPath()
    {
        for(int i = 1; i < path.Count; ++i)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPostionFromCoordinates(path[i].coordinates);
            float travelPercent = 0;

            transform.LookAt(endPos);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}