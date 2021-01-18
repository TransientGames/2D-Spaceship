using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Player _player;
    public int _shieldStrength = 3;
    private int _shieldMaxStrength = 3;

    private void Awake() {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnEnable() {
        _shieldStrength = _shieldMaxStrength;
    }


    private void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case "Asteroid":
                Hit(other.gameObject);
                break;
            case "EnemyLaser":
                Hit(other.gameObject);
                break;
        }
    }

    private void Hit(GameObject other) {
        Destroy(other);
        _shieldStrength--;
        if (_shieldStrength == 0) {
            this.gameObject.SetActive(false);
            _player.hasShield = false;
        }
    }

    public void Reshield() {
        _shieldStrength = _shieldMaxStrength;
    }
}
