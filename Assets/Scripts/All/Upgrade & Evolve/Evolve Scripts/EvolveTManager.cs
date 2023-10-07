using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectingMode
{
    Single,
    Multiple
}

public class EvolveTManager : MonoBehaviour
{
    public List<Card> teamList = new List<Card>();
    public List<Card> tempTeamList = new List<Card>();
    public static SelectingMode selectionMode;
}
