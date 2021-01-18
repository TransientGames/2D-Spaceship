using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public Camera cam;
    public List<GameObject> _powerUps = new List<GameObject>();
    private float _screenWidthMin;
    private float _screenWidthMax;
    private float _screenHeightMax;

    private void Awake() {
        // Get the orthographic camera's width & height in the viewport space
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;

        // Offset the screen width boundaries so asteroids don't spawn off screen
        halfWidth -= 0.5f;
        // Set the min and max width of the viewport
        _screenWidthMin = -halfWidth;
        _screenWidthMax = halfWidth;
        // Offset the verticalMax by 1 so objects don't spawn halfway on screen
        _screenHeightMax = halfHeight + 1f;
    }

    public void SpawnSinglePowerUp(float speed) {
        float yPos = _screenHeightMax;
        float xPos = Random.Range(_screenWidthMin, _screenWidthMax);
        Vector3 position = new Vector3(xPos, yPos, 1);

        int randOBJ = Random.Range(0, _powerUps.Count);
        GameObject obj = Instantiate(_powerUps[randOBJ]);
        PowerUp powerUp = obj.GetComponent<PowerUp>();

        obj.transform.SetParent(this.transform);
        obj.gameObject.layer = 9;
        powerUp.Initialize(position, speed);
    }
}
