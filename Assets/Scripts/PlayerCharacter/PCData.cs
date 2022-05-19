using UnityEngine;

[CreateAssetMenu(fileName = "PCData", menuName = "ScriptableObjects/PCData", order = 1)]
public class PCData : ScriptableObject
{
    [Header("Movement section")]
    public float defaultMovementSpeed;
    [HideInInspector] public float currentMovementSpeed;
}
