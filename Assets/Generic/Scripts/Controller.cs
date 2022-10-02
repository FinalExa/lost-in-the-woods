using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [HideInInspector] public float actualHealth;

    public virtual void HealthAddValue(float value)
    {
        return;
    }
}
