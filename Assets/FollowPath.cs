using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Transform[] waypoints;   // Array to hold waypoints (cubes)
    public float moveSpeed = 5f;    // Speed at which the object moves
    private int currentWaypoint = 0; // Index of the current waypoint

    private void Update()
    {
        // Check if there are waypoints
        if (waypoints.Length == 0)
            return;

        // Move towards the current waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, moveSpeed * Time.deltaTime);

        // Rotate towards the current waypoint
        Vector3 direction = (waypoints[currentWaypoint].position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // Check if the object has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypoint++;

            // If the current waypoint is beyond the array length, reset to the first waypoint
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
}
