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
    protected int maxHealth;
    [SerializeField]
    [Tooltip("The character's current health")]
    protected int currentHealth;
    [SerializeField]
    protected int armor;
    
    //======================================================================================================
    // Theoretical stat system 
    //======================================================================================================
    public abstract void ReceiveDamage(int damage);
    public abstract void RecieveHealing(int hpHealed);
}
