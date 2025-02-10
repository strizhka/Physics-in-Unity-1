using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    // �������� ������
    [SerializeField] private Vector3 _initialPosition; // ��������� ����������
    [SerializeField] private Vector3 _velocity;        // ��������
    [SerializeField] private Vector3 _acceleration;    // ���������
    [SerializeField] private float _time;              // ����� � ������ ��������

    private Vector3 _currentPosition; // ������� ����������
    private float _elapsedTime;       // ��������� �����

    private void Start()
    {
        // ������������� ��������� �������
        _currentPosition = _initialPosition;
        transform.position = _currentPosition;
        _elapsedTime = 0f;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_acceleration != Vector3.zero)
        {
            _currentPosition = LinearAcceleration(_initialPosition, _acceleration, _velocity, _elapsedTime);
        }
        else
        {
            _currentPosition = UniformMovement(_initialPosition, _velocity, _time);
        }

        transform.position = _currentPosition;

        // ��� ������: ����������� ����������� ���� � �������
        float distanceTravelled = Vector3.Distance(_initialPosition, _currentPosition);
        Debug.Log($"Time: {_elapsedTime:F2}s, Position: {_currentPosition}, Distance: {distanceTravelled:F2}");
    }

    private Vector3 LinearAcceleration(Vector3 startPosition, Vector3 acceleration, Vector3 velocity, float time)
    {
        Vector3 currentPosition = startPosition + velocity * time + 0.5f * acceleration * Mathf.Pow(time, 2);
        return currentPosition;
    }

    private Vector3 UniformMovement(Vector3 startPosition, Vector3 velocity, float time)
    {
        Vector3 currentPosition = startPosition + velocity * time;
        return currentPosition;
    }
}

