using System.Linq;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    public RaycastHit hit;
    public Ray ray;
    private Camera mainCamera;

    public Vector3 VectorPointToShoot;

    public Vector3 reticlePosition;
    public Vector3 ReticlePosition
    {
        get { return reticlePosition; }
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

        if (Physics.Raycast(screenRay, out hit))
        {
            reticlePosition = hit.point;
        }
    }
}
