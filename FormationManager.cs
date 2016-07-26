using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FormationManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void MoveSelectedUnits(GameObject[] units, Vector3 waypoint)
    {
        Vector3 troopPosition = waypoint;
        Debug.Log(units.Length);
        int leftRight = 1;
        float xOffset = 0.0f;
        float yOffset = 0.0f;
        int index = 0;
        foreach(GameObject unit in units)
        {
            if(index >= 5)
            {
                yOffset += 1.5f;
                troopPosition.z = waypoint.z + yOffset;
                index = 0;
                leftRight = 1;
                xOffset = 0.0f ;
            }

            troopPosition.x = waypoint.x + xOffset * leftRight;
            
            unit.GetComponent<UnitBehaviour>().Move(troopPosition);
            leftRight = -leftRight;
            if (leftRight < 0) xOffset += 1.5f;
            index++;
        }

    }

    //public void MoveSelectedUnits(List<GameObject> units, Vector3 waypoint)
    //{
    //    int leftRight = 1;
    //    float offset = 0.0f;
    //    foreach (GameObject unit in units)
    //    {
    //        waypoint.x += (offset * leftRight);
    //        unit.GetComponent<UnitBehaviour>().Move(waypoint);
    //        //leftRight = -leftRight;
    //        Debug.Log(leftRight);
    //        /*if (leftRight > 0)*/ offset += 1.0f;
    //    }

    //}

    // Update is called once per frame
    void Update () {
	
	}
}
