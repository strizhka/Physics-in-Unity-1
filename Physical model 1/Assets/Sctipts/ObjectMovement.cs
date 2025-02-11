using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject outVelocity;
    [SerializeField] GameObject outPosition;
    [SerializeField] GameObject inAcceleration;
    [SerializeField] GameObject inPosition;
    [SerializeField] private TMP_InputField velocityXInput;
    [SerializeField] private TMP_InputField velocityYInput;
    [SerializeField] private TMP_InputField velocityZInput;
    [SerializeField] private TMP_InputField accelerationXInput;
    [SerializeField] private TMP_InputField accelerationYInput;
    [SerializeField] private TMP_InputField accelerationZInput;
    [SerializeField] private TMP_InputField positionXInput;
    [SerializeField] private TMP_InputField positionYInput;
    [SerializeField] private TMP_InputField positionZInput;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI positionXText;
    [SerializeField] private TextMeshProUGUI positionYText;
    [SerializeField] private TextMeshProUGUI positionZText;
    [SerializeField] private TextMeshProUGUI velocityXText;
    [SerializeField] private TextMeshProUGUI velocityYText;
    [SerializeField] private TextMeshProUGUI velocityZText;

    private Vector3 _initialPosition;
    private Vector3 _velocity;
    private Vector3 _acceleration;
    private Vector3 _currentPosition;
    private Vector3 _currentVelocity;
    private float _elapsedTime;
    private bool _isMoving;

    private void Start()
    {
        ResetPosition();
    }

    private void Update()
    {
        if (_isMoving)
        {
            transform.rotation = Quaternion.LookRotation(_velocity);

            _elapsedTime += Time.deltaTime;

            if (_acceleration != Vector3.zero)
            {
                _currentPosition = LinearAcceleration(_initialPosition, _acceleration, _velocity, _elapsedTime);
            }
            else
            {
                _currentPosition = UniformMovement(_initialPosition, _velocity, _elapsedTime);
            }

            transform.position = _currentPosition;

            UpdateUI();
        }
    }

    private Vector3 LinearAcceleration(Vector3 startPosition, Vector3 acceleration, Vector3 velocity, float time)
    {
        return startPosition + velocity * time + 0.5f * acceleration * Mathf.Pow(time, 2);
    }

    private Vector3 UniformMovement(Vector3 startPosition, Vector3 velocity, float time)
    {
        return startPosition + velocity * time;
    }

    private void ResetPosition()
    {
        _elapsedTime = 0f;
        _currentPosition = _initialPosition;
        transform.position = _currentPosition;

        UpdateUI();
    }

    private void UpdateUI()
    {
        timeText.text = $"{_elapsedTime:F2} ñ";
        float distanceTravelled = Vector3.Distance(_initialPosition, _currentPosition);
        distanceText.text = $"{distanceTravelled:F2}";

        if (outPosition.activeInHierarchy)
        {
            positionXText.text = $"{_currentPosition.x:F0}";
            positionYText.text = $"{_currentPosition.y:F0}";
            positionZText.text = $"{_currentPosition.z:F0}";
        }

        if (outVelocity.activeInHierarchy)
        {
            _currentVelocity = _velocity + _acceleration * _elapsedTime;
            velocityXText.text = $"{_currentVelocity.x:F0}";
            velocityYText.text = $"{_currentVelocity.y:F0}";
            velocityZText.text = $"{_currentVelocity.z:F0}";
        }
    }

    public void StartMovement()
    {
        _isMoving = true;
        startButton.SetActive(false);
        pauseButton.SetActive(true);

        if (inPosition.activeInHierarchy)
        {
            _initialPosition = new Vector3(
                ParseInput(positionXInput.text),
                ParseInput(positionYInput.text),
                ParseInput(positionZInput.text)
            );
        }

        _velocity = new Vector3(
            ParseInput(velocityXInput.text),
            ParseInput(velocityYInput.text),
            ParseInput(velocityZInput.text)
        );

        if (inAcceleration.activeInHierarchy)
        {
            _acceleration = new Vector3(
                ParseInput(accelerationXInput.text),
                ParseInput(accelerationYInput.text),
                ParseInput(accelerationZInput.text)
            );
        }
    }

    public void StopMovement()
    {
        _isMoving = false;
        startButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResetMovement()
    {
        _isMoving = false;
        startButton.SetActive(true);
        pauseButton.SetActive(false);
        _initialPosition = Vector3.zero;
        positionYInput.text = "";
        positionXInput.text = "";
        positionZInput.text = "";
        velocityXInput.text = "";
        velocityYInput.text = "";
        velocityZInput.text = "";
        accelerationXInput.text = "";
        accelerationYInput.text = "";
        accelerationZInput.text = "";

        ResetPosition();
    }

    public void SetTask1()
    {
        ResetPosition();
        ResetMovement();

        inPosition.SetActive(false);
        inAcceleration.SetActive(false);
        outPosition.SetActive(false);
        outVelocity.SetActive(false);

        Debug.Log("Task 1: Uniform movement initialized.");
    }


    public void SetTask2()
    {
        ResetPosition();
        ResetMovement();

        inPosition.SetActive(true);
        inAcceleration.SetActive(false);
        outPosition.SetActive(true);
        outVelocity.SetActive(false);

        Debug.Log("Task 2: Uniform movement with initial position initialized.");
    }

    public void SetTask3()
    {
        ResetPosition();
        ResetMovement();

        inPosition.SetActive(true);
        inAcceleration.SetActive(true);
        outPosition.SetActive(true);
        outVelocity.SetActive(true);

        Debug.Log("Task 3: Accelerated movement initialized.");
    }

    private float ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return 0f;
        }

        if (float.TryParse(input, out float result))
        {
            return result;
        }

        Debug.LogError($"Invalid input: {input}");
        return 0f;
    }
}
