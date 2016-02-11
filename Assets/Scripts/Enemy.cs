using UnityEngine;
using System.Collections;

//this makes sure that the player always has a rigidbody2d component
[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour {

    //this is the preFab of the explosion when the enemy is killed
    public GameObject explosion;

    //fly speed controls how fast it moves from side to side
    public float flySpeed = 0.1f;
    //facingright is the direction it is moving, either right or left
    public bool facingRight = true;
    //flylength is how far it moves before changing direction
    public int flyLength = 100;
    //flycounter counts down the frames until changing direction
    int flyCounter;

    //scroll speed determines how fast it moves down the screen, to give
    //a sense of movement through space
    public float scrollSpeed = 0.01f;

    //make a bool so we can decide if this enemy will shoot anything or not
    public bool willShoot;
    //bulletTimer is how long before the enemy shoots
    public int bulletTimer;
    //this is to keep track of how long ago the enemy shot
    int bulletTime;
    //set multipleShots to true if you want the enemy to shoot more than once
    public bool multipleShots;
    //this is the type of bullet you want the enemy to shoot
    public GameObject bulletPreFab;


    //these variables control if the enemy drops a power up or not
    public bool dropsPowerUp;
    //what kind of power up
    public GameObject powerUpPreFab;
    //and give a percent chance to drop it when killed
    public int dropChance;


	// Use this for initialization
	void Start () {

        //set the flycounter to the proper length
        flyCounter = flyLength;
        //set the bulletTime to the maximum
        bulletTime = bulletTimer;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //this code determines if it is time to shoot or not

        //count down the timer
        bulletTime--;
        //check to see if the timer is at zero, and shoot a bullet 
        if (bulletTime == 0 && willShoot == true)
        {
            //shoot the bullet straight down
            Instantiate(bulletPreFab, new Vector2(transform.position.x, transform.position.y - 0.5f), Quaternion.identity);
            //If we want to shoot again, reset the shot counter
            if (multipleShots)  //this reads the same as if multipleShots == true
            {
                //set the counter back to the maximum to wait and shoot again
                bulletTime = bulletTimer;
            }
        }



        //Code below is to move the enemy
        
        //check the direction we're facing
        if (facingRight)
        {
            //move to the right by the flyspeed amount
            transform.position = new Vector2(transform.position.x + flySpeed, transform.position.y);
        }
        else
        {
            //move to the left by the flyspeed amount
            transform.position = new Vector2(transform.position.x - flySpeed, transform.position.y);
        }

        //count down the flycounter each frame
        flyCounter--;
        //check if the flycounter is zero, change direction and reset
        if (flyCounter <= 0)
        {
            facingRight = !facingRight; //toggle the facingRight boolean
            flyCounter = flyLength;   //reset flyCounter to default length
        }

        //scroll down the screen a bit, based on scroll speed
        transform.position = new Vector2(transform.position.x, transform.position.y - scrollSpeed);

	
	} // end of fixed update method

    //this is called whenever the enemy hits something
    void OnCollisionEnter2D(Collision2D other)
    {
        //Check to see that what we hit was the player's bullet
        if (other.gameObject.tag == "PlayerBullet")
        {

            //Since it got shot, we might drop a power up
            //first, get a number based on the drop chance and the random number generator
            //get a number between 0 and 100
            int dropRoll = Random.Range(0, 100);
            //compare the roll with dropChance, if the roll is lower than chance it was successful
            if(dropRoll <= dropChance)
            {
                //drop the power up right there
                Instantiate(powerUpPreFab, transform.position, Quaternion.identity);
            }

            //give the player 100 points for killing the enemy
            GameControl.Add_Score(100);

            //create the explosion prefab at the location of the enemy
            Instantiate(explosion, transform.position, Quaternion.identity);
            //send information to the debug, this is just for testing purposes
            Debug.Log("Enemy hit by " + other.gameObject.name);

            //Take the enemy out of the count of enemies
            GameControl.Add_Enemy(-1);

            //destroy both the bullet and the enemy
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        //if we run into the player
        if (other.gameObject.tag == "Player")
        {

            //create an explosion at the collision center
            Instantiate(explosion, transform.position, Quaternion.identity);

            //killing an enemy by running into is worth half points
            GameControl.Add_Score(50);

            //Take the enemy out of the count of enemies
            GameControl.Add_Enemy(-1);

            //destroy the enemy - add code to hurt the player later
            Destroy(gameObject);
        }

        //if the enemy makes it to the bottom of the screen
        if(other.gameObject.tag == "Border")
        {
            //Take the enemy out of the count of enemies
            GameControl.Add_Enemy(-1);

            //just disappear
            Destroy(gameObject);
        }

    }
}
