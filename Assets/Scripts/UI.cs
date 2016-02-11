using UnityEngine;
using UnityEngine.UI;   //We include the unityEngine.UI so we can control it
using UnityEngine.SceneManagement;  //this is used for loading levels
using System.Collections;

public class UI : MonoBehaviour {

    //hits is the number of times the player can be hit before they are killed
    //this number is collected from the GameControl class
    int hits;

    //text objects are the UI text object, this is the one we put the hits number in
    Text hitText;

    //this is the score number, we get from the GameControl class
    int score;

    Text scoreText;

    //the text box that will hold the game over message when needed
    Text gameOverText;

    //the number of enemies that the level starts with, send to Game control to make the level
    //winnable by the player. Make sure you set this to the number of enemies that start
    public int totalEnemies;

	// Use this for initialization
	void Start () {

        //To find our hitText object we have to search through all the game objects in the scene
        //if we find one with the proper name, we get the component of the text object so we can
        //manipulate it correctly.
        hitText = GameObject.Find("HitText").GetComponent<Text>();
        //collect the number of hits the player has right now
        hits = GameControl.hits;

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        score = GameControl.score;

        //get the game over text object
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        //clear out the game over text, because the game isn't over right now
        //instead we give instructions to start the game
        gameOverText.text = "Press Start \n x restarts";
        //the \n command ends the line in a string of text

        //set the totalenemies in the gamecontrol so the game can end properly
        GameControl.Set_Total_Enemies(totalEnemies);


        //start the game paused
        Time.timeScale = 0;

	
	}
	
	// Update is called once per frame
	void Update () {

        //keep the number of hits updated for the player
        hits = GameControl.hits;

        score = GameControl.score;

        //change the text in the hitText box so the player knows they need to be careful
        //references the text objects text property (var) and sets it to the hit number
        //however, our hits are an integer, so we have to convert it to a string using the
        //ToString() method that comes with C#.
        hitText.text = "Hits: "+hits.ToString();

        scoreText.text = "Score: " + score.ToString();

        //this is my pause button control
        //sumbit has been mapped to the start button on my controller, or enter on keyboard
        if (Input.GetButtonDown("Submit"))
        {
            //check to see if we are already paused
            if (Time.timeScale != 0)
            {
                //!= means we're not, so we set the time scale to 0%, stopping the game
                Time.timeScale = 0;
            }
            else
            {
                //set the time scale back up to 100%
                Time.timeScale = 1;
                //I included this to get rid of the press start message at the beginning
                //however, I can see some problems with doing it this way in the future
                gameOverText.text = "";
            }
            
        }

        //This resets the level, for debugging purposes
        if(Input.GetButtonDown("Fire3"))
        {
            //just reload the level using the scene name
            SceneManager.LoadScene("Scrap");
            //add the player back to the three lives so that they are started over
            //when score is added in, you would want to do the same thing with score here
            GameControl.Set_Hits(3);
        }
	
	}//end update method

    //custom method that will be called by Game Control when the player has died
    public void Game_Over()
    {
        //change the game over message so we know
        gameOverText.text = "GAME OVER";
    }
}
