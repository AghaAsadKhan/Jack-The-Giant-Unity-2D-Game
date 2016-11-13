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
		player = GameObject.Find ("Player");
	
	}
	void Start(){
		PositionPlayer ();
	
	}
	
	void SetMinAndMax(){
		Vector3 bounds =UnityEngine.Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height,0));
	    
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
	void PositionPlayer(){
		GameObject[] darkCloud = GameObject.FindGameObjectsWithTag ("Deadly");
		GameObject[] cloudsInGame = GameObject.FindGameObjectsWithTag ("Cloud");
		for(int i =0; i < darkCloud.Length; i++)
		{
			if(darkCloud[i].transform.position.y == 0f)
			{
				Vector3 vector3 = darkCloud [i].transform.position;
				darkCloud [i].transform.position = new Vector3 (cloudsInGame[0].transform.position.x,
					cloudsInGame[0].transform.position.y,
					cloudsInGame[0].transform.position.z);
				cloudsInGame [0].transform.position = vector3;


			}


		}
		Vector3 temp = cloudsInGame [0].transform.position;

		for(int i =1; i < cloudsInGame.Length; i++)
		{
			if (temp.y < cloudsInGame [i].transform.position.y) {
				temp = cloudsInGame [i].transform.position;
			
			}
		}
		temp.y += 0.8f;
		player.transform.position = temp;
	
	}

	void OnTriggerEnter2D(Collider2D target){

		if(target.tag == "Cloud" || target.tag =="Deadly")
		{
			if(target.transform.transform.position.y == lastCloudPositionY)
			{
				Shuffle (clouds);
				Shuffle (collectable);

				Vector3 temp = target.transform.position;

				for(int i=0; i< clouds.Length; i++)
				{
					if(!clouds[i].activeInHierarchy)
					{
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

						temp.y -= distanceBetweenClouds;
						lastCloudPositionY = temp.y;
						clouds [i].transform.position = temp;
						clouds [i].SetActive (true);
					}
					
				}


			}
		}
	}
}
