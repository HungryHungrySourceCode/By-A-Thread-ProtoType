using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [Header("Initialization")]
    public GameMood gameMoodScript;
    public Transform mainTransform;

    [Header("Attributes")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float armor = 0f;
    public float damageRatio = 1f;

    [Header("Static, Npc, Enemy, PLayer")]
    public string type = "Static";


    [Header("RuntimeVariables")]
    public bool invunerable = false;
    public bool canHeal = true;
    public bool dead;
    public bool targetable = true;

    public bool CheckIfDead(float damage, float health)
    {
        if (health - (damage - armor) * damageRatio * gameMoodScript.globalDamageMult <= 0 && invunerable == false)
            return true;
        else
            return false;
    }

    private void Awake()
    {


    }

    private void Start()
    {
        gameMoodScript = GameMood.instance;
        OnSpawn();
    }

    public void OnSpawn()
    {
        maxHealth *= gameMoodScript.globalMaxHealthMult;
        currentHealth = maxHealth;
        StartCoroutine(Regen());
    }

    public void TakeDamage(float amount)
    {
        if (CheckIfDead(amount, currentHealth))
        {
            currentHealth -= (amount - armor) * damageRatio * gameMoodScript.globalDamageMult;
            Die();
        }

        if (invunerable == false)
        {
            currentHealth -= (amount - armor) * damageRatio * gameMoodScript.globalDamageMult;
        }
    }

    public void Heal(float amount)
    {
        if ((currentHealth + (amount - armor)) >= maxHealth && canHeal == true)
        currentHealth = maxHealth;
        else
        {
            currentHealth += (amount - armor) * gameMoodScript.globalHealingMult;
        }
    }

    public void Die()
    {
        targetable = false;
        dead = true;
        Destroy(gameObject);
    }

    public IEnumerator Regen()
    {
        while (true)
        {
            if (currentHealth <= maxHealth)
            {
                currentHealth += gameMoodScript.healthRegenPerTick * gameMoodScript.globalHealingMult;
            }
            yield return new WaitForSeconds(1f);
        }
    }

}
