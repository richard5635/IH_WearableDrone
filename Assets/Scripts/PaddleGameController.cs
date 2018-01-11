using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PaddleRun
{
    
    public class PaddleGameController : MonoBehaviour
    {
        [Header("Paddle Game Controller")]

        private bool MonsterCalled;
        public GameObject monster;
        public GameObject player;
        public GameObject[] hazards;
        public Vector3 spawnValues;
        public int hazardCount;
        public float spawnWait;
        public float startWait;
        public float waveWait;

        public Text scoreText;
        public Text restartText;
        public Text gameOverText;
        public Text timerText;
        public Text warningText;

        [Header("Timer")]
        public int gameTime;


        private bool gameOver;
        private bool restart;
        private int score;
        //private bool Invincible;

        void Start()
        {
            gameOver = false;
            restart = false;
            MonsterCalled = false;
            restartText.text = "";
            gameOverText.text = "";
            warningText.text = "";
            score = 0;

            UpdateScore();
            StartCoroutine(Timer());
            //StartCoroutine(SpawnWaves());
        }

        void Update()
        {
            if (restart)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }

            if(Input.GetKeyUp("m"))
            {
                if(!MonsterCalled)
                {
                    StartCoroutine(spawnMonster());
                    MonsterCalled = true;
                }
            }


        }

        public void SpawnWaves(GameObject waveBox)
        {
            StartCoroutine(CoSpawnWaves(waveBox));
        }

        IEnumerator Timer()
        {
            while(gameTime > 0)
            {
                timerText.text = gameTime.ToString("0");
                yield return new WaitForSeconds(1.0f);
                gameTime --;
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
            scoreText.text = "SCORE: " + score;
        }

        public void GameOver()
        {
            gameOverText.text = "Game Over!";
            gameOver = true;
        }

        IEnumerator spawnMonster(){
            warningText.text = "WARNING!\nMONSTER APPROACHING";
            yield return new WaitForSeconds(2.5f);
            warningText.text = "";
            yield return new WaitForSeconds(0.5f);
            monster.SetActive(true);
            yield return new WaitForSeconds(20.0f);
            monster.SetActive(false);
            MonsterCalled = false;
            yield break;
        }
    }

}

