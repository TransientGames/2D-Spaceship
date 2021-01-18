using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _playText;
    [SerializeField] private GameObject _distanceTextObj;
    [SerializeField] private GameObject _gameOverTextObj;
    [SerializeField] private GameObject _restartTextObj;
    [SerializeField] private GameObject _ironTextObj;
    [SerializeField] private GameObject _scrapTextObj;
    private Text _distanceText;
    private Text _ironText;
    private Text _scrapText;

    private void Awake() {
        _distanceText = _distanceTextObj.GetComponent<Text>();
        _ironText = _ironTextObj.GetComponent<Text>();
        _scrapText = _scrapTextObj.GetComponent<Text>();
    }

    public void StartGame() {
        TogglePlayText(false);
        ToggleDistanceText(true);
        ToggleIronText(true);
        ToggleScrapText(true);
    }

    public void GameOver() {
        ToggleGameOverText(true);
        ToggleRestartText(true);
    }

    private void TogglePlayText(bool active) {
        _playText.SetActive(active);
    }

    private void ToggleDistanceText(bool active) {
        _distanceTextObj.SetActive(active);
    }

    public void UpdateDistanceText(int distance) {
        _distanceText.text = "Distance: " + distance;
    }

    private void ToggleIronText(bool active) {
        _ironTextObj.SetActive(active);
    }

    public void UpdateIronText(int iron) {
        _ironText.text = "Iron: " + iron;
    }

    private void ToggleScrapText(bool active) {
        _scrapTextObj.SetActive(active);
    }

    public void UpdateScrapText(int scrap) {
        _scrapText.text = "Scrap: " + scrap;
    }

    private void ToggleGameOverText(bool active) {
        _gameOverTextObj.SetActive(active);
    }

    private void ToggleRestartText(bool active) {
        _restartTextObj.SetActive(active);
    }
}
