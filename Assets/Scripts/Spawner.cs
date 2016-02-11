using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    //The amount of enemies the spawner can make at once
    public int enemyLimit;
    //how many enemies are currently created by spawner
    int enemyCount;
    //The amount of updates between spawning enemies
    public int delayCount;
    //how many frames since the last enemy was spawned
    int count;

    //the type of enemy to spawn
    public GameObject enemyType;

	// Use this for initialization
	void Start () {

        //set the number of enemies made to zero
        enemyCount = 0;
        //set the counter to the delay setting
        count = delayCount;
	
	}
	
	// Update is called once per frame
	void Update () {

        //throw this in so that if the game is paused, we don't spawn enemies
        if (Time.timeScale == 0)
        {
            //return command ends the current method and ignores everything under it
            return;
        }
        //count down the counter by one each frame
        count--;

        //count down is over, lets spawn an enemy
        if (count <= 0)
        {
            //Make sure we don't have too many enemies already
            // the != means does not equal
            if (enemyCount != enemyLimit)
            {
                //change the position based on which enemy it is, to give some variety
                //at this point, I don't vary the position at all
                //float variance = Random.Range(0f, 0.3f);
                Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);

                //Instantiate the enemy type
                Instantiate(enemyType, enemyPos, Quaternion.identity);

                //Tell the GameControl we made another enemy
                GameControl.Add_Enemy(1);

                //update the count of enemies
                enemyCount++;

                //reset the counter
                count = delayCount;
            }
        }
	
	}
}
