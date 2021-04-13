using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private float weaponRange = 25.0f;
    [SerializeField] private ParticleSystem projectileParticles;

    //TODO change this to be an enemy object
    private Transform target;

    void Update()
    {
        //TODO only want to call this if target is destroyed or out of range
        FindClosestTarget();

        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float currentTargetDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float newTargetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (newTargetDistance < currentTargetDistance)
            {
                closestTarget = enemy.transform;
                currentTargetDistance = newTargetDistance;
            }
        }
        target = closestTarget;
    }//end of method FindClosestTarget()

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        
        weapon.LookAt(target);
        Attack(targetDistance < weaponRange);
    }

    private void Attack(bool isActive)
    {
        ParticleSystem.EmissionModule emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
