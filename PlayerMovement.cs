using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Camera cam;
    private Vector3 touchOrigin;
    private float _playerY = -2.8f;
    private float _screenWidthMin;
    private float _screenWidthMax;

    private void Awake() {
        // Get the orthographic camera's width & height in the viewport space
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;

        // Offset the screen width boundaries so players can't go off screen
        halfWidth -= 0.5f;
        // Set the min and max width of the viewport
        _screenWidthMin = -halfWidth;
        _screenWidthMax = halfWidth;
    }

    void Update() {
        Movement();
    }

    void Movement() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                touchOrigin = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 1));
            }
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
                //Vector3 touchPosition = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 1));
                //Vector3 viewportTouch = cam.WorldToViewportPoint(touchPosition);

                Vector3 touchPosition = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 1));
                // x offset from finger touch when further from right side of screen
                // if touchposition.x further from the right side of the screen, increase the offset
                float offsetMultiplyer = 1.3f;
                //float xOffset = _screenWidthMax - ((touchPosition.x * viewportTouch.x) * offsetMultiplyer);
                float xOffset = _screenWidthMax - (touchPosition.x * offsetMultiplyer);
                touchPosition.x -= xOffset;
                // lock the player to a point on screen
                touchPosition.y = _playerY;
                transform.position = Vector3.Lerp(transform.position, touchPosition, 0.15f);
                if (transform.position.x > _screenWidthMax) {
                    transform.position = new Vector3(_screenWidthMax, transform.position.y, 1);
                }
                else if (transform.position.x < _screenWidthMin) {
                    transform.position = new Vector3(_screenWidthMin, transform.position.y, 1);
                }
            }
        }
    }
}
