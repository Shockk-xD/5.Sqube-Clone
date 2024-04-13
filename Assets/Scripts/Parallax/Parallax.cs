using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField, Range(0, 0.5f)] private float _speed = 0.2f;

    private Material _material;
    private float _distance;

    private void Start() {
        _material = GetComponent<Renderer>().material;
    }

    private void Update() {
        _distance += Time.deltaTime * _speed;
        _material.SetTextureOffset("_MainTex", Vector2.right * _distance);
    }
}
