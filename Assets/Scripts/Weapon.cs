using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerAttackController _playerAttackController;

    private void Start()
    {
        _playerAttackController = transform.root.GetComponent<PlayerAttackController>();
    }

    // bad code for hit check :(
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name);
        if (_playerAttackController.IsAttack == false) return;
        if (col.TryGetComponent(out EnemyMoveController enemyMoveController))
        {
            Debug.Log("Hit");
        }
    }
}
