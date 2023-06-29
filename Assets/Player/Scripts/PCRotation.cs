using UnityEngine;

public class PCRotation : MonoBehaviour
{
    [SerializeField] private Rotation rotation;
    private PCReferences pcReferences;
    private Vector3 direction;
    private Vector3 lastInput;
    [SerializeField] private bool isInGraphicsMode;
    [SerializeField] private Sprite lookingUpSprite;
    [SerializeField] private Sprite lookingDownSprite;
    [SerializeField] private Sprite lookingLeftSprite;
    [SerializeField] private Sprite lookingRightSprite;
    [SerializeField] private SpriteRenderer spriteRef;
    private Quaternion spriteStartRotation;
    [HideInInspector] public bool rotationLocked;

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
            spriteRef.sprite = lookingUpSprite;
        }
    }
    private void Update()
    {
        if (!pcReferences.pcCombo.GetIfIsAttacking() && !rotationLocked) Rotation();
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
        if (direction.z > 0 && this.transform.eulerAngles != rotation.left) SetRotationSide(rotation.left, lookingRightSprite);
        else if (direction.z < 0 && this.transform.eulerAngles != rotation.right) SetRotationSide(rotation.right, lookingLeftSprite);
        if (direction.x > 0 && this.transform.eulerAngles != rotation.forward) SetRotationSide(rotation.forward, lookingUpSprite);
        else if (direction.x < 0 && this.transform.eulerAngles != rotation.back) SetRotationSide(rotation.back, lookingDownSprite);
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

