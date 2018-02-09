using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace PaddleRun
{

    public class PaddleGameController : MonoBehaviour
    {


        [Header("Game Controller")]
        public bool KeyboardControl;
        public bool ViveControl;

        [Header("Game Objects")]

        private bool MonsterCalled;
        public GameObject Player;
        public GameObject monster;
        public GameObject Paddle;

        [Header("Spawn Control")]
        public Vector3 spawnValues;
        public GameObject[] hazards;
        public int hazardCount;
        public float spawnWait;
        public float startWait;
        public float waveWait;

        [Header("GameScript")]
        public PlayerMovementPaddle02 playerMovementScript;
        public SerialHandler serialHandler;

        [Header("UI Elements")]
        public GameObject warningBoard;
        public Button playAgain;
        public Text scoreText;
        public Text restartText;
        public TextMeshProUGUI gameOverText;
        public Text timerText;
        public GameObject warningText;
        //public TextMeshProUGUI warningText;
        public Text countdownText;
        public GameObject FinalScore;

        [Header("Timer")]
        public int gameTime;
        private int remainTime;


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
            gameOverText.gameObject.SetActive(false);
            score = 0;
            playerMovementScript.lockMovement = true;
            remainTime = gameTime;
            UpdateScore();

            Timer_ = Timer();
            Countdown_ = Countdown();

            StartGame();

            StartCoroutine(CoSpawnWaves());
        }

        public void StartGame()
        {

            StartCoroutine(Countdown_);
            Paddle.GetComponent<SteamVR_TrackedObject>().enabled = true;
            serialHandler.Write("s");
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
            if (!gameOver) AddScore();



        }

        public void SpawnWaves()
        {
            StartCoroutine(CoSpawnWaves());
        }

        IEnumerator Timer()
        {
            //make three times faster : spawn time divided by three
            //float spawnMultiplier = ExtensionMethods.Remap(remainTime / gameTime, 0, 1, 0.33f, 1);
            while (remainTime > 0)
            {
                timerText.text = remainTime.ToString("0");
                yield return new WaitForSeconds(1.0f);
                remainTime -= 1;
                //Game Behavior Control
                spawnWait -= 0.024f;
                waveWait -= 0.024f;
                if (remainTime <= 3)
                {
                    countdownText.gameObject.SetActive(true);
                    countdownText.text = remainTime.ToString("0");
                    countdownText.GetComponent<AudioSource>().Play();
                }
            }
            if (remainTime == 0)
            {
                timerText.text = remainTime.ToString("0");
                countdownText.text = "";
                GameOver();
            }
        }
        IEnumerator CoSpawnWaves()
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
                    Vector3 spawnPosition = new Vector3(
                        twoNum[Random.Range(0, twoNum.Length)], 
                        spawnValues.y, 
                        Player.transform.position.z + 16.0f + (Player.GetComponent<Rigidbody>().velocity.z * 0.1f) + Random.Range(-2.0f, 2.0f));
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
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

        public void AddScore()
        {
            score = (int)Player.transform.position.z;
            UpdateScore();
        }

        void UpdateScore()
        {
            scoreText.text = "SCORE: \n" + score;
        }

        public void GameOver()
        {
            StopCoroutine(Timer_);
            Paddle.GetComponent<SteamVR_TrackedObject>().enabled = false;
            warningBoard.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            gameOver = true;
            playerMovementScript.lockMovement = true;
            playAgain.gameObject.SetActive(true);
            FinalScore.SetActive(true);
            //Paddle.GetComponent<SteamVR_TrackedObject>().SetActive(false);
            serialHandler.Write("s");
        }

        IEnumerator spawnMonster()
        {

            warningBoard.SetActive(true);
            warningText.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            warningText.SetActive(false);
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
                countdownText.GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(1);
            }
            countdownText.text = "START!";
            yield return new WaitForSeconds(1);
            countdownText.gameObject.SetActive(false);
            playerMovementScript.lockMovement = false;
            StartCoroutine(Timer_);
            yield return null;
        }

        public void Restart()
        {
            SceneManager.LoadScene("PaddleRun2");
        }

        //Show Screen
        //Finish Countdown


    }

    public static class ExtensionMethods
    {

        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

    }

}

