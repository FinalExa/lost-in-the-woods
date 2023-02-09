using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetOfInteractions", menuName = "ScriptableObjects/SetOfInteractions", order = 4)]
public class SetOfInteractions : ScriptableObject
{
    [System.Serializable]
    public struct AttackTypeInteraction
    {
        public WeaponAttack.WeaponAttackType attackType;
        public bool destroyAttackSourceObject;
        public Options options;
    }
    [System.Serializable]
    public struct NamedInteraction
    {
        public string name;
        public bool destroyNamedObjectOnInteraction;
        public Options options;
        public bool hasNamedInteractionExitOptions;
        public bool destroyNamedObjectOnInteractionExit;
        public Options exitNamedInteractionOptions;
    }
    [System.Serializable]
    public struct TimeAfterStartInteraction
    {
        public bool hasTimeAfterStartInteraction;
        public float timeAfterStart;
        public bool repeats;
        public Options options;
    }
    [System.Serializable]
    public struct LightInteraction
    {
        public bool hasLightInteraction;
        public bool normalRefreshes;
        public Options normalOptions;
        public bool calmRefreshes;
        public Options calmOptions;
        public bool berserkRefreshes;
        public Options berserkOptions;
    }
    [System.Serializable]
    public struct Options
    {
        [Header("Destruction")]
        public bool isDestroyed;
        [Header("Transformation into another object")]
        public bool isTransformed;
        public GameObject transformedRef;
        [Header("Activates/Deactivates other objects")]
        public bool canSetObjectActiveStatus;
        public bool objectActiveStatus;
        [Header("Spawns other object")]
        public bool canSpawnObject;
        public GameObject objectToSpawn;
        public Vector3 objectSpawnPositionOffset;
        [Header("Rotates object in ref")]
        public bool rotates;
        public float rotateValue;
        [Header("Moves by a certain amount of space this gameObject towards direction for a certain amount of time")]
        public bool isMoved;
        public float movementDistance;
        public float movementTime;
        [Header("For special functionalities, touch only if you script")]
        public bool sendsSignalToSelf;
        public bool hasSpecialCondition;
        [Header("UX Effects on Interaction")]
        public UXEffect uxOnInteraction;
        [HideInInspector] public bool uxOnInteractionInitialized;
    }
    public bool turnsOff;
    public AttackTypeInteraction[] attackTypeInteractions;
    public NamedInteraction[] namedInteractions;
    public bool onDeathEnabled;
    public Options onDeathInteraction;
    public LightInteraction lightInteraction;
    public TimeAfterStartInteraction timeAfterStartInteraction;
}
