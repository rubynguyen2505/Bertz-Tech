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
    public int atk;
    public int hp;
    public int def;
    public Roles role;
    public Type type;
    public GameObject character;
    public bool unlocked;
    public bool inTeam;
}
