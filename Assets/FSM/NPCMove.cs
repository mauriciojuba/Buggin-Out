using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour {

    [SerializeField]
    Transform _Player;

    NavMeshAgent _navMeshAgent;


    [Header("WayPoint")]
    public Transform[] waypoints;
    public float TimeToNextPoint = 5f;
    public float TimeToChangeTarget = 5f;
    private int currentWaypoint = 0;


    // Use this for initialization
    void Start ()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if(_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent is not attached");
        }
        else
        {
            SetPLayer(); 
        }
	}

    private void SetPLayer()
    {
        if(_Player != null)
        {
            Vector3 targetVector = _Player.transform.position;

            _navMeshAgent.SetDestination(targetVector);
        }
    }

    private void SetWayPoint()
    {
        if(waypoints != null)
        {
            if (Vector3.Distance(gameObject.transform.position, waypoints[currentWaypoint].position) < 1)
            {
                TimeToNextPoint -= Time.deltaTime;
                if (TimeToNextPoint < 0)
                {
                    currentWaypoint++;
                    if (currentWaypoint >= waypoints.Length)
                        currentWaypoint = 0;
                    TimeToNextPoint = TimeToChangeTarget;
                    _navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        SetWayPoint();
    }
}
