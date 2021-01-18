using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Camera cam;
    private float _speed = 15f;
    private float _screenHeightMax;

    private void Start() {
        cam = GameObject.Find("Player Camera").GetComponent<Camera>();
        // Set the value for the bottom of the screen
        _screenHeightMax = cam.orthographicSize;
    }

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y > _screenHeightMax) {
            Destroy(this.gameObject);
        }
    }
}
