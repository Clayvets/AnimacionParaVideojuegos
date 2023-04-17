using System;
using UnityEngine;

public class IKSurface : MonoBehaviour
{
    private const float REFRESH_DELAY = 0.5f;
    
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask detectionMask;

    
    /// <summary>
    /// Decides wether or not a surface is suitable for IK snap 
    /// </summary>
    private Collider[] Query()
    {
        return Physics.OverlapSphere(transform.position, detectionRadius, detectionMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
