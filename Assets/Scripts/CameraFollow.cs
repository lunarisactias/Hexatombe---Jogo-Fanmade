using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        Follow();
    }

    public void Follow()
    {
        Vector3 newPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
