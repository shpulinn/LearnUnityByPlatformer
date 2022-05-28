using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100.0f;
    [SerializeField] private Slider healthSlider;
    private Animator _animator;

    private float _health;
    
    private int _takeDamageAnimTrigger;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();

        _takeDamageAnimTrigger = Animator.StringToHash("TakeDamage");

        _health = totalHealth;
        UpdateHealth();
    }

    public void ReduceHealth(float damage)
    {
        _health -= damage;
        _animator.SetTrigger(_takeDamageAnimTrigger);

        UpdateHealth();
        
        if (_health <= 0.0f) Die();
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }

    private void UpdateHealth()
    {
        healthSlider.value = _health / totalHealth;
    }
}
