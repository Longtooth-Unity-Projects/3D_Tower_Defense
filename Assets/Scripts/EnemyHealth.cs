using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitpoints = 5;

    [Tooltip("Adds specified amount to max hitpoints when enemy is destroyed")]
    [SerializeField] private int difficultyRamp = 1;
    
    private int currentHitpoints = 0;

    //cached references
    Enemy enemyMe;


    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitpoints = maxHitpoints;
    }

    private void Start()
    {
        enemyMe = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        processHit();
    }

    private void processHit()
    {
        currentHitpoints--;

        if (currentHitpoints <= 0)
        {
            enemyMe.RewardGold();
            //put back in pool
            gameObject.SetActive(false);

            //TODO increase enemy difficulty, find a better way
            maxHitpoints += difficultyRamp;
        }
    }
}
