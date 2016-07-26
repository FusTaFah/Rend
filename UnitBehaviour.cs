using UnityEngine;
using System.Collections;

public class UnitBehaviour : MonoBehaviour {
    //use the event system!

    //boolean which states whether or not this unit has been selected
    bool m_isSelected;
    //position the unit is to be moved to
    Vector3 m_movementPosition;
    //range of this unit's attack
    float m_attackRange;
    //projectile firing manager
    BulletManager m_bulletManager;
    //allegiance of this unit
    public bool m_pAllegiance;
    //states whether or not this unit is attacking
    bool m_attacking;

	// initialises the fields
	void Start () {
        m_attacking = false;
        m_attackRange = 5.0f;
        m_isSelected = false;
        m_movementPosition = gameObject.transform.position;
        m_bulletManager = gameObject.GetComponent<BulletManager>();
        gameObject.tag = m_pAllegiance ? "AllyUnit" : "EnemyUnit";
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
        if ((m_movementPosition - gameObject.transform.position).magnitude >= (m_attacking ? m_attackRange : 0.5f))
        {
            //move towards the goal position
            //gameObject.transform.forward = directionToGoal;
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
        m_attacking = false;
        if (m_isSelected)
        {
            m_movementPosition = newPosition;
        }
    }

    public void Attack(GameObject target)
    {
        m_attacking = true;
        Vector3 towardsTarget = target.transform.position - gameObject.transform.position;
        if (towardsTarget.sqrMagnitude > m_attackRange * m_attackRange)
        {
            if (m_isSelected)
            {
                m_movementPosition = target.transform.position;
            }
        }
        else
        {
            //m_bulletManager.SpawnBullet(gameObject.transform.position, Quaternion.FromToRotation(gameObject.transform.forward, towardsTarget.normalized), towardsTarget.normalized * 10.0f, m_pAllegiance ? "AllyBullet" : "EnemyBullet");
            m_bulletManager.SpawnBullet(gameObject.transform.position, Quaternion.FromToRotation(gameObject.transform.forward, towardsTarget.normalized), target.transform.position, m_pAllegiance ? "AllyBullet" : "EnemyBullet");
        }
    }
    
    ////collision behaviour of this unit
    //public void OnCollisionEnter(Collision coll)
    //{
    //    //if this body collides with a unit
    //    if (coll.collider.gameObject.tag == "Unit")
    //    {
    //        //stop
    //        m_movementPosition = gameObject.transform.position;
    //        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
    //    }
    //}

    //public void OnTriggerEnter(Collider coll)
    //{
    //    if(coll.gameObject.tag == "Unit")
    //    {
    //        Debug.Log("colliding");
    //        Vector3 directionAway = -(coll.transform.position - gameObject.transform.position).normalized;
    //        gameObject.transform.position += directionAway * Time.deltaTime * 20.0f;
    //    }
        
    //}
}
