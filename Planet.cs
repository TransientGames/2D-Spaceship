using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private float rotationSpeed = 30f;
    private float speed = 1f;
    private Camera cam;
    private float camFOV;
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    private int minDistance = 6;
    private int maxdistance = 15;
    public float halfHeightAtDistance;
    private float halfFieldOfView;

    private void Awake() {
        cam = Camera.main;
        camFOV = cam.fieldOfView;
        halfFieldOfView = camFOV * 0.5f * Mathf.Deg2Rad;
        _renderer = GetComponent<Renderer>();
        _propBlock = new MaterialPropertyBlock();
    }

    public void Initialize(float speed, float rotationSpeed, Vector3 position, Vector3 scale, Texture texture) {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        transform.position = position;
        transform.localScale = scale;

        // Get the current value of the material properties in the renderer.
        _renderer.GetPropertyBlock(_propBlock);
        // Assign our new value.
        _propBlock.SetTexture("_MainTex", texture);
        // Apply the edited values to the renderer.
        _renderer.SetPropertyBlock(_propBlock);
    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < halfHeightAtDistance * -1) {
            RebuildPlanet();
        }
    }

    private void RebuildPlanet() {
        int distance = Random.Range(minDistance, maxdistance);
        float speed = maxdistance / distance;

        float maxSpinSpeed = 30f;
        float rotationSpeed = maxSpinSpeed / distance;

        float size = 1f / distance;
        Vector3 scale = new Vector3(size, size, size);

        halfHeightAtDistance = distance * Mathf.Tan(halfFieldOfView);
        float halfWidthAtDistance = cam.aspect * halfHeightAtDistance;

        float xPos = Random.Range(-halfWidthAtDistance, halfWidthAtDistance);
        float yPos = halfHeightAtDistance;

        Vector3 position = new Vector3(xPos, yPos, distance);

        Texture tempTex = _propBlock.GetTexture("_MainTex");

        Initialize(speed, rotationSpeed, position, scale, tempTex);
    }
}
