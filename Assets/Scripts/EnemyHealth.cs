using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitpoints = 5;
    [SerializeField] private int currentHitpoints = 0;   //TODO serialized for debuggin purposes


    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitpoints = maxHitpoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        processHit();
    }

    private void processHit()
    {
        Debug.Log("Hit");
        currentHitpoints--;

        if (currentHitpoints <= 0)
        {
            //put back in pool
            gameObject.SetActive(false);
        }
    }
}
