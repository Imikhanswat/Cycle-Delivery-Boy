using System.Collections;
using UnityEngine;

public class WayPointMover : MonoBehaviour
{
    [SerializeField] private WayPoints waypoints;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distanceThreshold = 10f;
    [SerializeField] private float rotationSpeed = 2f; // Adjust as needed
    private Transform currentWayPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentWayPoint = waypoints.GetNextWayPoint(currentWayPoint);
        transform.position = currentWayPoint.position;

        currentWayPoint = waypoints.GetNextWayPoint(currentWayPoint);
        StartCoroutine(RotateTowardsWaypoint());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWayPoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWayPoint.position) < distanceThreshold)
        {
            currentWayPoint = waypoints.GetNextWayPoint(currentWayPoint);
            StartCoroutine(RotateTowardsWaypoint());
        }
    }

    IEnumerator RotateTowardsWaypoint()
    {
        Vector3 targetDirection = (currentWayPoint.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
