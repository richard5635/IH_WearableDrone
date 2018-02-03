using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PaddleRun
{

    public class PaddleGameController : MonoBehaviour
    {
        [Header("Game Controller")]

        private bool MonsterCalled;
        public GameObject monster;
        public GameObject player;
        public GameObject[] hazards;
        public Vector3 spawnValues;
        public int hazardCount;
        public float spawnWait;
        public float startWait;
        public float waveWait;

        [Header("GameScript")]
        public PlayerMovementPaddle02 playerMovementScript;

        [Header("UI Elements")]
        public GameObject warningBoard;
        public Button playAgain;
        public Text scoreText;
        public Text restartText;
        public Text gameOverText;
        public Text timerText;
        public Text warningText;
        public Text countdownText;

        [Header("Timer")]
        public int gameTime;


        private bool gameOver;
        private bool restart;
        private int score;
        private IEnumerator Timer_;
        private IEnumerator Countdown_;
        //private bool Invincible;

        void Awake()
        {
            Application.targetFrameRate = 60;
        }

        void Start()
        {
            gameOver = false;
            restart = false;
            MonsterCalled = false;
            restartText.text = "";
            warningBoard.SetActive(false);
            playAgain.gameObject.SetActive(false);
            gameOverText.text = "";
            warningText.text = "";
            score = 0;
            playerMovementScript.lockMovement = true;
            UpdateScore();
            
            Timer_ = Timer();
            Countdown_ = Countdown();
            
            //StartCoroutine(SpawnWaves());
        }

        public void StartGame() {
            StartCoroutine(Timer_);
            StartCoroutine(Countdown_);
        }

        void Update()
        {
            if (!playerMovementScript.lockMovement)
            {
                if (Input.GetKeyUp("m"))
                {
                    if (!MonsterCalled)
                    {
                        StartCoroutine(spawnMonster());
                        MonsterCalled = true;
                    }
                }
            }
            if (restart)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Restart();
                }
            }




        }

        public void SpawnWaves(GameObject waveBox)
        {
            StartCoroutine(CoSpawnWaves(waveBox));
        }

        IEnumerator Timer()
        {
            while (gameTime > 0)
            {
                timerText.text = gameTime.ToString("0");
                yield return new WaitForSeconds(1.0f);
                gameTime--;
            }
            if(gameTime ==0)
            {
                GameOver();
            }
        }
        IEnumerator CoSpawnWaves(GameObject waveBox)
        {
            yield return new WaitForSeconds(startWait);
            while (true)
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    float[] twoNum = new float[2];
                    twoNum[0] = spawnValues.x;
                    twoNum[1] = -spawnValues.x;
                    Vector3 spawnPosition = new Vector3(twoNum[Random.Range(0, twoNum.Length)], spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation, waveBox.transform.parent);
                    yield return new WaitForSeconds(spawnWait);
                }
                yield return new WaitForSeconds(waveWait);

                if (gameOver)
                {
                    restartText.text = "Press 'R' for Restart";
                    restart = true;
                    break;
                }
            }
        }

        public void AddScore(int newScoreValue)
        {
            score += newScoreValue;
            UpdateScore();
        }

        void UpdateScore()
        {
            scoreText.text = "SCORE: \n" + score;
        }

        public void GameOver()
        {
            StopCoroutine(Timer_);
            warningBoard.SetActive(true);
            gameOverText.text = "Game Over!";
            gameOver = true;
            playerMovementScript.lockMovement = true;
            playAgain.gameObject.SetActive(true);
        }

        IEnumerator spawnMonster()
        {
            warningBoard.SetActive(true);
            warningText.text = "WARNING!\nMONSTER APPROACHING";
            yield return new WaitForSeconds(2.5f);
            warningText.text = "";
            warningBoard.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            monster.SetActive(true);
            yield return new WaitForSeconds(20.0f);
            monster.SetActive(false);
            MonsterCalled = false;
            yield break;
        }

        IEnumerator Countdown()
        {
            countdownText.gameObject.SetActive(true);
            for (int i = 3; i > 0; i--)
            {
                countdownText.text = i.ToString();
                yield return new WaitForSeconds(1);
            }
            countdownText.text = "START!";
            yield return new WaitForSeconds(1);
            countdownText.gameObject.SetActive(false);
            playerMovementScript.lockMovement = false;
            StartCoroutine(Timer());
            yield return null;
        }

        public void Restart(){
            SceneManager.LoadScene("PaddleRun2");
        }

        //Show Screen
        //Finish Countdown
        

    }

}

