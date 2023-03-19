using UnityEngine;

public class PCRotation : MonoBehaviour
{
    [SerializeField] private Rotation rotation;
    private PCReferences pcReferences;
    private Vector3 direction;
    private Vector3 lastInput;
    [SerializeField] private bool isInGraphicsMode;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite faceRightSprite;
    [SerializeField] private Sprite faceLeftSprite;
    [SerializeField] private SpriteRenderer spriteRef;
    private Quaternion spriteStartRotation;

    void Awake()
    {
        pcReferences = this.gameObject.GetComponentInParent<PCReferences>();
    }
    private void Start()
    {
        this.transform.eulerAngles = rotation.forward;
        if (isInGraphicsMode)
        {
            spriteStartRotation = spriteRef.gameObject.transform.rotation;
            spriteRef.sprite = frontSprite;
        }
    }
    private void Update()
    {
        if (!pcReferences.pcCombo.GetIfIsAttacking()) Rotation();
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
        if (direction.z > 0 && this.transform.eulerAngles != rotation.left) SetRotationSide(rotation.left, faceLeftSprite);
        else if (direction.z < 0 && this.transform.eulerAngles != rotation.right) SetRotationSide(rotation.right, faceRightSprite);
        if (direction.x > 0 && this.transform.eulerAngles != rotation.forward) SetRotationSide(rotation.forward, frontSprite);
        else if (direction.x < 0 && this.transform.eulerAngles != rotation.back) SetRotationSide(rotation.back, backSprite);
    }

    private void SetRotationSide(Vector3 rotation, Sprite spriteToApply)
    {
        this.transform.eulerAngles = rotation;
        if (isInGraphicsMode)
        {
            spriteRef.gameObject.transform.rotation = spriteStartRotation;
            spriteRef.sprite = spriteToApply;
        }
    }
}

