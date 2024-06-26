using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 2f;
    public Vector3 direction = Vector3.left;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // ������� �����������, ����� ��� ������� �� ������� ������
    }
}