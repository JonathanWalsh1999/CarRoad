using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private Transform[] lane1Waypoints;  // Waypoints for lane 1 (right edge)

    [SerializeField]
    private Transform[] lane2LeftWaypoints;  // Waypoints for lane 2 (left edge)

    [SerializeField]
    private Transform[] lane2RightWaypoints; // Waypoints for lane 2 (right edge)

    [SerializeField]
    private Transform[] lane3Waypoints;  // Waypoints for lane 3 (left edge)

    [SerializeField]
    private float speed = 10.0f;

    [SerializeField]
    private float waypointTolerance = 2.0f; //Basically the distance away from the current waypoint until it increments to the next one
                                            //As a rule of thumb set this to something similiar to the speed
    [SerializeField]
    private float laneWidth = 2.0f;  // Width of the lane

    private int currentWaypointIndex = 0;
    private int currentLane = 1;  // Current lane index (initialized to 1)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform targetWaypoint = null;

        if (currentLane == 1)
        {
            targetWaypoint = lane1Waypoints[currentWaypointIndex];
        }
        else if (currentLane == 2)
        {
            if (currentWaypointIndex % 2 == 0)
            {
                targetWaypoint = lane2LeftWaypoints[currentWaypointIndex / 2];
            }
            else
            {
                targetWaypoint = lane2RightWaypoints[currentWaypointIndex / 2];
            }
        }
        else if (currentLane == 3)
        {
            targetWaypoint = lane3Waypoints[currentWaypointIndex];
        }


        if (targetWaypoint != null)
        {
            //Transform targetWaypoint = waypoints[currentWaypointIndex];

            //// Calculate distances to waypoints and find the closest waypoint
            //float closestDistance = Mathf.Infinity;
            //int closestWaypointIndex = 0;

            //for (int i = 0; i < waypoints.Length; i++)
            //{
            //    float distance = Vector3.Distance(transform.position, waypoints[i].position);
            //    if (distance < closestDistance)
            //    {
            //        closestDistance = distance;
            //        closestWaypointIndex = i;
            //    }
            //}

            //// Set the closest waypoint as the target waypoint
            //targetWaypoint = waypoints[closestWaypointIndex];

            //// Calculate lateral offset
            //float lateralOffset = targetWaypoint.position.x - transform.position.x;

            //// Determine lane side
            //if (lateralOffset > 0)
            //{
            //    currentLane = 1;  // Right side of the lane
            //}
            //else
            //{
            //    currentLane = 2;  // Left side of the lane
            //}



            //// Calculate desired lateral offset from the center of the lane
            //float desiredLateralOffset = laneWidth * 0.5f;

            //// Calculate current lateral offset from waypoint's x-coordinate
            //float currentLateralOffset = targetWaypoint.position.x - transform.position.x;

            //// Calculate lane index based on lateral offset
            //int laneIndex = Mathf.RoundToInt(currentLateralOffset / laneWidth);

            //Debug.Log(laneIndex);

            //// Calculate lateral movement direction
            //Vector3 lateralDirection = new Vector3(currentLateralOffset + desiredLateralOffset, 0, 0).normalized;

            //// Translate the AI car's position along the lateral direction
            //transform.Translate(lateralDirection * Time.deltaTime * speed);

            //// Move towards the target waypoint's z-coordinate
            //float zDistance = Mathf.MoveTowards(transform.position.z, targetWaypoint.position.z, speed * Time.deltaTime);
            //transform.position = new Vector3(transform.position.x, transform.position.y, zDistance);

            //// Check if close enough to waypoint to move to the next one
            //if (Vector3.Distance(transform.position, targetWaypoint.position) < waypointTolerance)
            //{
            //    currentWaypointIndex++;
            //}
        }
    }

}



