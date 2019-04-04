using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayectoriaB : MonoBehaviour
{
    public Vector3 currentPos;
    public LineRenderer lineRenderer;
	// Use this for initialization
	void Start ()
    {
       // currentPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateTrajectory(this.transform.position,transform.forward,30,0.1f,100.0f);
	}

    void UpdateTrajectory(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, float maxTravelDistance)
    {
        var positions = new List<Vector3>();
        var lastPos = startPos;

        positions.Add(startPos);

        var traveledDistance = 0.0f;
        while (traveledDistance < maxTravelDistance)
        {
            traveledDistance += speed * timePerSegmentInSeconds;
            var hasHitSomething = TravelTrajectorySegment(currentPos, direction, speed, timePerSegmentInSeconds, positions);
            if (hasHitSomething)
            {
                break;
            }
            lastPos = currentPos;
            currentPos = positions[positions.Count - 1];
            direction = currentPos - lastPos;
            direction.Normalize();
        }

        BuildTrajectoryLine(positions);
    }

    bool TravelTrajectorySegment(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, List<Vector3> positions)
    {
        var newPos = startPos + direction * speed * timePerSegmentInSeconds + Physics.gravity * timePerSegmentInSeconds;

        RaycastHit hitInfo;
        var hasHitSomething = Physics.Raycast(startPos, newPos, out hitInfo);
        if (hasHitSomething)
        {
            newPos = hitInfo.transform.position;
        }
        positions.Add(newPos);

        return hasHitSomething;
    }

    void BuildTrajectoryLine(List<Vector3> positions)
    {
        //lineRenderer.SetVertexCount(positions.Count);
        lineRenderer.positionCount = positions.Count;
        for (var i = 0; i < positions.Count; ++i)
        {
            lineRenderer.SetPosition(i, positions[i]);
        }
    }
}
