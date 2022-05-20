using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    private Animator _animator;

    private int _takeDamageAnimTrigger;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _takeDamageAnimTrigger = Animator.StringToHash("TakeDamage");
    }

    public void ReduceHealth(float damage)
    {
        health -= damage;
        _animator.SetTrigger(_takeDamageAnimTrigger);
        if (health <= 0.0f) Die();
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }
}
