using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutRootReceiver : MonoBehaviour
{
    private Vector3 sizeToCheck;
    private bool unableToWork;
    [SerializeField] private string neededName;
    [SerializeField] private string[] bannedNames;

    private void Start()
    {
        BoxCollider boxCollider = this.gameObject.GetComponent<BoxCollider>();
        if (boxCollider != null) sizeToCheck = Vector3.Scale(boxCollider.size, this.gameObject.transform.localScale);
        else unableToWork = true;
    }

    public bool GetStatus()
    {
        if (!unableToWork)
        {
            bool canOperate = false;
            List<NamedInteractionExecutor> listOfNames = GetNamedInteractions();
            if (listOfNames.Count < 1) return false;
            bool neededNameIsPresent = GetNeededName(listOfNames);
            bool bannedNamesArePresent = GetBannedNames(listOfNames);
            if (neededNameIsPresent && !bannedNamesArePresent) canOperate = true;
            return canOperate;
        }
        else
        {
            Debug.LogError("Error on " + this.gameObject.name + ": cannot find Box Collider!");
            return false;
        }
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
        if (neededName == string.Empty) return true;
        foreach (NamedInteractionExecutor namedInteraction in listOfNames)
        {
            if (namedInteraction.thisName == neededName) return true;
        }
        return false;
    }

    private bool GetBannedNames(List<NamedInteractionExecutor> listOfNames)
    {
        if (bannedNames.Length < 1) return false;
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
