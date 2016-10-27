using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitBehaviour : MonoBehaviour {
    //use the event system!

    //boolean which states whether or not this unit has been selected
    bool m_isSelected;
    //position the unit is to be moved to
    Vector3 m_movementPosition;
    //range of this unit's attack
    float m_attackRange;
    //distance to target that is considered close enough to stop
    float m_stoppingRange;
    //projectile firing manager
    BulletManager m_bulletManager;
    //allegiance of this unit
    public bool m_pAllegiance;
    //attacking target
    GameObject m_target;
    //attack speed of this target
    float m_attackSpeed;
    //time since the last attack
    float m_attackTimer;
    //health of this unit
    int m_health;
    //the bullet that hurts, this changes depending on the team this unit is on
    string m_damagingBullet;
    //timer for enemy scan
    float m_enemyInRangeScan;
    //max distance an idle unit can consider an enemy unit to be in targetting range
    float m_maxSearchRange;
    //unit state
    UnitState m_state;

    enum UnitState
    {
        IDLE,
        ATTACKING,
        MOVING,
        TARGETTING,
        DEAD
    }

    // initialises the fields
    void Start () {
        m_attackRange = 5.0f;
        m_stoppingRange = 0.5f;
        m_isSelected = false;
        m_movementPosition = gameObject.transform.position;
        m_bulletManager = gameObject.GetComponent<BulletManager>();
        gameObject.tag = m_pAllegiance ? "AllyUnit" : "EnemyUnit";
        m_damagingBullet = m_pAllegiance ? "EnemyBullet" : "AllyBullet";
        m_attackSpeed = 2.0f;
        m_attackTimer = 0.0f;
        m_health = 10;
        m_enemyInRangeScan = 0.0f;
        m_maxSearchRange = 7.0f;
        m_state = UnitState.IDLE;
	}
	
	// Update is called once per frame
	void Update () {
        if(m_state != UnitState.DEAD)
        {
            if (m_state == UnitState.MOVING)
            {
                MoveToTarget();
            }

            if (m_state == UnitState.ATTACKING)
            {
                if (m_target != null)
                {
                    if (m_attackTimer > m_attackSpeed)
                    {
                        m_bulletManager.SpawnBullet(gameObject.transform.position, Quaternion.identity, m_target, m_pAllegiance ? "AllyBullet" : "EnemyBullet");
                        m_attackTimer = 0.0f;
                    }
                    m_attackTimer += Time.deltaTime;
                    if ((m_target.transform.position - gameObject.transform.position).sqrMagnitude > m_attackRange * m_attackRange)
                    {
                        m_state = UnitState.TARGETTING;
                    }
                }
                else
                {
                    m_state = UnitState.IDLE;
                }
            }

            if (m_state == UnitState.TARGETTING)
            {
                if (m_target != null)
                {
                    m_movementPosition = m_target.transform.position;
                    if ((m_target.transform.position - gameObject.transform.position).sqrMagnitude > m_attackRange * m_attackRange)
                    {
                        MoveToTarget();
                    }
                    else
                    {
                        m_state = UnitState.ATTACKING;
                    }
                }
                else
                {
                    m_state = UnitState.IDLE;
                }
            }

            if (m_state == UnitState.IDLE)
            {
                if (m_enemyInRangeScan >= 2.0f)
                {
                    string enemy = m_pAllegiance ? "EnemyUnit" : "AllyUnit";
                    bool enemyFound = false;
                    foreach (GameObject g in GameObject.FindGameObjectsWithTag(enemy))
                    {
                        if ((g.transform.position - gameObject.transform.position).sqrMagnitude <= m_maxSearchRange * m_maxSearchRange)
                        {
                            Attack(g);
                            enemyFound = true;
                            break;
                        }
                    }
                    if (!enemyFound)
                    {
                        Debug.Log("Out of range");
                    }
                    m_enemyInRangeScan = 0.0f;
                }
                else
                {
                    m_enemyInRangeScan += Time.deltaTime;
                }
            }
        }
    }

    void MoveToTarget()
    {
        //get the direction from the current position to the goal
        Vector3 directionToGoal = (m_movementPosition - gameObject.transform.position).normalized;
        //if the unit is not already close enough to the goal
        if ((m_movementPosition - gameObject.transform.position).magnitude >= m_stoppingRange)
        {
            //move towards the goal position
            //gameObject.transform.forward = directionToGoal;
            gameObject.transform.forward = directionToGoal;
            //gameObject.transform.position = (gameObject.transform.position + gameObject.transform.TransformDirection(directionToGoal * Time.deltaTime * 10.0f));
            gameObject.transform.position = (gameObject.transform.position + gameObject.transform.forward * Time.deltaTime * 10.0f);
        }
        else
        {
            m_state = UnitState.IDLE;
        }
    }

    //select this unit
    public void Select()
    {
        m_isSelected = true;
        //change the colour of this unit to red
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    //deselect this unit
    public void Deselect()
    {
        m_isSelected = false;
        //if the unit is not selected, return the unit to its original colour
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
    }

    //set this unit's movement position
    public void Move(Vector3 newPosition)
    {
        m_state = UnitState.MOVING;
        m_movementPosition = newPosition;
    }

    public void Attack(GameObject target)
    {
        m_target = target;
        Vector3 towardsTarget = target.transform.position - gameObject.transform.position;
        if (towardsTarget.sqrMagnitude > m_attackRange * m_attackRange)
        {
            m_state = UnitState.ATTACKING;
        }
        else
        {
            m_state = UnitState.TARGETTING;
        }
    }

    //collision behaviour of this unit
    public void OnCollisionEnter(Collision coll)
    {
        
        if(coll.gameObject.tag == m_damagingBullet)
        {
            m_health -= 1;

            if (m_health <= 0)
            {
                m_state = UnitState.DEAD;
                Destroy(gameObject);
            }
        }

        if (coll.gameObject.tag == "AllyUnit" || coll.gameObject.tag == "EnemyUnit")
        {
            Debug.Log("colliding");
            Vector3 distanceAway = (gameObject.transform.position - coll.transform.position);
            gameObject.transform.position += distanceAway / 2.0f;
        }

        ////if this body collides with a unit
        //if (coll.collider.gameObject.tag == "Unit")
        //{
        //    //stop
        //    m_movementPosition = gameObject.transform.position;
        //    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        //}
    }

    public bool IsUnitDead()
    {
        return m_state == UnitState.DEAD;
    }

    public List<string> GetAbilityList()
    {
        List<string> abilities = new List<string>();
        abilities.Add("fuck");
        abilities.Add("da");
        abilities.Add("police");
        return abilities;
    }

    public bool IsSelected()
    {
        return m_isSelected;
    }
}
