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
    //select square texture
    public Texture m_selectionTexture;
    //the mouse position to start drawing the select box texture in
    Vector2 m_InitialMousePos;
    //the framework responsible for positioning soldiers
    FormationManager m_formationManager;

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
        //initialise initial mouse position
        m_InitialMousePos = new Vector2();
        //initialise the formation manager
        m_formationManager = gameObject.GetComponent<FormationManager>();
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
        if (mousePos.x < 10.0f)
        {
            //translate the newPos as 10 * Time.deltatime to the left
            newPos = new Vector3(newPos.x - 10 * Time.deltaTime, newPos.y, newPos.z);
        }
        //if the X mouse position is greater than 1200 pixels to the right
        if (mousePos.x > 1250.0f)
        {
            //translate the newPos as 10 * Time.deltatime to the right
            newPos = new Vector3(newPos.x + 10 * Time.deltaTime, newPos.y, newPos.z);
        }
        //if the Y mouse position is less than 100 pixels to the bottom
        if (mousePos.y < 50.0f)
        {
            //translate the newPos as 10 * Time.deltatime backwards
            newPos = new Vector3(newPos.x, newPos.y, newPos.z - 10 * Time.deltaTime);
        }
        //if the Y mouse position is greater than 500 pixels to the top
        if (mousePos.y > 520.0f)
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
            if (Input.GetButtonDown("Fire1"))
            {
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

            else if (Input.GetButton("Fire1"))
            {
                Vector3 temp = gameObject.transform.position + cameraToWorldDirection * rch.distance;

                if (!m_selecting)
                {
                    foreach (GameObject g in m_selectedUnits)
                    {
                        g.GetComponent<UnitBehaviour>().Deselect();
                    }
                    m_selectedUnits.Clear();
                    m_selectionSquare.Vertex1 = temp;
                    m_selecting = true;
                    m_InitialMousePos = Input.mousePosition;
                }
                else
                {
                    m_selectionSquare.Vertex4 = temp;
                }

                

                
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Unit"))
                {
                    if (m_selectionSquare.Inside(g.transform.position))
                    {
                        m_selectedUnits.Add(g);
                    }
                }

                m_selecting = false;
                m_selectionSquare = new SelectSquare();
                foreach (GameObject g in m_selectedUnits)
                {
                    g.GetComponent<UnitBehaviour>().Select();
                }
            }
            else if (Input.GetButton("Fire2"))
            {
                switch (rch.collider.gameObject.tag)
                {
                    case "Plane":
                        //move all units towards where the mouse pointed to on the plane
                        GameObject[] units = m_selectedUnits.ToArray();
                        m_formationManager.MoveSelectedUnits(m_selectedUnits, gameObject.transform.position + cameraToWorldDirection * rch.distance);
                        //foreach (GameObject g in m_selectedUnits)
                        //{
                        //    g.GetComponent<UnitBehaviour>().Move(gameObject.transform.position + cameraToWorldDirection * rch.distance);
                        //}
                        break;
                }
            }
        }
    }

    void OnGUI()
    {
        if (m_selecting)
        {
            Vector2 rect1 = m_InitialMousePos;
            
            Vector2 rect2 = Input.mousePosition;
            Rect r = new Rect(rect1.x, Screen.height - rect1.y, rect2.x - rect1.x, (rect1.y - rect2.y));
            m_debugger.AppendDebugger(rect2.x + " " + rect2.y);
            //Rect r = new Rect(0, 0, 10, 10);
            GUI.DrawTexture(r, m_selectionTexture);
        }
    }
}
