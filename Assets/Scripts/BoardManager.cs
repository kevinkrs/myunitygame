using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count{
		public int minimum;
		public int maximum;

		public Count (int min, int max){
			minimum = min;
			maximum = max;
		}
	}
	public MapManager mapManager;
	private bool playerSpawned = false;

	public Count wallCount = new Count (5,9);
	public Count foodCount = new Count(1,5);
	public GameObject exit;
	public GameObject[] floorTiles;
	public GameObject[] nodeTiles; 
	public GameObject[] foodTiles;
	public GameObject[] wallTiles;
	public GameObject[] enemyTiles;
	public GameObject[] outerWallTiles;
	List <List<Vector3>> mapGrid = new List <List<Vector3>>(); 
	public GameObject player;      //Public variable to store a reference to the player game object
	public Vector3 playPos;


	private Transform boardHolder;
	private List <Vector3> gridPositions = new List<Vector3>();


	void BoardSetup(){
		// mapManager.initMapData();
		boardHolder = new GameObject("Board").transform;
		mapManager = new MapManager();
		int _col = 0;
		int _row = 0;	
		mapGrid = mapManager.initMapData();
		int questionCounter = 0;
		int infoCounter = 0;
		for(int i = mapGrid.Count-1; i> -1;i--){
			for(int z = 0; z <mapGrid[i].Count ;z++){
				// full generation
				int tileIdentity = (int)mapGrid[i][z].x;
				int nodeContent = (int)mapGrid[i][z].y;
				int tileContent= (int)mapGrid[i][z].z;
				if(tileIdentity != 0){
					GameObject toInstantiate = floorTiles[tileIdentity-1];
					GameObject instance = Instantiate(toInstantiate, new Vector3 (_row,_col,0f), Quaternion.identity) as GameObject;   
					instance.transform.SetParent(boardHolder);

				}
				
				if(nodeContent != 0){
					GameObject toInstantiate = nodeTiles[nodeContent-1];
					GameObject instance = Instantiate(toInstantiate, new Vector3 (_row,_col,0f), Quaternion.identity) as GameObject;   
					if(nodeContent == 38){
						Debug.Log("spawning questions");
						     questionTrigger qt = instance.GetComponent<questionTrigger>();
							 qt.questionID = questionCounter;
							 questionCounter = questionCounter +1;
					}else if(nodeContent == 78){
						Debug.Log("spawning info");
						     infoTrigger it = instance.GetComponent<infoTrigger>();
							 it.infoID = infoCounter;
							 infoCounter = infoCounter +1;
					}else{

					}
					instance.transform.SetParent(boardHolder);
	
				}
				// events
				if(tileContent == 1){
					if(!playerSpawned){
						Debug.Log(_row);
						Debug.Log(_col);
						playPos = new Vector3(_row,_col,0f); 
						player.transform.position = playPos;  
					};
				};
				// enemies
				if(tileContent == 6){
					GameObject toInstantiate = enemyTiles[0];
					GameObject instance = Instantiate(toInstantiate, new Vector3 (_row,_col,0f), Quaternion.identity) as GameObject;   
					instance.transform.SetParent(boardHolder);
				};

				if(tileContent != 0){
					Debug.Log(tileContent);
						Debug.Log(_row);
						Debug.Log(_col);
				};
				_row = _row +1;
			}
			_row = 0; 
			_col = _col +1;
		}
	}
	public void startBattleScene(){
				int player_x = (int)player.transform.position.x;	
				int player_y = (int)player.transform.position.y;	
				
				// GameObject toInstantiate = floorTiles[1];
				// GameObject instance = Instantiate(toInstantiate, new Vector3 (player_y,player_x,-2f), Quaternion.identity) as GameObject;   

	}
	void updateBoard(){
		// foreach(Transform child in boardHolder){
		// 	Destroy(child.gameObject);
		// }

		int _col = 0;
		int _row = 0;	
		int player_x = (int)player.transform.position.x;	
		int player_y = (int)player.transform.position.y;	
		// int tileIdentity = (int)mapGrid[player_x][player_y].x;
		for(int i = mapGrid.Count-1; i> -1;i--){
			for(int z = 0; z <mapGrid[i].Count ;z++){
				int tileIdentity = (int)mapGrid[i][z].x;
				int tileNodeContent= (int)mapGrid[i][z].y;
				// Debug.Log(mapGrid[i][z].x);   tile
				// Debug.Log(mapGrid[i][z].y);   item/unit on tile
				// Debug.Log(mapGrid[i][z].z);   special events on tile
				if(_row == player_x || (_row < player_x+4 && _row > player_x-4 )){
					if(_col == player_y || (_col < player_y+4 && _col > player_y-4 )){
						
						// if(tileIdentity == 1 || tileIdentity == 2 || tileIdentity ==3){
						// GameObject toInstantiate = floorTiles[3];
						// GameObject instance = Instantiate(toInstantiate, new Vector3 (_row,_col,0f), Quaternion.identity) as GameObject;
						// instance.transform.SetParent(boardHolder);
						// }else{
						// GameObject toInstantiate = floorTiles[10];
						// GameObject instance = Instantiate(toInstantiate, new Vector3 (_row,_col,0f), Quaternion.identity) as GameObject;
						// instance.transform.SetParent(boardHolder);
						// }


						// if(tileNodeContent != 0){
						// 	GameObject tileNode = nodeTiles[tileNodeContent-1];
						// 	GameObject tileNodeInstance = Instantiate(tileNode, new Vector3(_row, _col,0f),Quaternion.identity);
						// 	tileNodeInstance.transform.SetParent(boardHolder);
						// }
						
					}
				}
				_row = _row +1;
			};
			_row = 0; 
			_col = _col +1;
		}
				// int tileIdentity = (int)mapGrid[player_y][player_x].x;
				// int tileContent= (int)mapGrid[player_y][player_x].y;
				// GameObject toInstantiate = floorTiles[tileIdentity];
				// GameObject instance = Instantiate(toInstantiate, new Vector3 (player_x,player_y,0f), Quaternion.identity) as GameObject;
				// instance.transform.SetParent(boardHolder);



		// Debug.Log(player.transform.position.x);
		// Debug.Log(player.transform.position.y);
	}	
	// Vector3 RandomPosition(){
	// 	int randomIndex = Random.Range(0, gridPositions.Count);
	// 	Vector3 RandomPosition = gridPositions[randomIndex];
	// 	gridPositions.RemoveAt(randomIndex);
	// 	return RandomPosition;
	// }

	// void LayoutObjectAtRandom(GameObject[] tileArray, int minimum,int maximum){
	// 	int objectCount = Random.Range(minimum,maximum +1);
	// 	for(int i = 0; i < objectCount;i++){
	// 		Vector3 randomPosition = RandomPosition();
	// 		GameObject tileChoice = tileArray[Random.Range(0,tileArray.Length)];
	// 		Instantiate(tileChoice, randomPosition, Quaternion.identity);
	// 	}; 
	// }

	public void SetupScene(int level){
		BoardSetup();
		// initialiseList();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		updateBoard();
	}

	
}



		// LayoutObjectAtRandom(wallTiles,wallCount.minimum,wallCount.maximum);
		// LayoutObjectAtRandom(foodTiles,foodCount.minimum,foodCount.maximum);
		// int logEnemyCount = (int)Mathf.Log(level,2f);
		// LayoutObjectAtRandom(enemyTiles,logEnemyCount,logEnemyCount);
		// Instantiate(exit, new Vector3(columns -1, rows-1,0f),Quaternion.identity);




		// mapLayout[0] = new List<Vector3>{new Vector3(1,1,0f),new Vector3(1,1,0f)};
		// mapArr = new int[]{};

		// for(int x = -1; x < columns +1; x++){
		// 	for(int y =-1; y < rows +1; y++){

		// 		GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
		// 		if(x == -1 || x == columns || y == -1 || y ==rows){
		// 			toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
		// 		};
		// 		GameObject instance = Instantiate(toInstantiate, new Vector3 (x,y,0f), Quaternion.identity) as GameObject;
		// 		instance.transform.SetParent(boardHolder);
		// 	}
		// }


			//void initialiseList(){
		// gridPositions.Clear();

		// for(int x = 1; x < columns -1; x++ ){
		// 	 for(int y = 1; y < rows - 1; y++){
		// 		 gridPositions.Add(new Vector3(x,y,0f));
		// 	 }
		// }
	//}