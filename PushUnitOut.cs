using UnityEngine;
using System.Collections;

public class PushUnitOut : MonoBehaviour {

    bool m_selected;

    Vector3 m_unitRallyPoint;

    void Start()
    {
        Vector3 positionInFront = gameObject.transform.position + new Vector3(0.0f, 0.0f, 4.0f);
        m_unitRallyPoint = positionInFront;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && m_selected)
        {
            Vector3 positionInFront = gameObject.transform.position + new Vector3(0.0f, 0.0f, 4.0f);
            GameObject unit = Instantiate(Resources.Load("Prefabs/Unit"), positionInFront, Quaternion.identity) as GameObject;
            unit.GetComponent<UnitBehaviour>().m_pAllegiance = true;
            unit.GetComponent<UnitBehaviour>().Move(m_unitRallyPoint);
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

	void OnTriggerEnter(Collider col)
    {
        Debug.Log("Colliding");
        Vector3 fromThisToThem = col.gameObject.transform.position - gameObject.transform.position;
        fromThisToThem.y = 0.0f;
        col.gameObject.transform.position += (fromThisToThem/* + col.gameObject.transform.forward*/).normalized/* * Time.deltaTime * 20.0f*/;
    }
}
