using UnityEngine;

public class PlayerAimLimiter : MonoBehaviour
{
    private bool _mouseIsOnPlayer;

    private Camera _mainCamera;

    public LayerMask LayerMaskAimLimiter;

    private void Start()
    {
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMaskAimLimiter);

        if (hit)
        {
            _mouseIsOnPlayer = true;
            //Debug.Log("MOUSE IS ON PLAYER!");
        }
        else
        {
            _mouseIsOnPlayer = false;
            //Debug.Log("MOUSE IS NOT ON PLAYER!");
        }

    }

    public bool ReturnMouseIsOnPlayer()
    {
        return _mouseIsOnPlayer;
    }
}
