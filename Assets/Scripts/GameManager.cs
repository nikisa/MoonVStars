using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Text highscoreText;
    public Text waveText;

    public Text win;
    public Text lose;
    public Text highscoreTextEnd;
    public Text pointTextEnd;

    public GameObject username;
    private string _username;
    
    public int points = 0;
    public int combo = 1;

    private int _wave = 1;
    private bool _waveFinished = false;
    
    bool _hasLevelStarted = false;
    bool _isGamePlaying = false;
    bool _isGameOver = false;
    bool _hasLevelFinished = false;

    public bool HasLevelStarted { get { return _hasLevelStarted; } set { _hasLevelStarted = value; } }
    public bool IsGamePlaying { get { return _isGamePlaying; } set { _isGamePlaying = value; } }
    public bool IsGameOver { get { return _isGameOver; } set { _isGameOver = value; } }
    public bool HasLevelFinished { get { return _hasLevelFinished; } set { _hasLevelFinished = value; } }

    public float delay = 1f;

    public UnityEvent startLevelEvent;
    public UnityEvent playLevelEvent;
    public UnityEvent endLevelEvent;
    

    PlayerController _playerController;
    EnemyController _enemyController;
    EarthManager _earthManager;
   //public HighscoreTable _ht;

    private void Awake() {

        int highscore = GetHighscore();

        _playerController = Object.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        _enemyController = Object.FindObjectOfType<EnemyController>().GetComponent<EnemyController>();
        _earthManager = Object.FindObjectOfType<EarthManager>().GetComponent<EarthManager>();
        //_ht = Object.FindObjectOfType<HighscoreTable>().GetComponent<HighscoreTable>();

        
        
        Debug.Log("Highscore: " + highscore);


    }

    void Start() {
        if (_playerController != null) {
            StartCoroutine("RunGameLoop");
        }
    }

    IEnumerator RunGameLoop() {
        yield return StartCoroutine("StartLevelRoutine");
        yield return StartCoroutine("PlayLevelRoutine");
        yield return StartCoroutine("EndLevelRoutine");
    }

    IEnumerator StartLevelRoutine() {

        highscoreText.text = GetHighscore().ToString();

        _playerController.playerInputEnabled = false;
        _enemyController.EnemyActivated = false;
        
        while (!_hasLevelStarted) {
            yield return null;
        }

        if (startLevelEvent != null) {
            startLevelEvent.Invoke();
        }
    }
    IEnumerator PlayLevelRoutine() {
        _playerController.playerInputEnabled = true;
        _enemyController.EnemyActivated = true;
        _isGamePlaying = true;
      
        yield return new WaitForSeconds(delay);
        
        if (playLevelEvent != null) {
            playLevelEvent.Invoke();
        }

        while (!_isGameOver) {
            if (isWinner()) {
                lose.color = new Color(0, 0, 0, 0);
                pointTextEnd.text = points.ToString();
                highscoreTextEnd.text = GetHighscore().ToString();
                _isGameOver = true;
            }
            else if (isLoser()) {
                win.color = new Color(0, 0, 0, 0);
                pointTextEnd.text = points.ToString();
                highscoreTextEnd.text = GetHighscore().ToString();
                _isGameOver = true;

            }
            yield return null;
        }
        yield return null;
    }


    IEnumerator EndLevelRoutine() {
        _playerController.playerInputEnabled = false;
        _enemyController.EnemyActivated = false;

        if (endLevelEvent != null) {
            endLevelEvent.Invoke();
        }

        while (!_hasLevelFinished) {
            yield return null;
        }
        //ht.AddHighscoreEntry(points , _username);
        setHighscore(points);
        Restart();
    }

    void Restart() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void PlayLevel() {
        _hasLevelStarted = true;
    }

    public void nextWave() {
        _wave++;
        _earthManager.life = 3;
        _earthManager.UpdateEarthStatus();
        waveText.text = _wave.ToString();
}

    public void resetWave() {
        _wave = 0;
    }

    public int getWave() {
        return _wave;
    }

    public bool isWaveFinished() {
        return _waveFinished;
    }

    public void finishWave() {
        _waveFinished = true;
    }

    public void startWave() {
        _waveFinished = false;
    }

    bool isWinner() {
        return getWave() > 15;
    }

    bool isLoser() {
        return _earthManager.life <= 0;
    }

    public int getPoints() {
        return points;
    }

    public static int GetHighscore() {
        return PlayerPrefs.GetInt("highscore", 0);
    }

    public static bool setHighscore(int score) {
        int highscore = GetHighscore();
        if (score > highscore) {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            return true;
        }
        else {
            return false;
        }
    }

    public void SetName() {
        _username = username.GetComponent<InputField>().text;
        Debug.Log("Your name is: " + _username);
    }
}
