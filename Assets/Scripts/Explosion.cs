using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {

        anim = gameObject.GetComponent<Animator>();
        anim.Play("Explosion");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void animationDone()
    {
        Destroy(gameObject);
    }
}
