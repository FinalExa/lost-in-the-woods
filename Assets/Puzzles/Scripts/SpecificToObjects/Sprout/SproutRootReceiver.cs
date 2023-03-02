using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutRootReceiver : MonoBehaviour
{
    [SerializeField] private Vector3 sizeToCheck;
    [SerializeField] private string neededName;
    [SerializeField] private string[] bannedNames;

    public bool GetStatus()
    {
        bool canOperate = false;
        List<NamedInteractionExecutor> listOfNames = GetNamedInteractions();
        bool neededNameIsPresent = GetNeededName(listOfNames);
        bool bannedNamesArePresent = GetBannedNames(listOfNames);
        if (neededNameIsPresent && !bannedNamesArePresent) canOperate = true;
        return canOperate;
    }

    private List<NamedInteractionExecutor> GetNamedInteractions()
    {
        Collider[] collidersInReceiver = Physics.OverlapBox(this.transform.position, sizeToCheck);
        List<NamedInteractionExecutor> listOfNames = new List<NamedInteractionExecutor>();
        foreach (Collider collider in collidersInReceiver)
        {
            NamedInteractionExecutor namedInteraction = collider.gameObject.GetComponent<NamedInteractionExecutor>();
            if (namedInteraction != null) listOfNames.Add(namedInteraction);
        }
        return listOfNames;
    }

    private bool GetNeededName(List<NamedInteractionExecutor> listOfNames)
    {
        foreach (NamedInteractionExecutor namedInteraction in listOfNames)
        {
            if (namedInteraction.thisName == neededName) return true;
        }
        return false;
    }

    private bool GetBannedNames(List<NamedInteractionExecutor> listOfNames)
    {
        foreach (NamedInteractionExecutor namedInteraction in listOfNames)
        {
            for (int i = 0; i < bannedNames.Length; i++)
            {
                if (namedInteraction.thisName == bannedNames[i]) return true;
            }
        }
        return false;
    }
}
