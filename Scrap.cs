using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{

    private Rigidbody _rigidbody;
    private Camera cam;
    private float _screenHeightMin;
    private float _rotationSpeed = 30f;
    private Vector3 _explosionPosition;

    private void Awake() {
        _rigidbody = this.GetComponent<Rigidbody>();
        cam = GameObject.Find("Player Camera").GetComponent<Camera>();
        // Set the value for the bottom of the screen
        float halfHeight = cam.orthographicSize;
        _screenHeightMin = -halfHeight;
    }

    void Start()
    {
        FindRandomExplosionPosition();
        int randRotation = Random.Range(0, 2);
        if (randRotation == 1) {
            _rotationSpeed = -_rotationSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * _rotationSpeed * Time.deltaTime);
        if (transform.position.y < _screenHeightMin) {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate() {
        _rigidbody.AddExplosionForce(3f, _explosionPosition, 3f);
    }

    private void FindRandomExplosionPosition() {
        float randX = Random.Range(-0.2f, 0.2f);
        float randY = Random.Range(-0.2f, 0.2f);
        _explosionPosition.x = transform.position.x + randX;
        _explosionPosition.y = transform.position.y + randY;
        _explosionPosition.z = 1f;
    }
}
