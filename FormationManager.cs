using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FormationManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void MoveSelectedUnits(GameObject[] units, Vector3 waypoint)
    {
        int leftRight = 1;
        float offset = 1.5f;
        foreach(GameObject unit in units)
        {
            Debug.Log(offset);
            waypoint.x += offset * leftRight;
            unit.GetComponent<UnitBehaviour>().Move(waypoint);
            leftRight = -leftRight;
            if (leftRight > 0) offset += 1.5f;
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
