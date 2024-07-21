

using UnityEngine;


public class TileController : MonoBehaviour
{
    private Camera _camera;


    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray,out hitInfo)&&hitInfo.transform.CompareTag(GlobalConstants.GROUND_TAG))
        {
            transform.position = new Vector3(hitInfo.point.x, 0.2f, hitInfo.point.z);
            //Debug.Log($"x:{hitInfo.point.x} y:{hitInfo.point.y} z:{hitInfo.point.z}");
        }
    }
}
