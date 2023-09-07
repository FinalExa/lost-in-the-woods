using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveIntValuesForSaveSystem
{
    public int ValueToSave { get; set; }

    public void SetValue();
}
