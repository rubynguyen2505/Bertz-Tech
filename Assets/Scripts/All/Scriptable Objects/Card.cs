using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Roles
{
    Attacker, 
    Defender, 
    Healer, 
    Buffer, 
    Debuffer
}

public enum Type
{
    Red,
    Blue,
    Green,
    Yellow,
    Purple
}

[CreateAssetMenu(fileName = "New Card", menuName = "Character Card")]
public class Card : ScriptableObject
{
    public string charaName;
    public Sprite image;
    public Sprite charFull;
    public Sprite itemFrame;
    public Sprite stars;
    public GameObject character;
    public int maxLv = 20, lv = 1;
    public Roles role;
    public Type type;
    
    public bool unlocked;
    public bool inTeam;
    

    [Header("Base Stats")]
    public int hp;
    public int atk;
    public int def;
    
    

    [Header("Battle Stats")]
    public int _hp;
    public int _atk;
    public int _def;

}

