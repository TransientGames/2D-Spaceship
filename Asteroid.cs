using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Asteroid : MonoBehaviour
{

    private float _speed = 3f;
    private Camera cam;
    private float _screenHeightMin;
    private float _rotationSpeed = 30f;
    private ScrapManager _scrapManager;

    private void Start() {
        _scrapManager = GameObject.Find("Scrap Manager").GetComponent<ScrapManager>();
        cam = GameObject.Find("Player Camera").GetComponent<Camera>();
        // Set the value for the bottom of the screen
        float halfHeight = cam.orthographicSize;
        _screenHeightMin = -halfHeight;
    }

    public void Initialize(Vector3 position, float speed) {
        transform.position = position;
        _speed = speed;
        int randRotation = Random.Range(0,2);
        if (randRotation == 1) {
            _rotationSpeed = -_rotationSpeed;
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.down * _rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < _screenHeightMin) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case "Laser":
                _scrapManager.SpawnRocks(transform.position);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
                break;
            case "Player":
                _scrapManager.SpawnRocks(transform.position);
                Destroy(this.gameObject);
                break;
            case "Shield":
                _scrapManager.SpawnRocks(transform.position);
                Destroy(this.gameObject);
                break;
        }
    }
}
