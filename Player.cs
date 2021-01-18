using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject laser;
    public GameObject Shield;
    private Shield _shieldComp;
    private GameManager _gameManager;
    private Vector3 touchOrigin;
    private float _fireRate = 1f;
    private float _rapidFireTimer = 5f;
    private float _timeSinceLastRapidFirePickup = -1f;
    public bool hasShield = false;
    private float _shieldTimer = 10f;
    private float _timeSinceLastShieldPickup = -1f;

    private void Awake() {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _shieldComp = Shield.GetComponent<Shield>();
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case "Asteroid":
                Destroy(other.gameObject);
                Hit();
                break;
            case "Scrap_Rock":
                Destroy(other.gameObject);
                _gameManager.AddIron();
                break;
            case "ScrapMetal":
                Destroy(other.gameObject);
                _gameManager.AddScrap();
                break;
            case "EnemyLaser":
                Destroy(other.gameObject);
                Hit();
                break;
            case "RapidFirePowerUp":
                Destroy(other.gameObject);
                StartCoroutine(RapidFireTimer());
                _timeSinceLastRapidFirePickup = Time.time + _rapidFireTimer;
                _fireRate = 0.3f;
                break;
            case "ShieldPowerUp":
                Destroy(other.gameObject);
                StartCoroutine(ShieldTimer());
                _timeSinceLastShieldPickup = Time.time + _rapidFireTimer;
                if (hasShield == true) {
                    _shieldComp.Reshield();
                }
                else {
                    hasShield = true;
                    Shield.SetActive(true);
                }
                break;
        }
    }

    public void StartShooting() {
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting() {
        while (this.gameObject != null) {
            yield return new WaitForSeconds(_fireRate);
            Vector3 position = transform.position;
            position.y += 1f;
            if (this.gameObject != null) {
                Instantiate(laser, position, Quaternion.identity);
            }
        }
    }

    IEnumerator RapidFireTimer() {
        yield return new WaitForSeconds(_rapidFireTimer);
        if (Time.time >= _timeSinceLastRapidFirePickup) {
            _fireRate = 1f;
        }
    }

    IEnumerator ShieldTimer() {
        yield return new WaitForSeconds(_shieldTimer);
        if (Time.time >= _timeSinceLastShieldPickup) {
            Shield.SetActive(false);
            hasShield = false;
        }
    }

    private void Hit() {
        if (hasShield == false) {
            _gameManager.GameOver();
            Destroy(this.gameObject);
        }
    }
}
