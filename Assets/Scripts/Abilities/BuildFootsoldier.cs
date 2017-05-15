using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFootsoldier : UnitAbility {

    public BuildFootsoldier() : base("Build Footsoldier", 10.0f)
    {

    }

    public override void UseAbility()
    {
        
    }

    public override void UseAbility(GameObject target)
    {
        
    }

    public override void UseAbility(Vector3 position)
    {
        
    }

    public override void UseAbility(Vector3 position, GameObject target)
    {
        
    }

    public override void UseAbility(Vector3 position, Vector3 target)
    {
        Vector3 positionInFront = target - position;
        GameObject unit = GameObject.Instantiate(Resources.Load("Prefabs/Unit"), positionInFront, Quaternion.identity) as GameObject;
        unit.GetComponent<UnitBehaviour>().m_pAllegiance = true;
        unit.GetComponent<UnitBehaviour>().Move(target);
    }
}
