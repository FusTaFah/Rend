using UnityEngine;
using System.Collections;

public abstract class UnitAbility{

    protected float m_maxCooldown;
    protected float m_cooldown;
    protected string m_abilityName;

    public UnitAbility(string abilityName, float maxCooldown)
    {
        m_abilityName = abilityName;
        m_maxCooldown = maxCooldown;
        m_cooldown = 0.0f;
    }

    public void UpdateCooldown(float deltaTime)
    {
        if(m_cooldown <= 0.0f)
        {
            m_cooldown = 0.0f;
        }
        else
        {
            m_cooldown -= deltaTime;
        }
        
    }

    public abstract void UseAbility();

    public string GetAbilityName()
    {
        return m_abilityName;
    }

    public float GetCurrentCooldown()
    {
        return m_cooldown;
    }

    public float GetMaxCooldown()
    {
        return m_maxCooldown;
    }
}
