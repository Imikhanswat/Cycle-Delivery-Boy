using UnityEngine;
using UnityEngine.UI;

public class WayPoints : MonoBehaviour
{
    [Range(0f, 2f)]
    [SerializeField] private float WayPointsSize = 1f;

    public LineRenderer lineRenderer; // Expose the LineRenderer component as a public variable
    public Image fillImage; // Reference to the Image component for filling progress

    private Color lineColor;

    private float totalDistance; // Total distance between waypoints

    void Start()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component not assigned to WayPoints script.");
            return;
        }

        lineRenderer.positionCount = transform.childCount;
        lineRenderer.startWidth = 0.3f; // You can adjust the width of the line as needed
        lineRenderer.endWidth = 0.3f;

        // Get the initial color of the LineRenderer
        lineColor = lineRenderer.startColor;

        // Calculate the total distance between waypoints
        totalDistance = CalculateTotalDistance();

        // Initialize fill amount of the image
        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, WayPointsSize);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
    }

    public Transform GetNextWayPoint(Transform currentWayPoint)
    {
        if (currentWayPoint == null)
        {
            return transform.GetChild(0);
        }
        if (currentWayPoint.GetSiblingIndex() < transform.childCount - 1)
        {
            return transform.GetChild(currentWayPoint.GetSiblingIndex() + 1);
        }
        else
        {
            return transform.GetChild(0);
        }
    }

    public float CalculateTotalDistance()
    {
        float totalDistance = 0f;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            totalDistance += Vector3.Distance(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
        return totalDistance;
    }

    public void UpdateFillAmount(Transform currentWayPoint)
{
    if (fillImage != null)
    {
        float distanceCovered = 0f;
        int currentIndex = currentWayPoint.GetSiblingIndex();
        for (int i = 0; i < currentIndex; i++)
        {
            distanceCovered += Vector3.Distance(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        if (currentIndex < transform.childCount - 1)
        {
            Vector3 currentWaypointPosition = transform.GetChild(currentIndex).position;
            Vector3 nextWaypointPosition = transform.GetChild(currentIndex + 1).position;
            float distanceToNextWaypoint = Vector3.Distance(currentWaypointPosition, nextWaypointPosition);
            float distanceToCurrentWaypoint = Vector3.Distance(currentWaypointPosition, currentWayPoint.position);
            distanceCovered += Mathf.Clamp01(1f - distanceToCurrentWaypoint / distanceToNextWaypoint) * distanceToNextWaypoint;
        }

        fillImage.fillAmount = distanceCovered / totalDistance;
    }
}



    void Update()
    {
        // Update the positions of the line renderer to connect the waypoints
        for (int i = 0; i < transform.childCount; i++)
        {
            lineRenderer.SetPosition(i, transform.GetChild(i).position);
        }

        // Set the color of the LineRenderer
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }
}
