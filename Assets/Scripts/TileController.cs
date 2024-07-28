
using UnityEngine;



public class TileController : MonoBehaviour
{
    private Grid _grid;
    private Camera _camera;
    private Renderer[] _renderers ;
    private Color[] _startColors ;
    private bool isPositioning = true;
    private Collider[] _colliders;


    private void Awake()
    {
        _camera = Camera.main;
        _grid = FindObjectOfType<Grid>();
        _renderers = GetComponentsInChildren<Renderer>();
        _startColors = new Color[_renderers.Length];
        for (var i = 0; i < _startColors.Length; i++)
        {
            _startColors[i] = _renderers[i].material.color;
        }
        _colliders = GetComponentsInChildren<Collider>();
        for (var i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].enabled = false;
        }
    }
    private void Update()
    {
        if (!isPositioning)
        {
            return;
        }


        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo) )
        {
            var cellPosition = _grid.WorldToCell(hitInfo.point);
            var cellCenter = _grid.GetCellCenterWorld(cellPosition);
            transform.position = new Vector3(cellCenter.x, 0.1f, cellCenter.z);
            if (hitInfo.transform.CompareTag(GlobalConstants.GROUND_TAG))
            {
                for (var i = 0; i < _renderers.Length; i++)
                {
                    _renderers[i].material.color = Color.green;
                }

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    for (var i = 0; i < _colliders.Length; i++)
                    {
                        _colliders[i].enabled = true;
                    }
                    isPositioning = false;
                    var settingPoint = new Vector3(cellCenter.x, 0, cellCenter.z);
                    transform.position = settingPoint;
                    for (var i = 0; i < _startColors.Length; i++)
                    {
                        _renderers[i].material.color = _startColors[i];
                    }
                }
            }
            if (!hitInfo.transform.CompareTag(GlobalConstants.GROUND_TAG))
            {
                cellPosition = _grid.WorldToCell(hitInfo.point);
                cellCenter = _grid.GetCellCenterWorld(cellPosition);
                transform.position = new Vector3(cellCenter.x, 0.1f, cellCenter.z);
                for (var i = 0; i < _renderers.Length; i++)
                {
                    _renderers[i].material.color = Color.red;
                }
                // for (var i = 0; i < _renderers.Length; i++)
                // {
                //     Debug.Log(_renderers[i].material.color);
                // }
            }
        }

    }
}