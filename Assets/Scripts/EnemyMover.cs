using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{   

    [SerializeField] [Range(0.0f, 5.0f)] float movementSpeed = 1f;
    [SerializeField] List<Tile> path = new List<Tile>();

    Enemy enemyMe;
    string pathTagName = "Path";

    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }


    private void Start()
    {
        enemyMe = GetComponent<Enemy>();
    }


    private void FindPath()
    {
        path.Clear();

        //Tag container that holds waypoints
        GameObject parent = GameObject.FindGameObjectWithTag(pathTagName);

        foreach (Transform child in parent.transform)
        {
            path.Add(child.GetComponent<Tile>());
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    private void FinishPath()
    {
        GetComponent<Enemy>().TakeGold();

        //put back in pool
        gameObject.SetActive(false);
    }

    private IEnumerator FollowPath()
    {
        foreach (Tile tile in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = tile.transform.position;
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