using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGateType
{
    None,
    Yellow,
    Green,
    Blue,
    Red
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager GetInstance() =>
        ((_instance) ?? //Find Stored Instance
        (_instance = FindObjectOfType<GameManager>())) ?? //Find Existing Instance in Scene
        (_instance = new GameObject("GameManager").AddComponent<GameManager>()); //Create new Instance in Scene

    [SerializeField] private EGateType _currentGateState = EGateType.None;

    public static EGateType GetGateState() => GetInstance()._currentGateState;
    public static void SetGateState(EGateType pGateType) => GetInstance()._currentGateState = pGateType;

    private Vector2 _spawnPoint;
    public static void GotoSpawnPoint(GameObject pObject)
    {
        if(GetInstance()._spawnPoint == null)
        {
            Debug.LogError("GameManager: Spawnpoint not set");
            return;
        }
        pObject.transform.position = GetInstance()._spawnPoint;
    }
    public static void SetSpawnPoint(Transform pTransform) =>
        GetInstance()._spawnPoint = pTransform.position;
    public static void SetSpawnPoint(Vector2 pPosition) =>
        GetInstance()._spawnPoint = pPosition;

    [SerializeField] private Vector2 _SHPositionIntensity;
    [SerializeField] private float _SHAngleIntensity;
    [Tooltip("Defines the percentile intensity of the screenshake in the last second")]
    [SerializeField] private AnimationCurve _IntensityCurve;
    private float _screenShakeTimer;
    public static void SetScreenShake(float pTime) => GetInstance()._screenShakeTimer = pTime;

    private void Update()
    {
        if (_screenShakeTimer <= 0)
        {
            Camera.main.transform.localPosition = Vector2.zero;
            Camera.main.transform.localEulerAngles = Vector3.zero;
            return;
        }
        _screenShakeTimer -= Time.deltaTime;
        float mult = _IntensityCurve.Evaluate(_screenShakeTimer);

        Camera.main.transform.localPosition = _SHPositionIntensity * new Vector2((Random.value - .5f) * mult, (Random.value - .5f) * mult);
        Camera.main.transform.localEulerAngles = new Vector3(0, 0, _SHAngleIntensity * ((Random.value - .5f) * mult));
    }
}
