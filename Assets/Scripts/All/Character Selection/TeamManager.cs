using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SelectionMode
{
    Single, 
    Multiple
}

//for manage the team composition
//from the selection until confirmation or cancel
public class TeamManager : MonoBehaviour
{
    public List<Card> teamList = new List<Card>();
    public List<Card> tempTeamList = new List<Card>();
    public static SelectionMode selectionMode;
}
