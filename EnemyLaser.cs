using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour {
    private Camera cam;
    private float _speed = 8f;
    private float _screenHeightMin;

    private void Start() {
        cam = GameObject.Find("Player Camera").GetComponent<Camera>();
        // Set the value for the bottom of the screen
        float _screenHeightMax = cam.orthographicSize;
        _screenHeightMin = -_screenHeightMax;
    }

    void Update() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < _screenHeightMin) {
            Destroy(this.gameObject);
        }
    }
}
