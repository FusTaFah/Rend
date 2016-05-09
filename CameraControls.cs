using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraControls : MonoBehaviour {
    //game camera
    Camera m_cam;
    //game debugger
    DebugUtil m_debugger;
    //boolean to keep track of state of selecting
    bool m_selecting;
    //list of all selected units
    List<GameObject> m_selectedUnits;
    //selection square
    SelectSquare m_selectionSquare;

	// Use this for initialization
	void Start () {
        //initialise camera
        m_cam = Camera.main;
        //initialise debugger
        m_debugger = GameObject.Find("Canvas").GetComponentInChildren<DebugUtil>();
        //set selecting to false
        m_selecting = false;
        //initialise empty unit list
        m_selectedUnits = new List<GameObject>();
        //initialise selection square
        m_selectionSquare = new SelectSquare();
	}
	
	// Update is called once per frame
    void Update()
    {
        //obtain the current mouse position as pixels from the bottom left of the screen
        Vector3 mousePos = Input.mousePosition;
        //create a temporary variable to store the camera position
        Vector3 newPos = gameObject.transform.position;
        //print the mouse position onto the screen
        m_debugger.AppendDebugger("MousePosition: " + mousePos);

        //if the X mouse position is less than 100 pixels to the left
        if (mousePos.x < 100.0f)
        {
            //translate the newPos as 10 * Time.deltatime to the left
            newPos = new Vector3(newPos.x - 10 * Time.deltaTime, newPos.y, newPos.z);
        }
        //if the X mouse position is greater than 1200 pixels to the right
        if (mousePos.x > 1200.0f)
        {
            //translate the newPos as 10 * Time.deltatime to the right
            newPos = new Vector3(newPos.x + 10 * Time.deltaTime, newPos.y, newPos.z);
        }
        //if the Y mouse position is less than 100 pixels to the bottom
        if (mousePos.y < 100.0f)
        {
            //translate the newPos as 10 * Time.deltatime backwards
            newPos = new Vector3(newPos.x, newPos.y, newPos.z - 10 * Time.deltaTime);
        }
        //if the Y mouse position is greater than 500 pixels to the top
        if (mousePos.y > 500.0f)
        {
            //translate the newPos as 10 * Time.deltatime forward
            newPos = new Vector3(newPos.x, newPos.y, newPos.z + 10 * Time.deltaTime);
        }

        //set the camera position to the new position
        gameObject.transform.position = newPos;

        //store the mouse position in a temporary variable
        Vector3 cameraToWorld = mousePos;
        //translate this position such that it is on the near clipping plane
        cameraToWorld.z = m_cam.nearClipPlane;
        //obtain the world point of the near clipping plane
        Vector3 worldPoint = m_cam.ScreenToWorldPoint(cameraToWorld);
        //show the position on the debugger
        m_debugger.AppendDebugger("WorldPoint: " + worldPoint);
        //use the world point of the mouse and the camera's position to get a direction
        //towards the world
        Vector3 cameraToWorldDirection = (worldPoint - gameObject.transform.position).normalized;
        //use this position to initialise a ray

        Ray x = new Ray(gameObject.transform.position, cameraToWorldDirection);
        //prepare a raycast towards where the mouse is pointing
        RaycastHit rch;
        Physics.Raycast(x, out rch);

        if (rch.collider != null)
        {
            if (Input.GetButton("Fire1"))
            {
                Vector3 temp = cameraToWorldDirection * rch.distance;

                if (!m_selecting)
                {
                    m_selectionSquare.Vertex1 = new Vector2(temp.x, temp.z);
                    m_selecting = true;
                }
                else
                {
                    m_selectionSquare.Vertex4 = new Vector2(temp.x, temp.z);
                    m_selectionSquare.Vertex2 = new Vector2(m_selectionSquare.Vertex4.x, m_selectionSquare.Vertex1.y);
                    m_selectionSquare.Vertex3 = new Vector2(m_selectionSquare.Vertex4.y, m_selectionSquare.Vertex1.x);
                }
                //switch (rch.collider.gameObject.tag)
                //{
                //    case "Unit":
                //        rch.collider.gameObject.GetComponent<UnitBehaviour>().Select();
                //        m_selectedUnits.Add(rch.collider.gameObject);
                //        break;
                //    case "Plane":
                //        //deselect all units
                //        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Unit"))
                //        {
                //            g.GetComponent<UnitBehaviour>().Deselect();
                //        }
                //        break;
                //}
            }
            else if (Input.GetButton("Fire2"))
            {
                switch (rch.collider.gameObject.tag)
                {
                    case "Plane":
                        //move all units towards where the mouse pointed to on the plane
                        foreach (GameObject g in m_selectedUnits)
                        {
                            g.GetComponent<UnitBehaviour>().Move(gameObject.transform.position + cameraToWorldDirection * rch.distance);
                        }
                        break;
                }
            }
        }
    }
}
