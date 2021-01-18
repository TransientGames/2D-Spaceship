using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 3f;
    private Camera cam;
    private float _screenHeightMin;
    private float _rotationSpeed = 30f;

    private void Start() {
        cam = GameObject.Find("Player Camera").GetComponent<Camera>();
        // Set the value for the bottom of the screen
        float halfHeight = cam.orthographicSize;
        _screenHeightMin = -halfHeight;
    }

    public void Initialize(Vector3 position, float speed) {
        transform.position = position;
        _speed = speed;
        int randRotation = Random.Range(0, 2);
        if (randRotation == 1) {
            _rotationSpeed = -_rotationSpeed;
        }
    }

    void Update() {
        transform.Rotate(Vector3.down * _rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < _screenHeightMin) {
            Destroy(this.gameObject);
        }
    }
}
