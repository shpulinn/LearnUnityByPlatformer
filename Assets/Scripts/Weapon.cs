using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float weaponDamage = 20.0f;
    private PlayerAttackController _playerAttackController;

    private void Start()
    {
        _playerAttackController = transform.root.GetComponent<PlayerAttackController>();
    }

    //bad code for hit check :(
    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (_playerAttackController.IsAttack == false) return;
        EnemyHealth enemyHealth = col.GetComponent<EnemyHealth>();
        if (enemyHealth != null && _playerAttackController.IsAttack)
        {
            enemyHealth.ReduceHealth(weaponDamage);
            col = null;
        }
        //enemyHealth.ReduceHealth(weaponDamage);
        // if (col.TryGetComponent(out EnemyHealth enemyHealth ))
        // {
        //     enemyHealth.ReduceHealth(weaponDamage);
        // }
    }
}
