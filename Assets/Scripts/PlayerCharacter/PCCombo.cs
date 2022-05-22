using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCCombo : MonoBehaviour
{
    private PCReferences pcReferences;
    private int currentComboProgress;
    private int comboTimer;

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void Combo()
    {

    }
}
