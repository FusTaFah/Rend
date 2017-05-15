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

    public abstract void UseAbility(Vector3 position);

    public abstract void UseAbility(Vector3 position, Vector3 target);

    public abstract void UseAbility(Vector3 position, GameObject target);

    public abstract void UseAbility(GameObject target);

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
