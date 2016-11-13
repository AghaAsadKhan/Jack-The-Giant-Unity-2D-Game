using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject[] clouds;
	private float distanceBetweenClouds = 3f;
	private float minX;
	private float maxX;

	private float lastCloudPositionY;
	private float controlX;
	[SerializeField]
	private GameObject[] collectable;
	private GameObject player;



	// Use this for initialization
	void Awake() {
		controlX = 0;
		SetMinAndMax ();
		CreateClouds ();
	
	}
	
	void SetMinAndMax(){
		Vector3 bounds = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height,0));
	    
		maxX = bounds.x - 0.5f;
		minX = -bounds.x + 0.5f;
	      
	}


	void Shuffle(GameObject[] arrayToShufle){
		for(int i = 0; i<arrayToShufle.Length; i++)
		{
			GameObject temp = arrayToShufle[i];
			int random = Random.Range (i,arrayToShufle.Length);
			arrayToShufle[i] = arrayToShufle[random];
			arrayToShufle [random] = temp;
		}
	}
	// make clouds 
	void CreateClouds(){

		Shuffle (clouds);
		float positionY = 0f;

		for(int i =0; i <clouds.Length; i++)
		{
			Vector3 temp = clouds [i].transform.position;

			temp.y = positionY;


			//1open> making zik zak positions for clouds 
			if(controlX == 0)
			{
				temp.x = Random.Range (0.0f,maxX);
				controlX = 1;
			}else if (controlX ==1 )
			{
				temp.x = Random.Range (0.0f,minX);
				controlX = 2;
			}else if (controlX == 2)
			{
				temp.x = Random.Range (1.0f,maxX);
				controlX = 3;
			}else if (controlX == 3)
			{
				temp.x = Random.Range (-1.0f,minX);
				controlX = 0;
			}
            //1closed> 

			lastCloudPositionY = positionY;
			clouds [i].transform.position = temp;

			positionY -= distanceBetweenClouds;
		}
	
	}
}
