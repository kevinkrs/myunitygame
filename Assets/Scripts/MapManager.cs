using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;  

public class MapManager : MonoBehaviour {
    private string mapData = "mapData.txt";
	public static MapManager instance = null;


    // private List<List<Vector3>> mapLayout = new List<List<Vector3>>();

	public List<List<Vector3>> initMapData(){
		string filePath = Path.Combine(Application.streamingAssetsPath, mapData);
		if(File.Exists(filePath)){
			string dataAsJson = File.ReadAllText(filePath);
			char[] separator1 = {'|'};
			char[] separator2 = {','};
			char[] linebreaks = { '\n', '\r'};
 			string[] lineData = dataAsJson.Split(linebreaks);
			List <List<Vector3>> tempContentList = new List<List<Vector3>>();
			foreach(string line in lineData){
				if(line !="" && line !=" "){
					string[] entryData = line.Split(separator1);
					List <Vector3> templist = new List <Vector3>();
					foreach(string data in entryData){
						string[] c = data.Split(separator2);
						// Debug.Log(c);
						// Debug.Log(c[0]);
						// Debug.Log(c[1]);
						// Debug.Log(c[2]);
						templist.Add(new Vector3(float.Parse(c[0]),float.Parse(c[1]),float.Parse(c[2])));

					}
					tempContentList.Add(templist);
				}
			}
			return tempContentList;
		}else{
			return null;
		}
	}
	void Awake () {
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);		

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
