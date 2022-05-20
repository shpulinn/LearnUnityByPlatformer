using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float enemyDamage = 10.0f;
    [SerializeField] private float callDownTimer = 1.0f;

    private float _timer;
    private bool _isDamage = true;

    private void Start()
    {
        _timer = callDownTimer;
    }

    private void Update()
    {
        if (_isDamage) return;
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _isDamage = true;
            _timer = callDownTimer;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        var playerHealth = col.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null && _isDamage)
        {
            playerHealth.ReduceHealth(enemyDamage);
            _isDamage = false;
        }
    }
}
