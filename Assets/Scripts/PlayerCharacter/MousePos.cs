using System.Linq;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    public RaycastHit hit;
    public Ray ray;
    private Camera mainCamera;
    private int layerMask = 1 << 6;

    public Vector3 position;
    public Vector3 Position
    {
        get { return position; }
    }

    public void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
    public void Update()
    {
        if (mainCamera)
        {
            MouseRaycast();
        }

    }
    private void MouseRaycast()
    {
        Ray screenRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(screenRay, out hit, layerMask))
        {
            position = hit.point;
        }
    }
}
