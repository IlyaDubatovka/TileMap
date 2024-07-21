

using UnityEngine;


public class TileController : MonoBehaviour
{
    private Grid _grid;
    private Camera _camera;
    private Renderer[] _renderers=new Renderer[10];


    private void Awake()
    {
        _camera = Camera.main;
        _grid = FindObjectOfType<Grid>();
        _renderers = GetComponentsInChildren<Renderer>();
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray,out hitInfo)&&hitInfo.transform.CompareTag(GlobalConstants.GROUND_TAG))
        {
            var cellPosition = _grid.WorldToCell(hitInfo.point);
            var cellCenter = _grid.GetCellCenterWorld(cellPosition);
            transform.position=new Vector3(cellCenter.x,0.1f,cellCenter.z);
            for (var i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.color=Color.green;
            }
        }
        if (Physics.Raycast(ray,out hitInfo)&&hitInfo.transform.CompareTag(GlobalConstants.TILE_TAG))
        {
            var cellPosition = _grid.WorldToCell(hitInfo.point);
            var cellCenter = _grid.GetCellCenterWorld(cellPosition);
            transform.position=new Vector3(cellCenter.x,0.1f,cellCenter.z);
            for (var i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.color=Color.red;
            }
        }
    }
}
