using UnityEngine;
using System.Collections;

public class ManagerScript : MonoBehaviour {

	private bool areWeSpinning = false;
	private float[] outcome;
	private int wonCoins = 0;

	public GameObject[] wheels; //All the slotwheels go in here for easy access
	public float[] rotatePositions; //Define the positions where the image is on the slotwheel
	public string[] matches; //Setup matches f.e. 40|40|*:5 means has to be left 40, middle 40, right anything and pays out 5 times the input.
	public int level = 5; //At a scale of 1 to 10, how hard is it to win
	public int spinTime = 5; //How long the wheels spin
	public int spinTimeOffset = 1; //Time between each wheel to stop
	public int playerMoney = 25; //Player starting money
	public AudioClip coinDrop;
	public AudioClip winner;
	public AudioClip startSpinning;
	
	// Use this for initialization
	void Start () {
		outcome = new float[wheels.Length]; //Preload the array for the wheels
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 400, 80), "Press spacebar to start spinning.\r\nCoins in pocket: "+playerMoney);

		if(wonCoins > 0){
		GUI.Label(new Rect(10, 40, 400, 80), "WINNER!!!!\r\n-you won "+wonCoins+" coins-");
		}

		if(playerMoney == 0){
			GUI.Label(new Rect(10, 40, 400, 80), "You have no coins anymore...");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// If we press space or mousebutton left then spin the wheels and subtract the money
		if( (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonUp(0)) && areWeSpinning == false){
			if(playerMoney > 0){
				playerMoney--;
				calculateOutcome();
			}
		}
	}

	void calculateOutcome (){
		int currentSelectID = 0; 
		int lastID = Random.Range(0, rotatePositions.Length);
		int end;
		int begin;
		//For each wheel calculate the outcome before they start to spin.
		foreach (GameObject go in wheels) {
			//Lower then level 5? make it hard and as random as possible
			if( level < 5 ){
				begin = 0;
				end = rotatePositions.Length;
			//Higher then level 5 and lower then level 8 then add 3 and subtract 3 from the chosen number to randomize the next outcome
			}else if( level >= 5 && level < 8){
				begin = (lastID-3 >= 0) ? lastID-3 : 0;
				end = (lastID+3 < rotatePositions.Length) ? lastID+3 : rotatePositions.Length;
			//Higher then level 8 add 1 and subtract 1 from the chosen number to randomize the next outcome to make it super easy to win
			}else{
				begin = (lastID-1 >= 0) ? lastID-1 : 0;
				end = (lastID+1 < rotatePositions.Length) ? lastID+1 : rotatePositions.Length;
			}
			int rRange = Random.Range(begin, end); //Get a random number
			outcome[currentSelectID] = (float)rotatePositions[rRange]; //Final outcome
			lastID = rRange; //Save last roll number
			currentSelectID++;
		}
		//Start the animation and sounds
		startSpining();
	}

	void startSpining (){
		areWeSpinning = true;
		int offset = 0;
		int currentSelectID = 0;
		int totalSpinTime = 0;
		//For each wheel we will give it the spin time and stop position when timer is ended
		foreach (GameObject go in wheels) {
			totalSpinTime = spinTime + (spinTimeOffset * offset);
			float position = outcome[currentSelectID];
			offset++; currentSelectID++;
			go.GetComponent<WheelScript>().spinTime = totalSpinTime;
			go.GetComponent<WheelScript>().startSpinning = true;
			go.GetComponent<WheelScript>().endPosition = position;
		}
		//Play the start spinning sound
		GetComponent<AudioSource>().PlayOneShot(startSpinning, 0.7F);
		//Start the timer
		StartCoroutine(spinningTime(totalSpinTime));
	}

	void getCollection (){
		//Lets see what we have in our collection and pay it out if we got combos
		string collection = "";
		foreach (float state in outcome) {
			if(collection == ""){
				collection = ""+state;
			}else{
				collection = collection + "|" +state;
			}
		}
		payPlayer(collection);
		areWeSpinning = false;
	}

	void payPlayer(string payment) {
		Debug.Log(payment);
		string[] collection = payment.Split(char.Parse("|"));
		int pay = 0;
		//Check for each wheel outcome if it matches any of the wins we added.
		foreach (string match in matches) {
			string[] part = match.Split(char.Parse(":"));
			string payout = part[1];
			string[] splitted = part[0].Split(char.Parse("|"));
			if(splitted[0] == collection[0] || splitted[0] == "*"){
				if(splitted[1] == collection[1] || splitted[1] == "*"){
					if(splitted[2] == collection[2] || splitted[2] == "*"){
						if(int.Parse(payout) > pay){
							//We have a winner! pay out the highest winner when we have more winners. (like 2 cherrys and 3 cherrys we only want to payout the 3 cherrys)
							pay = int.Parse(payout);
						}
					}
				}
			}
		}
		if(pay > 0){
			//If we won anything start paying and play sounds to announce the player have won coins.
			playerMoney += pay;
			wonCoins = pay;
			StartCoroutine(wonTime());
			GetComponent<AudioSource>().PlayOneShot(winner, 1.0F);
			GetComponent<AudioSource>().PlayOneShot(coinDrop, 1.0F);
			print ("WINNER!!!! We paid you "+pay);
		}
	}

	IEnumerator spinningTime (float time)
	{
		yield return new WaitForSeconds(time);
		
		getCollection();
	}

	IEnumerator wonTime ()
	{
		yield return new WaitForSeconds(3);
		
		wonCoins = 0;
	}

}
