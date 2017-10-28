using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Move_Script : MonoBehaviour {
	GameObject FloorObj;
	Vector3 Destiny;

	public float Speed;
	public int Advantage;
	public bool Check_Enemy;
	public bool Stop_For_Fight;

	// Use this for initialization
	/*void Start () {
		FloorObj = GameObject.Find ("Floor");
		Destiny = Vector3.zero;
		Speed = 5f;
		Advantage = Set_Advantage (this.transform);
		if (Advantage > 3) {
			Check_Enemy = true;
		} else {
			Check_Enemy = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (this.transform.position == Destiny
			|| Destiny == Vector3.zero)
			//Set_Random_Destiny();
		if (Stop_For_Fight == false) {
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (Destiny.x, Destiny.y, Destiny.z), Time.deltaTime * Speed);
		}
	}



	public int Set_Advantage(Transform obj, Transform tkMud){
		int Advan = 0;
		if (obj.gameObject.tag == "Bear")
			Advan = 1;
		else if(obj.gameObject.tag == "Rabbit")
			Advan = 2;
		else if(obj.gameObject.tag == "Fox")
			Advan = 3;
		else if(obj.gameObject.tag == "EBear")
			Advan = 4;
		else if(obj.gameObject.tag == "ERabbit")
			Advan = 5;
		else if(obj.gameObject.tag == "EFox")
			Advan = 6;

		return Advan;
	}

	//내가 이기냐 지냐? return bool
	//이겼으면 어ㅓㅎ게 할건지
	//졌ㅡ면 어ㅓㅎ게 할ㄴ지

	void KillDoll()
	{
		
	}

	void OnTriggerEnter2D(Collider2D col){
		int Check_Tag = col.GetComponent<Random_Move_Script> ().Set_Advantage (col.transform);
		//Debug.Log (Check_Tag);
		switch(Set_Advantage(this.transform)){
		case 1:
			if (Check_Tag == 5)
				this.gameObject.SetActive (false);

			break;
		case 2:
			if (Check_Tag == 6)
				this.gameObject.SetActive(false);
			break;
		case 3:
			if (Check_Tag == 4)
				this.gameObject.SetActive(false);
			break;
		case 4:
			if (Check_Tag == 2)
				this.gameObject.SetActive(false);
			break;
		case 5:
			if (Check_Tag == 3)
				this.gameObject.SetActive(false);
			break;
		case 6:
			if (Check_Tag == 1)
				this.gameObject.SetActive(false);
			break;
		}
	}

*/
}
