using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public bool LeftClickInput { get; private set; }
    public bool RightClickInput { get; private set; }
    public Vector3 MovementInput { get; private set; }
    [SerializeField] private KeyCode dodgeKey;
    public bool DodgeInput { get; private set; }
    [SerializeField] private KeyCode lanternKey;
    public bool LanternInput { get; private set; }
    private void Update()
    {
        GetInputs();
    }
    private void GetInputs()
    {
        GetLeftClickInput();
        GetRightClickInput();
        GetMovementInput();
        GetDodgeInput();
        GetLanternInput();
    }
    private void GetLeftClickInput()
    {
        if (Input.GetButton("Fire1")) LeftClickInput = true;
        else LeftClickInput = false;
    }
    private void GetRightClickInput()
    {
        if (Input.GetButton("Fire2")) RightClickInput = true;
        else RightClickInput = false;
    }
    private void GetMovementInput()
    {
        float frontInput = Input.GetAxisRaw("Horizontal");
        float sideInput = Input.GetAxisRaw("Vertical");
        MovementInput = new Vector3(sideInput, 0, frontInput).normalized;
    }
    private void GetDodgeInput()
    {
        if (Input.GetKeyDown(dodgeKey)) DodgeInput = true;
        else DodgeInput = false;
    }
    private void GetLanternInput()
    {
        if (Input.GetKeyDown(lanternKey)) LanternInput = true;
        else LanternInput = false;
    }
    public void StopAllInputs()
    {
        LeftClickInput = false;
        RightClickInput = false;
        MovementInput = Vector3.zero.normalized;
    }
}
