using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MovingObject {
	public Text foodText;
	public int wallDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
	public float restartLevelDelay = 1f;
	private Animator animator;
	private int life;
	public AudioClip moveSound1;
	public AudioClip moveSound2;
	public AudioClip eatSound1;
	public AudioClip eatSound2;
	public AudioClip drinkSound1;
	public AudioClip drinkSound2;
	public AudioClip gameOverSound;
	public static Player instance = null;

	public int xPos = 0;
	public int yPos = 0;

	// Use this for initialization
	protected override void Start () {
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

		animator = GetComponent<Animator>();
		// food = GameManager.instance.playerFoodPoints;
		// foodText.text = "Food: "+ food;
		base.Start();
	}
	private void OnDisable(){
		// GameManager.instance.playerFoodPoints = food;

	}
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.instance.playersTurn){
			return;
		}
		int horizontal = 0;
		int vertical   = 0;

		horizontal = (int) Input.GetAxisRaw("Horizontal");
		vertical = (int) Input.GetAxisRaw("Vertical");

		if(horizontal != 0){
			vertical = 0;
		};

		if(horizontal != 0 || vertical != 0){
			AttemptMove<Wall> (horizontal,vertical);
		};
	}

	protected override void onCantMove<T>(T component){
		Wall hitWall = component as Wall;
		hitWall.DamageWall(wallDamage);
		animator.SetTrigger("playerChop");
	}

	private void Restart(){
		Application.LoadLevel(Application.loadedLevel);
	}
	protected override void AttemptMove<T>(int xDir, int yDir){
		// food = food -1;
		// foodText.text = "Food: "+ food;
		if(!GameManager.instance.battle){
			base.AttemptMove <T> (xDir,yDir);
			RaycastHit2D hit;
			if(Move (xDir,yDir,out hit)){
				SoundManager.instance.RandomizeSfc(moveSound1,moveSound2);
			}
			GameManager.instance.playersTurn = false;
		}
	}
	// public void LoseFood (int loss){
	// 	animator.SetTrigger("playerHit");
	// 	food = food - loss;
	// 	foodText.text = "-" + loss + " | Food: "+ food;
	// }
	private void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Exit"){
			// Invoke("Restart",restartLevelDelay);
			// enabled = false;
			// Debug.Log("Reached the stairs, gz!");
		}
		else if(other.tag == "Enemy"){
			Debug.Log("Enemy! lookout");
		}
		else if(other.tag == "question"){
			questionTrigger qt = other.GetComponent<questionTrigger>();
			Debug.Log(" question: "+qt.questionID);
			SceneManager.LoadScene("questionScne", LoadSceneMode.Additive);

		}
		else if(other.tag == "info"){
			infoTrigger it = other.GetComponent<infoTrigger>();
			Debug.Log(" info: "+it.infoID);
		}
 
	}


	private void CheckIfGameOver(){
		if(life <= 0){
			SoundManager.instance.PlaySingle(gameOverSound);
			SoundManager.instance.musicSource.Stop();
			GameManager.instance.GameOver();
		}
	}
}
