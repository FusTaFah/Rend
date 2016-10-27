using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI : MonoBehaviour {

    List<string> m_abilities;

    CameraControls m_cameraReference;
    Texture2D tex;

    // Use this for initialization
    void Start () {
        m_abilities = new List<string>();
        m_cameraReference = gameObject.GetComponent<CameraControls>();

        Rect r = new Rect(new Vector2(0.0f, 3 * Screen.height / 4.0f), new Vector2(Screen.width, Screen.height / 4.0f));
        tex = new Texture2D((int)r.width, (int)r.height);
        Color[] colors = new Color[(int)r.width * (int)r.height];
        for (int i = 0; i < (int)r.height; i++)
        {
            for (int j = 0; j < (int)r.width; j++)
            {
                colors[i + j].a = 1.0f;
            }
        }
        tex.SetPixels(colors);
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
        GUI.DrawTexture(new Rect(new Vector2(0.0f, 3 * Screen.height / 4.0f), new Vector2(Screen.width, Screen.height / 4.0f)), tex);

        //list the abilities
        uint k = 1;
        foreach(string abilityName in m_abilities)
        {
            Rect buttonShape = new Rect(new Vector2((k * Screen.width) / 9, 8.0f * Screen.height / 10), new Vector2(Screen.width / 9, Screen.height / 9));
            if(GUI.Button(buttonShape, abilityName))
            {
                m_cameraReference.GetSelectedUnit().GetComponent<StructureBehaviour>().SpawnUnit(UnitType.FOOTSOLDIER);
            }
            k++;
        }
    }
}
