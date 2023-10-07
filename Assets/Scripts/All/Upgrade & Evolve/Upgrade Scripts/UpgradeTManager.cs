using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectMode
{
    Single,
    Multiple
}

public class UpgradeTManager : MonoBehaviour
{
    public List<Card> teamList = new List<Card>();
    public List<Card> tempTeamList = new List<Card>();
    public static SelectMode selectionMode;
}
