using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Camera cam;
    private float _screenHeightMax;
    private ScrapManager _scrapManager;
    private float yPos;
    private float _speed = 2f;
    private bool _isShooting = false;
    [SerializeField] private GameObject _laser;

    void Start()
    {
        _scrapManager = GameObject.Find("Scrap Manager").GetComponent<ScrapManager>();
        cam = GameObject.Find("Player Camera").GetComponent<Camera>();
        // Set the value for the bottom of the screen
        _screenHeightMax = cam.orthographicSize;
        yPos = _screenHeightMax - 1;
    }

    void Update()
    {
        
        if (transform.position.y < yPos && _isShooting == false) {
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            StartCoroutine(StartShooting());
        }
        else if(_isShooting == false) {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Laser")) {
            Destroy(other.gameObject);
            _scrapManager.SpawnScrapMetal(transform.position);
            Destroy(this.gameObject);
        }
    }

    IEnumerator StartShooting() {
        _isShooting = true;
        while (_isShooting) {
            yield return new WaitForSeconds(1f);
            Vector3 position = transform.position;
            position.y -= 1f;
            Instantiate(_laser, position, Quaternion.identity);
        }
    }
}
