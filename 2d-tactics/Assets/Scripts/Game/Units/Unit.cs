using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Game/Unit")]
public class Unit : ScriptableObject
{
    public int attackPower;
    public int speed;
    public int defenseStrength;
    public UnitType unitType; 

    private int turnPoints;

    public void SetTurnPoints(int turnPoints) {
        this.turnPoints = turnPoints;
    }
}

public enum UnitType
{
    Meele,
    Archer,
    Mage
}