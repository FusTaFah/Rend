using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI : MonoBehaviour {

    List<string> m_abilities;

    CameraControls m_cameraReference;

	// Use this for initialization
	void Start () {
        m_abilities = new List<string>();
        m_cameraReference = gameObject.GetComponent<CameraControls>();

    }
	
	// Update is called once per frame
	void Update () {
	    if(m_cameraReference.GetSelectedUnit() != null)
        {
            if(m_cameraReference.GetSelectedUnit().tag == "AllyUnit")
            {
                m_abilities = m_cameraReference.GetSelectedUnit().GetComponent<UnitBehaviour>().GetAbilityList();
            }else if(m_cameraReference.GetSelectedUnit().tag == "Building")
            {
                m_abilities = m_cameraReference.GetSelectedUnit().GetComponent<StructureBehaviour>().GetAbilityList();
            }
        }
        else
        {
            m_abilities.Clear();
        }
	}

    void OnGUI()
    {
        //draw the background of the gui
        Rect r = new Rect(new Vector2(0.0f, Screen.height / 4.0f), new Vector2(Screen.width, 0.0f));
        GUIStyle gui = new GUIStyle();
        Texture2D tex = new Texture2D((int)r.width, (int)r.height);
        Color[] colors = { Color.gray };
        tex.SetPixels(colors);
        gui.normal.background = tex;
        GUI.Box(new Rect(new Vector2(0.0f, Screen.height / 4.0f), new Vector2(Screen.width, 0.0f)), "", gui);

        //list the abilities
        uint i = 1;
        foreach(string abilityName in m_abilities)
        {
            Rect buttonShape = new Rect(new Vector2((i * Screen.width) / 9, 8.0f * Screen.height / 10), new Vector2(Screen.width / 9, Screen.height / 9));
            GUI.Button(buttonShape, abilityName);
            i++;
        }
    }
}
