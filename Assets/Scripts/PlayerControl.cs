using UnityEngine;
using System.Collections;

//this makes sure that the player always has a rigidbody2d component
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerControl : MonoBehaviour {

    //the rigidbody attached to the object, so that we can add force to it    
    Rigidbody2D body;

    //the maximum time to wait for a bullet
    public int bulletTimer = 35;
    //when this time reaches 0, you can shoot again
    int bulletTime;

    //xForce and yForce are used to determine how fast we move in those directions
    public float xForce = 1;
    public float yForce = 0.5f;

    //this is the bullet prefab that the player will shoot when they hit the shoot button
    public GameObject bulletPreFab;

    //this is the prefab for the player when they explode
    public GameObject explosion;

	// Use this for initialization
	void Start () {

        //find the body so we can move
        body = gameObject.GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {

        //TODO
        //Add velocity check to keep players speed at a maximum


        //make sure the player can't move or shoot when the game is paused
        if (Time.timeScale == 0)
        {
            //the return command just ends the current method
            return;
        }


        //Count down the bullet time
        bulletTime--;
        
        //Joystick controls
        //the axis data comes from the left stick on the logitech controller
        //or from the WASD or arrow keys on keyboard
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //We use the axis numbers (from -1 to 1) to add force to the player
        //multiply that by the force var so we can change player speed through
        //power ups or something later in the game

        body.AddForce(new Vector2(0, yAxis) * yForce, ForceMode2D.Impulse);
        body.AddForce(new Vector2(xAxis, 0) * xForce, ForceMode2D.Impulse);

        //this is how we shoot things
        //Fire1 is the A button on the logitech by default, or Cntrl on keyboard
        //Fire1 is also defaulted mapped to the left mouse button
        if (Input.GetButton("Fire1") && bulletTime <= 0)
        {
            //do all the fire bullet code here. Fire1 can be remapped by the player in the build
            //and changed to any button on their controller

            //we create this new vector so that we spawn the bullet outside of the player
            Vector2 bulletPos = new Vector2(transform.position.x, transform.position.y + 0.5f);
            //Create the bullet prefab, a little bit infront of the player so we don't shoot ourselves
            Instantiate(bulletPreFab, bulletPos, Quaternion.identity);
            //set the bullet timer to zero so that we can not shoot anymore
            bulletTime = bulletTimer;
        }

        
        //Input commands using keyboard, Not as effecient
        //and not necessary because of above code, but I left it in so that
        //you can see how it would work
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Move left, using xForce
            body.AddForce(new Vector2(-1, 0) * xForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Move Right, using xForce
            body.AddForce(new Vector2(1, 0) * xForce, ForceMode2D.Impulse);
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Move upward, using yForce
            body.AddForce(new Vector2(0, 1) * yForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Move downard, using yForce
            body.AddForce(new Vector2(0, -1) * yForce, ForceMode2D.Impulse);
        }

        //Check if the bullet time is counted down and they are hitting space to shoot
        //doing it this way we can hold down the space bar and continually shoot, or hit the button repeatedly
        if (Input.GetKey(KeyCode.Space) && bulletTime <= 0)
        {
            //Shoot a thing!
            
            //we create this new vector so that we spawn the bullet outside of the player
            Vector2 bulletPos = new Vector2(transform.position.x, transform.position.y + 0.5f);
            //Create the bullet prefab, a little bit infront of the player so we don't shoot ourselves
            Instantiate(bulletPreFab, bulletPos, Quaternion.identity);
            //set the bullet timer to zero so that we can not shoot anymore
            bulletTime = bulletTimer;
        }

	}//End update

    //on collision is called when we run into something
    void OnCollisionEnter2D(Collision2D other)
    {
        //first we check if it was an enemy
        if (other.gameObject.tag == "Enemy")
        {
            //call the player hit method from game control, to change the number of lives
            GameControl.Player_Hit();
        }
        //check if it was a bullet
        if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "PlayerBullet")
        {
            //we may have just shot ourselves, but take damage never the less
            GameControl.Player_Hit();
        }
    }

    //this is a public custom method that is called when the player has lost all of his lives
    public void Die()
    {
        //make an explosion where we are at
        Instantiate(explosion, transform.position, Quaternion.identity);
        //kill the players gameobject
        Destroy(gameObject);
    }
}
