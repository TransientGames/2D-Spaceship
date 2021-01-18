using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UIManager _uiManager;
    private bool _gamePlaying = false;
    private bool _gameOver = false;
    private int _distance = 0;
    private int _iron = 0;
    private int _scrap = 0;
    private Player _player;
    private AsteroidManager _asteroidManager;
    private EnemyManager _enemyManager;
    private PowerUpManager _powerManager;

    private void Start() {
        _uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        _asteroidManager = GameObject.Find("Asteroid Manager").GetComponent<AsteroidManager>();
        _enemyManager = GameObject.Find("Enemy Manager").GetComponent<EnemyManager>();
        _powerManager = GameObject.Find("PowerUp Manager").GetComponent<PowerUpManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update() {
        if (Input.touchCount > 0 && _gamePlaying == false && _gameOver == false) {
            if (_player != null) {
                StartGame();
            }
        }
        else if(Input.touchCount > 0 && _gameOver){
            SceneManager.LoadScene(0);
        }
    }

    private void StartGame() {
        _gamePlaying = true;
        _uiManager.StartGame();
        _player.StartShooting();
        StartCoroutine(EventCycle());
        StartCoroutine(StartDistance());
    }

    IEnumerator StartDistance() {
        while (_gamePlaying) {
            yield return new WaitForSecondsRealtime(0.1f);
            if (_player != null) {
                _distance++;
                _uiManager.UpdateDistanceText(_distance);
            }
        }
    }

    public void GameOver() {
        StartCoroutine(GameOverCoroutine());
        _gamePlaying = false;
        _uiManager.GameOver();
    }

    IEnumerator GameOverCoroutine() {
        yield return new WaitForSeconds(1f);
        _gameOver = true;
    }

    IEnumerator EventCycle() {
        while (_gamePlaying) {
            yield return new WaitForSeconds(1.25f);
            int randEvent = Random.Range(0, 5);
            switch (randEvent) {
                case 0:
                    _enemyManager.SpawnSingleEnemy();
                    break;
                case 1:
                    _asteroidManager.SpawnSingleAsteroid(4f);
                    break;
                case 2:
                    _asteroidManager.SpawnRowOfAsteroids(4f);
                    break;
                case 3:
                    _powerManager.SpawnSinglePowerUp(4f);
                    break;
                case 4:

                    break;
            }
        }
    }

    public void AddIron() {
        _iron++;
        _uiManager.UpdateIronText(_iron);
    }

    public void AddScrap() {
        _scrap++;
        _uiManager.UpdateScrapText(_scrap);
    }
}
