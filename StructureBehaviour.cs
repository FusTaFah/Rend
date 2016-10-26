using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StructureBehaviour : MonoBehaviour {

    public enum UnitType
    {
        FOOTSOLDIER,
        RIFLEMAN,
        HEAVY
    }

    bool m_selected;

    Vector3 m_unitRallyPoint;

    void Start()
    {
        Vector3 positionInFront = gameObject.transform.position + new Vector3(0.0f, 0.0f, 4.0f);
        m_unitRallyPoint = positionInFront;
    }

    void Update()
    {
        if (m_selected)
        {
            
        }
    }

    public void Select()
    {
        m_selected = true;
    }

    public void DeSelect()
    {
        m_selected = false;
    }

    public void SpawnUnit(UnitType unitType)
    {
        if(unitType == UnitType.FOOTSOLDIER)
        {
            Vector3 positionInFront = gameObject.transform.position + new Vector3(0.0f, 0.0f, 4.0f);
            GameObject unit = Instantiate(Resources.Load("Prefabs/Unit"), positionInFront, Quaternion.identity) as GameObject;
            unit.GetComponent<UnitBehaviour>().m_pAllegiance = true;
            unit.GetComponent<UnitBehaviour>().Move(m_unitRallyPoint);
        }
    }

	void OnTriggerEnter(Collider col)
    {
        Debug.Log("Colliding");
        Vector3 fromThisToThem = col.gameObject.transform.position - gameObject.transform.position;
        fromThisToThem.y = 0.0f;
        col.gameObject.transform.position += (fromThisToThem/* + col.gameObject.transform.forward*/).normalized/* * Time.deltaTime * 20.0f*/;
    }

    public bool IsSelected()
    {
        return m_selected;
    }

    public List<string> GetAbilityList()
    {
        List<string> abilities = new List<string>();
        abilities.Add("fuck");
        abilities.Add("da");
        abilities.Add("police");
        return abilities;
    }
}
