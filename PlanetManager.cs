using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public GameObject planet;
    public int numberOfPlanets;
    public List<Texture> textures = new List<Texture>();
    private Camera cam;
    private float camFOV;
    private int minDistance = 6;
    private int maxdistance = 15;
    private float halfFieldOfView;


    private void Awake() {
        cam = Camera.main;
        camFOV = cam.fieldOfView;
        halfFieldOfView = camFOV * 0.5f * Mathf.Deg2Rad;
    }


    void Start()
    {
        for (int i = 0; i < numberOfPlanets; i++) {
            if (i < numberOfPlanets * 0.7f) {
                BuildPlanets();
            }
            else {
                PlanetBuildDelay();
            }
        }
    }

    
    private void BuildPlanets() {
        int distance = Random.Range(minDistance, maxdistance);
        float speed = maxdistance / distance;

        float maxSpinSpeed = 30f;
        float rotationSpeed = maxSpinSpeed / distance;

        float size = 1f / distance;
        Vector3 scale = new Vector3(size, size, size);

        float halfHeightAtDistance = distance * Mathf.Tan(halfFieldOfView);
        float halfWidthAtDistance = cam.aspect * halfHeightAtDistance;

        float xPos = Random.Range(-halfWidthAtDistance, halfWidthAtDistance);
        float yPos = Random.Range(-halfHeightAtDistance, halfHeightAtDistance);

        Vector3 position = new Vector3(xPos, yPos, distance);

        Texture randTexture = textures[Random.Range(0, textures.Count)];

        GameObject obj = Instantiate(planet);
        Planet planetComp = obj.GetComponent<Planet>();

        obj.transform.SetParent(this.transform);
        obj.gameObject.layer = 8;
        planetComp.halfHeightAtDistance = halfHeightAtDistance;
        planetComp.Initialize(speed, rotationSpeed, position, scale, randTexture);
    }
    
    private void BuildRestOfPlanets() {
        int distance = Random.Range(minDistance, maxdistance);
        float speed = maxdistance / distance;

        float maxSpinSpeed = 30f;
        float rotationSpeed = maxSpinSpeed / distance;

        float size = 1f / distance;
        Vector3 scale = new Vector3(size, size, size);

        float halfHeightAtDistance = distance * Mathf.Tan(halfFieldOfView);
        float halfWidthAtDistance = cam.aspect * halfHeightAtDistance;

        float xPos = Random.Range(-halfWidthAtDistance, halfWidthAtDistance);
        float yPos = halfHeightAtDistance;

        Vector3 position = new Vector3(xPos, yPos, distance);

        Texture randTexture = textures[Random.Range(0, textures.Count)];

        GameObject obj = Instantiate(planet);
        Planet planetComp = obj.GetComponent<Planet>();

        obj.transform.SetParent(this.transform);
        obj.gameObject.layer = 8;
        planetComp.halfHeightAtDistance = halfHeightAtDistance;
        planetComp.Initialize(speed, rotationSpeed, position, scale, randTexture);
    }

    IEnumerator PlanetBuildDelay() {
        yield return new WaitForSeconds(3f);
        BuildRestOfPlanets();
    }

}
