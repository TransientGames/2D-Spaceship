using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapManager : MonoBehaviour
{

    public List<GameObject> rocks = new List<GameObject>();
    public List<GameObject> scrapMetal = new List<GameObject>();

    public void SpawnRocks(Vector3 position) {
        int numOfRocks = Random.Range(1, 4);
        for (int i = 0; i < numOfRocks; i++) {
            int randRock = Random.Range(0, rocks.Count);
            GameObject rock = Instantiate(rocks[randRock], position, Quaternion.identity);
            rock.transform.SetParent(this.transform);
            rock.gameObject.layer = 9;
        }
    }

    public void SpawnScrapMetal(Vector3 position) {
        int numOfScrap = Random.Range(1, 4);
        for (int i = 0; i < numOfScrap; i++) {
            int randScrap = Random.Range(0, scrapMetal.Count);
            GameObject scrap = Instantiate(scrapMetal[randScrap], position, Quaternion.identity);
            scrap.transform.SetParent(this.transform);
            scrap.gameObject.layer = 9;
        }
    }

}
