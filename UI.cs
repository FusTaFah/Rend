using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI : MonoBehaviour {

    List<string> m_abilities;
    GameObject m_selectedUnit;

	// Use this for initialization
	void Start () {
        m_abilities = new List<string>();
        m_selectedUnit = null;
	}
	
	// Update is called once per frame
	void Update () {
        m_selectedUnit = gameObject.GetComponent<CameraControls>().GetSelectedUnit();
	    if(m_selectedUnit != null)
        {
            if(m_selectedUnit.tag == "AllyUnit")
            {
                m_selectedUnit.GetComponent<UnitBehaviour>();
            }else if(m_selectedUnit.tag == "Building")
            {
                m_selectedUnit.GetComponent<StructureBehaviour>();
            }
        }
	}

    void OnGUI()
    {
        uint i = 1;
        foreach(string abilityName in m_abilities)
        {
            Rect r = new Rect(new Vector2((i * Screen.width) / 9, 8.0f * Screen.height / 10), new Vector2(Screen.width / 9, Screen.height / 9));
            GUI.Button(r, "ability nam");
            i++;
        }
        
    }
}
