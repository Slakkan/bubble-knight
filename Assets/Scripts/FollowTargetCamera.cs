using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowTargetCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _distanceToTarget;
    [SerializeField]
    private float _angle = 45f;

    private Camera _camera;

    private Vector3 _targetToCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = GetComponent<Camera>();

        
    }

    // Update is called once per frame
    void Update()
    {
        _targetToCamera = Quaternion.AngleAxis(_angle, Vector3.right) * -Vector3.forward;
        _camera.transform.position = _target.position + _targetToCamera * _distanceToTarget;
        _camera.transform.LookAt(_target);
    }
}
