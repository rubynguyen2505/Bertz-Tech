using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AKCS
{
    public enum SelectionMode{Single, Multiple}
    //for manage the team composition
    //from the selection until confirmation or cancel
    public class TeamManager : MonoBehaviour
    {
        public List<Cards> listTeam = new List<Cards>();
        public List<Cards> tempListTeam = new List<Cards>();
        public static SelectionMode selectionMode;
    }
}