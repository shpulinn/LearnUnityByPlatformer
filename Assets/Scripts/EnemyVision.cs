using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private GameObject currentHitObject;
    [SerializeField] private float circeRadius;
    [SerializeField] private float maxVisionDistance;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Color gizmosColor;

    private EnemyMoveController _enemyMoveController;
    private Vector2 _origin;
    private Vector2 _direction;
    private float _currentHitDistance;

    private const string PlayerTag = "Player";

    private void Start()
    {
        _enemyMoveController = GetComponent<EnemyMoveController>();
    }

    private void Update()
    {
        _origin = transform.position;

        _direction = _enemyMoveController.IsFacingRight ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.CircleCast(_origin, circeRadius, _direction, maxVisionDistance, layerMask);

        if (hit)
        {
            currentHitObject = hit.transform.gameObject;
            _currentHitDistance = hit.distance;
            if (currentHitObject.CompareTag(PlayerTag))
            {
                _enemyMoveController.StartChasingPlayer();
            }
        }
        else
        {
            currentHitObject = null;
            _currentHitDistance = maxVisionDistance;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawLine(_origin, _origin + _direction * _currentHitDistance);
        Gizmos.DrawWireSphere(_origin + _direction * _currentHitDistance, circeRadius);
    }
}
