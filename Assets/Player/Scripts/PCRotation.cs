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
    private Vector3 MovementDirection(Camera camera, Inputs inputs)
    {
        Vector3 forward = new Vector3(-camera.transform.forward.x, 0f, -camera.transform.forward.z).normalized;
        Vector3 right = new Vector3(camera.transform.right.x, 0f, camera.transform.right.z).normalized;
        return (inputs.MovementInput.x * forward) + (inputs.MovementInput.z * right);
    }
    private void Rotation()
    {
        if ((lastInput == null || lastInput != pcReferences.inputs.MovementInput) && pcReferences.inputs.MovementInput != Vector3.zero)
        {
            lastInput = pcReferences.inputs.MovementInput;
            direction = MovementDirection(pcReferences.cam, pcReferences.inputs);
            SetRotation();
        }
    }
    private void SetRotation()
    {
        if (direction.x > 0 && this.transform.eulerAngles != rotation.left) this.transform.eulerAngles = rotation.left;
        else if (direction.x < 0 && this.transform.eulerAngles != rotation.right) this.transform.eulerAngles = rotation.right;
        if (direction.z > 0 && this.transform.eulerAngles != rotation.back) this.transform.eulerAngles = rotation.back;
        else if (direction.z < 0 && this.transform.eulerAngles != rotation.forward) this.transform.eulerAngles = rotation.forward;
    }
}

