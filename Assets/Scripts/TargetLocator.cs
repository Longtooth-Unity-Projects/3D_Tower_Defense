using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform target;  //TODO serialized for debugging


    private void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform;
    }


    void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        weapon.LookAt(target);
    }
}
