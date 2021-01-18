using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Camera cam;
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> enemySlots = new List<GameObject>();
    public List<float> slotLocations = new List<float>();
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


    public void SpawnSingleEnemy() {
        for (int i = 0; i < enemySlots.Count; i++) {
            if (enemySlots[i] == null) {
                float yPos = _screenHeightMax;
                bool foundSlot = false;
                int randXPos = 0;
                while (foundSlot == false) {
                    randXPos = Random.Range(0, enemySlots.Count);
                    if (enemySlots[randXPos] == null) {
                        foundSlot = true;
                    }
                }
                float xPos = slotLocations[randXPos];
                Vector3 position = new Vector3(xPos, yPos, 1);

                int randOBJ = Random.Range(0, enemies.Count);
                GameObject obj = Instantiate(enemies[randOBJ]);
                obj.transform.SetParent(this.transform);
                enemySlots[randXPos] = obj;
                obj.transform.position = position;
                obj.gameObject.layer = 9;
                break;
            }
        }
    }
}
