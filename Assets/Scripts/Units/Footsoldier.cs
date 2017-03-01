using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsoldier : UnitBehaviour {

	// Use this for initialization
	void Start () {
        InitializeUnit();
        m_abilities.Add(new BuildBuilding("Barraks", 0.0f));
	}
}
