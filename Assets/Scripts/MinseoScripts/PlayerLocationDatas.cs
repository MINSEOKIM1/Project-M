using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerLocation", order = 2)]
public class PlayerLocationDatas : ScriptableObject
{
    public Vector3[] locations;
}
