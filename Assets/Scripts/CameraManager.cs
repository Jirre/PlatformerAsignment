using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject _FocusTarget;
    
    [Header("Screen Transition Settings")]
    [SerializeField] private float _LerpDuration;
    [SerializeField] private float _CameraWidth;
    [SerializeField] private int _MinCamPosition;
    [SerializeField] private int _MaxCamPosition;

    private float _lerpTimer;
    private float _targetPosition, _startPosition;

    private StateMachine _stateMachine;

    private void Awake() =>
        InitStates();

    private void Update()
    {
        if (_stateMachine != null)
            _stateMachine.Update();
    }

    private void InitStates()
    {
        _stateMachine = new StateMachine();
        _stateMachine.Add("InitState", InitState);
        _stateMachine.Add("GameplayState", GameplayState);
        _stateMachine.Add("TransitionState", TransitionState);

        _stateMachine.Goto("InitState");
    }

    private void InitState()
    {
        _startPosition = Mathf.Round(_FocusTarget.transform.position.x / _CameraWidth); ;
        _targetPosition = _startPosition;
        transform.position = new Vector3(_CameraWidth * _targetPosition, transform.position.y, transform.position.z);
        _stateMachine.Goto("GameplayState");
    }
    private void GameplayState()
    {
        if (_FocusTarget == null)
        {
            Debug.LogError("CameraManager: Focus Target not set");
            return;
        }
        if (Mathf.Round(_FocusTarget.transform.position.x / _CameraWidth) != _targetPosition)
        {
            _startPosition = _targetPosition;
            _targetPosition = Mathf.Round(_FocusTarget.transform.position.x / _CameraWidth);
            _lerpTimer = _LerpDuration;
            _stateMachine.Goto("TransitionState");

            PlayerManager[] players = FindObjectsOfType<PlayerManager>();
            if ((players?.Length ?? 0) > 0) foreach (PlayerManager p in players) p.DeactivatePlayer();
        }
    }
    private void TransitionState()
    {
        if (Mathf.Max(0, _lerpTimer -= Time.deltaTime) > 0)
            transform.position = new Vector3(_CameraWidth * Mathf.Lerp(_targetPosition, _startPosition, _lerpTimer / _LerpDuration), transform.position.y, transform.position.z);
        else
        {
            if (_targetPosition < _MinCamPosition)
            {
                _startPosition = _MaxCamPosition;
                _targetPosition = _MaxCamPosition;
                _FocusTarget.transform.position = new Vector3(_FocusTarget.transform.position.x + (_CameraWidth * (Mathf.Abs(_MinCamPosition) + Mathf.Abs(_MaxCamPosition) + 1)), _FocusTarget.transform.position.y, _FocusTarget.transform.position.z);
            }
            else if (_targetPosition > _MaxCamPosition)
            {
                _startPosition = _MinCamPosition;
                _targetPosition = _MinCamPosition;
                _FocusTarget.transform.position = new Vector3(_FocusTarget.transform.position.x - (_CameraWidth * (Mathf.Abs(_MinCamPosition) + Mathf.Abs(_MaxCamPosition) + 1)), _FocusTarget.transform.position.y, _FocusTarget.transform.position.z);
            }
            transform.position = new Vector3(_CameraWidth * _targetPosition, transform.position.y, transform.position.z);
            _stateMachine.Goto("GameplayState");

            PlayerManager[] players = FindObjectsOfType<PlayerManager>();
            if ((players?.Length ?? 0) > 0) foreach (PlayerManager p in players) p.ReactivatePlayer();
        }
    }
}
