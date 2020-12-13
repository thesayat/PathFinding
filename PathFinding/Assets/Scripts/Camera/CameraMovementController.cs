using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _cameraStartPosition;
    private Vector3 _dragOrigin;
    private float _dragSpeed = 8f;
    private float _cameraDefaultPosZ;

    void Awake(){
        _camera = GetComponent<Camera>();
        _cameraDefaultPosZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _cameraStartPosition = transform.position;
            _dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        MoveCamera(_dragOrigin - Input.mousePosition);
    }
    private void MoveCamera(Vector3 movementVec)
    {
        Vector3 newPosition = _cameraStartPosition + (_camera.ScreenToViewportPoint(movementVec) * _dragSpeed);
        newPosition.z = _cameraDefaultPosZ;
        transform.position = newPosition;
    }
}
