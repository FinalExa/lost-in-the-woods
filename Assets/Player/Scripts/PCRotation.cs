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
    [SerializeField] private Sprite sideSprite;
    [SerializeField] private SpriteRenderer spriteRef;
    private Quaternion spriteStartRotation;

    void Awake()
    {
        pcReferences = this.gameObject.GetComponentInParent<PCReferences>();
    }
    private void Start()
    {
        this.transform.eulerAngles = rotation.forward;
        if (isInGraphicsMode) spriteStartRotation = spriteRef.gameObject.transform.rotation;
    }
    private void Update()
    {
        Rotation();
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
        if (direction.z > 0 && this.transform.eulerAngles != rotation.left) SetRotationSide(rotation.left, sideSprite, false);
        else if (direction.z < 0 && this.transform.eulerAngles != rotation.right) SetRotationSide(rotation.right, sideSprite, true);
        if (direction.x > 0 && this.transform.eulerAngles != rotation.forward) SetRotationSide(rotation.forward, frontSprite, false);
        else if (direction.x < 0 && this.transform.eulerAngles != rotation.back) SetRotationSide(rotation.back, backSprite, false);
    }

    private void SetRotationSide(Vector3 rotation, Sprite spriteToApply, bool isFlipped)
    {
        this.transform.eulerAngles = rotation;
        if (isInGraphicsMode)
        {
            spriteRef.gameObject.transform.rotation = spriteStartRotation;
            spriteRef.sprite = spriteToApply;
            spriteRef.flipX = isFlipped;
        }
    }
}

