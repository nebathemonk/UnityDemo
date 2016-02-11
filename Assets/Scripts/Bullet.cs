using UnityEngine;
using System.Collections;

//this makes sure that the player always has a rigidbody2d component
[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour {

    //find the body component so that we can move the bullet
    Rigidbody2D body;

    //xforce and yforce determine the path of the bullet, incase we want fancy moves
    public float xForce = 0f;
    public float yForce = 1f;

    //this will countdown how long the bullet will live, before disappearing
    public int lifetime = 100;

    //this is the type of energy ball we want, references the sprite array from StartUp class
    public int energyBall;

    //this is the sprite rendering component, we need to know this to change the sprite
    SpriteRenderer renderedSprite;

	// Use this for initialization
	void Start () {

        //find all of our components
        body = gameObject.GetComponent<Rigidbody2D>();
        renderedSprite = gameObject.GetComponent<SpriteRenderer>();

        //Set the sprite to the correct type
        renderedSprite.sprite = GameControl.energyBalls[energyBall];

        //start the bullet moving from the get go
        body.AddForce(new Vector2(0, 1) * yForce, ForceMode2D.Impulse);
        body.AddForce(new Vector2(1, 0) * xForce, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {


        //kill the bullet after a certain length of updates
        lifetime--;     //this counts down by one, each call of update
        //check if the lifetime counter is zero
        if(lifetime <= 0){
            //this destroys the bullet object
            Destroy(gameObject);
        }
	
	}

    //this was a test method to see if we could change bullet at run time.
    //at this moment, it is depreciated (inert, useless, no beuno)
    public void make(int bulletType = 0, float XF = 1f, float YF = 1f, int life=100)
    {
        //this is a method with defaults, incase you leave out some information when calling it

        //set the sprite of the bullet
        renderedSprite.sprite = GameControl.energyBalls[bulletType];
        //set the force variables
        xForce = XF;
        yForce = YF;
        //set the life variable
        lifetime = life;

        //start the bullet moving with the forces just decided
        body.AddForce(new Vector2(0, 1) * yForce, ForceMode2D.Impulse);
        body.AddForce(new Vector2(1, 0) * xForce, ForceMode2D.Impulse);
    }

    //Collision enter is for when we run into collider objects
    void OnCollisionEnter2D(Collision2D other)
    {

        Destroy(gameObject);
        /*
        //See if the bullet hit the edge of the screen, get rid of it
        if (other.gameObject.tag == "Border")
        {
            //kill the bullet silently
            Destroy(gameObject);
        }
         */
    }
}
