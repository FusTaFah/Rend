using UnityEngine;
using System.Collections;

public class UnitBehaviour : MonoBehaviour {
    //use the event system!

    //boolean which states whether or not this unit has been selected
    bool m_isSelected;
    //position the unit is to be moved to
    Vector3 m_movementPosition;
	// initialises the fields
	void Start () {
        m_isSelected = false;
        m_movementPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //if the unit is selected
        if (m_isSelected)
        {
            //change the colour of this unit to red
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else
        {
            //if the unit is not selected, return the unit to its original colour
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        }
        //get the direction from the current position to the goal
        Vector3 directionToGoal = (m_movementPosition - gameObject.transform.position).normalized;
        //if the unit is not already close enough to the goal
        if ((m_movementPosition - gameObject.transform.position).magnitude >= 0.5f)
        {
            //move towards the goal position
            gameObject.transform.position = (gameObject.transform.position + gameObject.transform.TransformDirection(directionToGoal * Time.deltaTime * 10.0f));
        }
	}

    //select this unit
    public void Select()
    {
        m_isSelected = true;
    }

    //deselect this unit
    public void Deselect()
    {
        m_isSelected = false;
    }

    //set this unit's movement position
    public void Move(Vector3 newPosition)
    {
        if (m_isSelected)
        {
            m_movementPosition = newPosition;
        }
    }
    
    //collision behaviour of this unit
    public void OnCollisionEnter(Collision coll)
    {
        //if this body collides with a unit
        if (coll.collider.gameObject.tag == "Unit")
        {
            //stop
            m_movementPosition = gameObject.transform.position;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
