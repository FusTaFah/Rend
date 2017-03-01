using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBuilding : UnitAbility{

    public BuildBuilding(string abilityName, float maxCooldown): base(abilityName, maxCooldown)
    {
        
    }

    public override void UseAbility()
    {
        if(m_cooldown <= 0.0f)
        {

        }
    }
}
