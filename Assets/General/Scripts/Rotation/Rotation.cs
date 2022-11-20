using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rotation", menuName = "ScriptableObjects/Rotation", order = 2)]
public class Rotation : ScriptableObject
{
    public Vector3 forward;
    public Vector3 back;
    public Vector3 right;
    public Vector3 left;
}
