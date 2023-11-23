using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaypointMover : MonoBehaviour
{
    //Store a referenca to th wypoint system this abject will use
    [SerializeField] private Waypoints waypoints;

    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float distanceThreshold = 1f;
    [SerializeField] private float rotationSpeed = 2f; // Lis‰tty k‰‰ntymisnopeus


    // The current waypoint target that the object is moving towards
    private Transform currentWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial position to the firs waypoint
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        // Set the next waypoint target
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
    }

    void Update()
    {
        // oma lis‰ys lerp niin pehme‰mpi
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, step);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            //Ei tarvii kuin quaterniossa LookAt
            //transform.LookAt(currentWaypoint);
        }

        // K‰‰nny kohti seuraavaa waypointia pehme‰mmin
        Vector3 direction = currentWaypoint.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
