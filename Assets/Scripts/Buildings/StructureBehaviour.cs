using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StructureBehaviour : UnitBehaviour {

    Vector3 m_unitRallyPoint;

    void Start()
    {
        m_abilities = new List<UnitAbility>();
        Vector3 positionInFront = gameObject.transform.position + new Vector3(0.0f, 0.0f, 4.0f);
        m_unitRallyPoint = positionInFront + new Vector3(0.0f, 0.0f, 10.0f);
    }

    protected override void Update()
    {
        if (m_state != UnitState.DEAD)
        {
            m_attackTimer += Time.deltaTime;
            foreach (UnitAbility ability in m_abilities)
            {
                ability.UpdateCooldown(Time.deltaTime);
            }
            if (m_state == UnitState.MOVING)
            {
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

	protected override void OnTriggerEnter(Collider col)
    {
        Debug.Log("Colliding");
        Vector3 fromThisToThem = col.gameObject.transform.position - gameObject.transform.position;
        fromThisToThem.y = 0.0f;
        col.gameObject.transform.position += (fromThisToThem/* + col.gameObject.transform.forward*/).normalized/* * Time.deltaTime * 20.0f*/;
    }
}
