using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private Animator _animator;
    private bool _isAttack = false;

    private int _attackTriggerID;
    
    public bool IsAttack => _isAttack;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        AssignAnimationsIDs();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isAttack = true;
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetTrigger(_attackTriggerID);
    }

    public void FinishAttack()
    {
        _isAttack = false;
    }
    
    private void AssignAnimationsIDs()
    {
        _attackTriggerID = Animator.StringToHash("Attack");
    }
}
