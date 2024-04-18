using System.Linq;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField, Range(0.025f, 0.15f)] private float _parallaxSpeed;

    private Transform _camera;
    private Vector3 _cameraStartPosition;
    private float _distance;

    private GameObject[] _backgrounds;
    private Material[] _materials;
    private float[] _backgroundsSpeed;

    private float _farthestBack;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private void Start()
    {
        _camera = Camera.main.transform;
        _cameraStartPosition = _camera.position;

        int backgroundsCount = transform.childCount;
        _backgrounds = new GameObject[backgroundsCount];
        _materials = new Material[backgroundsCount];
        _backgroundsSpeed = new float[backgroundsCount];

        for (int i = 0; i < backgroundsCount; i++)
        {
            _backgrounds[i] = transform.GetChild(i).gameObject;
            _materials[i] = _backgrounds[i].GetComponent<Renderer>().material;
        }
        
        BackgroundsSpeedCalculate(backgroundsCount);
    }

    private void BackgroundsSpeedCalculate(int backgroundsCount)
    {
        _farthestBack = _backgrounds.Max(b => b.transform.position.z - _camera.position.z);
        
        for (int i = 0; i < backgroundsCount; i++)
        {
            _backgroundsSpeed[i] = 1 - (_backgrounds[i].transform.position.z - _camera.position.z) / _farthestBack;
        }
    }

    private void LateUpdate()
    {
        _distance = _camera.position.x - _cameraStartPosition.x;
        transform.position = new Vector3(_camera.position.x, transform.position.y, transform.position.z);
        
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            float speed = _backgroundsSpeed[i] * _parallaxSpeed;
            _materials[i].SetTextureOffset(MainTex, new Vector2(_distance, 0) * speed);
        }
    }
}
