using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public float turnDelay = .1f;
	private List<Enemy> enemies;
	public float levelStartDelay = 2f;
	private Text levelText;
	private GameObject levelImage;
	public bool battle = false;
	private bool doingSetup;
	private bool enemyesMoving;
	// Use this for initialization
	public BoardManager boardScript;
	public static GameManager instance = null;
	private int level = 1;
	public int playerFoodPoints = 100;
	[HideInInspector]public bool playersTurn = true;

	void Awake () {
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}
		enemies = new List<Enemy>();
		DontDestroyOnLoad(gameObject);		
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
	private void OnLevelWasLoaded (int index){
		level = level+1;
		InitGame();
	}
	void InitGame(){
		doingSetup = true;
		// levelText.text = "Day "+level;
		// levelImage.SetActive(true);
		Invoke("HideLevelImage",levelStartDelay);
		enemies.Clear();
		boardScript.SetupScene(level);

	}
	private void HideLevelImage(){
		// levelImage.SetActive(false);
		doingSetup = false;
	}
	public void GameOver(){
		// levelText.text = "After "+level + " days, you starved.";
		// levelImage.SetActive(true);
		// enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(playersTurn || enemyesMoving || doingSetup){
			return;
		}	
		StartCoroutine(MoveEnemies());

	}
	public void AddEnemyToList(Enemy script){
		enemies.Add(script);
	}
	public void startBattle(Enemy enemy){
		if(!battle){
			boardScript.startBattleScene();
			battle = true;
			Debug.Log(enemy.stringy);
		}
	}

	IEnumerator MoveEnemies(){
		if(!battle){
			enemyesMoving = true;
			yield return new WaitForSeconds(turnDelay);
			if(enemies.Count == 0){
				yield return new WaitForSeconds(turnDelay);
			}

			for(int i = 0; i < enemies.Count;i++){
				enemies[i].MoveEnemy();
				yield return new WaitForSeconds(enemies[i].moveTime);
			}
			playersTurn = true;
			enemyesMoving = false;
		}

	}
}
