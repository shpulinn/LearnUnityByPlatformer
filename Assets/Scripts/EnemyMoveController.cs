using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6.0f;
    [SerializeField] private float patrolSpeed = 1.0f;
    [SerializeField] private float chaseSpeed = 3.0f;
    [SerializeField] private float waitTime = 5.0f;
    [SerializeField] private float timeToChase = 3.0f;
    [SerializeField] private float minDistanceToPlayer = 1.5f;

    [SerializeField] private Transform enemyModelTransform;

    [SerializeField] private Color gizmosColor;

    private Rigidbody2D _rb;
    private Vector2 _leftBoundaryPosition;
    private Vector2 _rightBoundaryPosition;

    private Vector2 _nextPoint;

    private bool _isFacingRight = true;
    private bool _isWaiting = false;
    private float _waitingTime;
    private float _chaseTime;
    private float _currentWalkSpeed;

    private Vector3 _enemyTransformPosition;

    private bool _isChasingPlayer = false;
    private Transform _playerTransform;

    private const string PlayerTag = "Player";

    public bool IsFacingRight { get => _isFacingRight; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _leftBoundaryPosition = transform.position;
        _rightBoundaryPosition = _leftBoundaryPosition + Vector2.right * walkDistance;
        _waitingTime = waitTime;
        _chaseTime = timeToChase;
        _currentWalkSpeed = patrolSpeed;
        
        // bad code
        _playerTransform = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Transform>();
    }

    private void Update()
    {
        if (_isChasingPlayer)
        {
            StartChasingTimer();
        }
        if (_isWaiting && !_isChasingPlayer)
        {
            StartWaitTimer();
        }
        
        if (IsShouldWait()) _isWaiting = true;
    }

    private void FixedUpdate()
    {
        _nextPoint = Vector2.right * (_currentWalkSpeed * Time.fixedDeltaTime);
        
        // check minimal distance to prevent endless walking
        if (_isChasingPlayer && Mathf.Abs(DistanceToPlayer()) < minDistanceToPlayer) return;

        if (_isChasingPlayer) ChasePlayer();
        
        if (!_isChasingPlayer && !_isWaiting) Patrol();
    }

    public void StartChasingPlayer()
    {
        _isChasingPlayer = true;
        _chaseTime = timeToChase;
        _currentWalkSpeed = chaseSpeed;
    }

    private void ChasePlayer()
    {
        float distance = DistanceToPlayer();
        if (distance < 0) _nextPoint.x *= -1;
        if (distance > 0.2f && !_isFacingRight) Flip();
        else if (distance < 0.2f && _isFacingRight) Flip();
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private void Patrol()
    {
        if (!_isFacingRight) _nextPoint.x *= -1;
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private void StartWaitTimer()
    {
        _waitingTime -= Time.deltaTime;
        if (_waitingTime > 0f) return;
        _isWaiting = false;
        _waitingTime = waitTime;
        Flip();
    }

    private void StartChasingTimer()
    {
        _chaseTime -= Time.deltaTime;
        if (_chaseTime < 0f)
        {
            _isChasingPlayer = false;
            _chaseTime = timeToChase;
            _currentWalkSpeed = patrolSpeed;
        }
    }

    private bool IsShouldWait()
    {
        bool isOutOfRightBoundary = _isFacingRight && transform.position.x >= _rightBoundaryPosition.x;
        bool isOutOfLeftBoundary = !_isFacingRight && transform.position.x <= _leftBoundaryPosition.x;

        return isOutOfLeftBoundary || isOutOfRightBoundary;
    }
    
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        var playerScaleCurrent = enemyModelTransform.localScale;
        playerScaleCurrent.x *= -1;
        enemyModelTransform.localScale = playerScaleCurrent;
    }

    private float DistanceToPlayer()
    {
        return _playerTransform.position.x - transform.position.x;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawLine(_leftBoundaryPosition, _rightBoundaryPosition);
        Gizmos.DrawCube(_leftBoundaryPosition, Vector3.one);
        Gizmos.DrawCube(_rightBoundaryPosition, Vector3.one);
    }
}
