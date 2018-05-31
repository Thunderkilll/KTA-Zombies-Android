using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Instantiation : MonoBehaviour {
	
	public GameObject[] enemies;
	public GameObject box;
	//score
	public Text scoreValue;
	public Text enemyLeftValue,timeLeftValue,highestScoreValue;
	private float myTime=60f,currentTime;
	public GameObject gameOverScreen,youWinScreen,waveOneScreen;

	private  static int score=0;
	private  static int enemyLeft=10;
	bool didWin =false,wave2=false;


	void Start () {
		Invoke ("StopWaveOneScreen",2f);
		wave2 = false;
		didWin =false;
		score = 0;
		enemyLeft = 10;
		
		for (int i = 1; i < 10; i++) {
			int x = Random.Range (-200, 47);
			int y = Random.Range (-45, 47);
			//int o = Random.Range (0, 2);
			Instantiate (enemies[1], new Vector2 (x, y), Quaternion.identity);
		}
		for (int i = 0; i < 20; i++) {
			int x = Random.Range (-200, 47);
			int y = Random.Range (-45, 47);
			Instantiate (box, new Vector2 (x, y), Quaternion.identity);
		}



	}

	// Update is called once per frame
	void Update () {
		if (PlayerPrefs.HasKey ("highestScore")) {
			if (score > PlayerPrefs.GetInt ("highestScore")) {
				PlayerPrefs.SetInt ("highestScore", score);
			}
		}
		else {
			PlayerPrefs.SetInt ("highestScore", score);
		}

		enemyLeftValue.text = enemyLeft.ToString();
		currentTime = (myTime - Time.timeSinceLevelLoad);
		timeLeftValue.text=currentTime.ToString();
		highestScoreValue.text = PlayerPrefs.GetInt ("highestScore").ToString ();

		scoreValue.text = score.ToString();
		if ((currentTime <= 0) && (enemyLeft > 0)) {

			gameOverScreen.SetActive (true);
		}
		if ((currentTime >= 0) && (enemyLeft < 0)&&(didWin==false)) {
			youWinScreen.SetActive (true);
			Invoke ("StopYouWin", 4f);
			didWin = true;
			myTime = 90f;
			enemyLeft = 30;
		}
		if ((didWin)&&(wave2==false)) {
			wave2 = true;

			for (int i = 1; i < 30; i++) {
				int x = Random.Range (-200, 47);
				int y = Random.Range (-45, 47);
				//int o = Random.Range (0, 2);
				Instantiate (enemies[1], new Vector2 (x, y), Quaternion.identity);
			}

		}

	}
	public void ScorePlus(int amount)
	{
		score += amount;
	}
	public void EnemyLeftCalc()
	{
		enemyLeft--;

	}
	void StopYouWin()
	{
		youWinScreen.SetActive (false);
	}
	void StopWaveOneScreen()
	{
		waveOneScreen.SetActive (false);
	}
		




}
