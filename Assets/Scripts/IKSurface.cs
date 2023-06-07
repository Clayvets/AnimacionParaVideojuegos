using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BoolUnityevent : UnityEvent<bool>
{
    
}

public class IKSurface : MonoBehaviour
{
    //private const float REFRESH_DELAY = 0.5f;
    
    [SerializeField] private Transform detectionReference;
    [SerializeField] private Transform innerDetectionReference;
    [SerializeField] private float radius = 1.0f;
    [SerializeField] private LayerMask detectionLayers;
    public BoolUnityevent OnDetected;

    bool QueryNearbySurfacePosition(out Vector3 nearestPoint)
    {
        try
        {
            Collider[] objectsInRange = Physics.OverlapSphere(detectionReference.position, radius, detectionLayers);
        
            Vector3[] closestPoints = new Vector3[objectsInRange.Length];
            for (int i = 0; i < objectsInRange.Length; i++)
            {
                closestPoints[i] = objectsInRange[i].ClosestPoint(detectionReference.position);
            }

            Vector3 closestPoint = closestPoints.OrderBy(vector => Vector3.Distance(vector, detectionReference.position))
                .First();

            if (closestPoint != detectionReference.position)
            {
                nearestPoint = closestPoint;
            }
            else
            {
                Ray ray = new Ray(innerDetectionReference.position, detectionReference.position - innerDetectionReference.position);
                Physics.Raycast(ray, out RaycastHit hit, ray.direction.magnitude, detectionLayers);
                nearestPoint = hit.point;
            }
            return true;
        }
        catch
        {
            nearestPoint = transform.position;
            return false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 foundPoint = Vector3.zero;
        if (QueryNearbySurfacePosition(out foundPoint))
        {
            
            transform.position = foundPoint;
            OnDetected?.Invoke(true);
        }
        else
        {
            OnDetected?.Invoke(false);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectionReference.position, radius);
    }
}
