using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100.0f;
    [SerializeField] private Slider sliderHealth;
    private Animator _animator;

    private int _takeDamageAnimTrigger;
    private float _health;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();

        _takeDamageAnimTrigger = Animator.StringToHash("TakeDamagePlayer");

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
        sliderHealth.value = _health / totalHealth;
    }
}
