//======================================================================================================
// UnitStats.cs
// Description: Base Stat system all units should contain
// Author: Reynald Brassard
//======================================================================================================
using UnityEngine;
using System.Collections;

public abstract class UnitStats : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField]
    [Tooltip("The max health the character can possibly have")]
    protected int maxdHealth;
    [SerializeField]
    protected int currentHealth;
    [SerializeField]
    protected int maxMana;
    [SerializeField]
    protected int currentMana;
    [SerializeField]
    protected int armor;

    
    //======================================================================================================
    // Theoretical stat system 
    //======================================================================================================
    [SerializeField]
    protected int level;
    [Header("Attributes")]
    [SerializeField]
    protected int intelligence;
    [SerializeField]
    protected int strength;
    [SerializeField]
    protected int agility;

    public abstract void InitialStatSetup();
    public abstract void LevelUp();
    public abstract void ReceiveDamage(int damage);
    public abstract void RecieveHealing(int hpHealed);
    public abstract void LoseMana(int mana);
    public abstract void RecieveMana(int mana);

}
