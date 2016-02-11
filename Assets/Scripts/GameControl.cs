using UnityEngine;
using UnityEngine.SceneManagement;  //For changing levels
using System.Collections;

//Static classes are able to be referenced and called by everything in the game
//the just always exist, 'static', and don't need a specific object to be referenced
//this is a good place to hold things like the players score and things that move from level to level

static public class GameControl {

    //you might notices this one does NOT have :monobehaviour inheritence
    //it is its own beast and works only in the way we tell it to
    //this also means we don't have access to things like
    //update() and start(), because those are part of monobehaviour family

    //this is an array that holds all of the different energy type sprites
    static public Sprite[] energyBalls;

    //hitsMax is the maximum amount of hits the player can have, and start with
    static public int hitsMax = 3;
    //this is the amount of hits the player has left
    static public int hits;

    //add another variable for keeping track of the players score
    static public int score;

    //this is the number of enemies that have to leave play to end the level
    static public int totalEnemies;

    // Use this for initialization of a static class, called a Constructor - just has the same name
    //and no return type. It can take arguments, though
    static GameControl()
    {
        //loaded all of the sprites in the resource folder given
        //so that we can use different types of bullets
        energyBalls = Resources.LoadAll<Sprite>("Sprites/Weapons");

        //set the current number of hits to the maximum
        hits = hitsMax;

        //set the score to zero at the beginning of game
        score = 0;

    }

    //this is a custom method for the GameControl class
    //PlayerHit is what happens when the player class has been damaged
    static public void Player_Hit()
    {
        //if we are already at zero hits, just kill us
        if (hits <= 0)
        {
            //tell the player they died. Place holder debug
            Debug.Log("GRAM ROVER. U DED");
            //call the game over method
            Game_Over();
        }
        else
        {
            //have some hits to lose, take one away
            hits--;
        }
    }

    //this is a public method that has an argumet - it takes in an integer
    //the integer is declared with a value when the method is called elsewhere in the code
    static public void Add_Hits(int hitsToAdd)
    {
        //add the number of hits to the current number of hits the player has
        hits += hitsToAdd;
    }

    //this is a similar method to the one above, but just sets the hits instead of adding
    static public void Set_Hits(int newHits)
    {
        //change the hits to whatever number we want. This is used for resetting mostly
        hits = newHits;
    }

    //custom method to add to the score 
    static public void Add_Score(int addScore)
    {
        score += addScore;
    }

    //method to set the score, probably mostly for restarts
    static public void Set_Score(int newScore)
    {
        score = newScore;
    }

    //use this method to change the total enemies at the beginning of the level
    //at the moment the best place I found for this is in the UI script, not the best solution
    //I would prefer to pull this out of a text file in the final build of a game like this.
    static public void Set_Total_Enemies(int total)
    {
        totalEnemies = total;
    }
    static public void Add_Enemy(int enemies)
    {
        //this adds or takes away from the amount of enemies necessary for ending the level
        totalEnemies += enemies;

        //this is a debug command to tell us if the totalenemies is updating correctly
        Debug.Log("Total enemies count at " + totalEnemies.ToString());


        if (totalEnemies <= 0)
        {
            //there are no enemies left to kill/spawn, end the level
            Level_Win();
        }
    }

    //call this method when we win a level to move on to the next one
    static public void Level_Win()
    {
        //figure out which scene we're in, then load the next one in the build settings list
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    static public void Game_Over()
    {
        //look for the player using a tag search, since there should only be one player on the field
        //but we don't know the exact object
        //then get the player control component on that object so we can call its methods
        PlayerControl player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        //tell the player that he has died
        player.Die();

        //find the ui script on the game control object in the scene
        UI ui = GameObject.Find("GameControl").GetComponent<UI>();
        //tell the ui that the game is over
        ui.Game_Over();
        
    }

}
