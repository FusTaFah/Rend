using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBuilding : UnitAbility{

    public BuildBuilding(string abilityName, float maxCooldown): base(abilityName, maxCooldown)
    {
        
    }

    enum BuildState
    {
        NOT_BUILDING,
        READY_TO_BUILD,
        BUILDING
    }

    public void UpdateAbility()
    {

    }

    public override void UseAbility()
    {
        if(m_cooldown <= 0.0f)
        {

        }
    }

    public override void UseAbility(Vector3 position)
    {

    }

    public override void UseAbility(Vector3 position, Vector3 target)
    {

    }

    public override void UseAbility(Vector3 position, GameObject target)
    {

    }

    public override void UseAbility(GameObject target)
    {

    }
}
