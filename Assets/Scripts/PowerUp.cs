using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    //this is a power up class that can change the players stats in different ways
    //so far, it just gives them back a hit - a life up kind of thing.

    //an integer of how many hits the player gets back when they grab it, could be negative to take away
    public int hitGain;


    //The powerup should float downward so it doesn't look weird with eveything else
    public float scrollSpeed;


    //the power up will float around for a little while, but disappear after a while
    //lifespan is total time to live, lifetime is the counter variable
    public int lifeSpan;
    int lifeTime;

	// Use this for initialization
	void Start () {

        //set the timeer to the maximum lifespan
        lifeTime = lifeSpan;
	
	}
	
	// Update is called once per frame
	void Update () {

        //count down the life timer
        lifeTime--;

        //scroll down a bit
        transform.position = new Vector2(transform.position.x, transform.position.y - scrollSpeed);

        //if we are out of time...
        if (lifeTime <= 0)
        {
            //destroy the power up object
            Destroy(gameObject);
        }
	
	}

    //when something runs into it
    void OnCollisionEnter2D(Collision2D other)
    {
        //make sure the player is the one that hit the power up
        if (other.gameObject.tag == "Player")
        {
            //at this point, just one effect - add a life
            GameControl.Add_Hits(hitGain);

            //pickup is worth 50 points
            GameControl.Add_Score(50);

            //powerup has been used, get rid of it
            Destroy(gameObject);
        }
    }
}
