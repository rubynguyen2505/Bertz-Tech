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

public enum Rarity
{
    Legendary,
    Epic,
    Rare
}
[CreateAssetMenu(fileName = "New Card", menuName = "Character Card")]
public class Card : ScriptableObject
{
    public string charaName;
    public Sprite image;
    public Sprite charFull;
    public Sprite itemFrame;
    public Sprite stars;
    public Sprite starsAcross;
    public Sprite charaFrame;
    public Sprite charaRole;
    public GameObject character;
    public int maxLv = 20, lv = 1;
    public Roles role;
    public Type type;
    public Rarity rarity;
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

    //name: str
    //role: str
    //type: str
    //stars: int
    //current lv: int
    //max lv: int
    //base hp: int
    //base atk: int
    //base def: int
    //updated hp: int
    //updated atk: int
    //updated def: int
    //unlocked: bool
    //in team: bool
}

