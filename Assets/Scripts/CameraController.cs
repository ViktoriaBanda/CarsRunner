using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Vector3 _offset;
    private Transform _playerTransform;

    private void LateUpdate()
    {
        transform.position = new Vector3(_playerTransform.position.x + _offset.x, _offset.y, _offset.z);
    }

    public void Initialize(PlayerController playerController)
    {
        _playerTransform = playerController.Player.transform;
    }
}
