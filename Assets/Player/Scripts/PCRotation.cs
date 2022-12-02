using UnityEngine;

public class PCRotation : MonoBehaviour
{
    [SerializeField] private Rotation rotation;
    private PCReferences pcReferences;
    private Vector3 direction;
    private Vector3 lastInput;
    void Awake()
    {
        pcReferences = this.gameObject.GetComponentInParent<PCReferences>();
    }
    private void Update()
    {
        Rotation();
    }
    private void Start()
    {
        this.transform.eulerAngles = rotation.forward;
    }
    private void Rotation()
    {
        if ((lastInput == null || lastInput != pcReferences.inputs.MovementInput) && pcReferences.inputs.MovementInput != Vector3.zero)
        {
            lastInput = pcReferences.inputs.MovementInput;
            direction = pcReferences.inputs.MovementInput;
            SetRotation();
        }
    }
    private void SetRotation()
    {
        if (direction.z > 0 && this.transform.eulerAngles != rotation.left) this.transform.eulerAngles = rotation.left;
        else if (direction.z < 0 && this.transform.eulerAngles != rotation.right) this.transform.eulerAngles = rotation.right;
        if (direction.x > 0 && this.transform.eulerAngles != rotation.forward) this.transform.eulerAngles = rotation.forward;
        else if (direction.x < 0 && this.transform.eulerAngles != rotation.back) this.transform.eulerAngles = rotation.back;
    }
}

