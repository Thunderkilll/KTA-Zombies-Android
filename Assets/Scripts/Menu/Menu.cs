using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	public void level0()
	{
		SceneManager.LoadScene ("level0");
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

	}
	public void level1()
	{
		SceneManager.LoadScene ("level1");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
		

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
