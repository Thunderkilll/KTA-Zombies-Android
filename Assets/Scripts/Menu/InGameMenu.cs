using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

	GameObject p;
	public GameObject youWinScreen;
	void Start () {
		p = GameObject.FindWithTag ("player");
		p.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ResumeGame()
	{
		p.SetActive (true);
		gameObject.SetActive (false);
	}
	public void RestartLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		//SceneManager.LoadScene ("level1");

	
	}
	public void NextLevel()
	{
		print ("hiiii");
		youWinScreen.SetActive (false);
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		//SceneManager.LoadScene ("level1");


	}
	public void QuitGame()
	{
		Application.Quit();
	}
}
