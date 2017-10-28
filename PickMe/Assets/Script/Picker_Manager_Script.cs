using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Doll{
public class Picker_Manager_Script : MonoBehaviour {

	public float Down_Time;
	public float Time_Check;
	public bool Stop_Check;
	public GameObject floor;
		Vector2 Picker_Pos;
	void Start () {
		Down_Time = 3f;
		Time_Check = 0f;
		Stop_Check = false;
			speed = 3f;
			newVec = Vector2.zero;
	}

	Vector2 newVec;
	float previous;
	public float speed;
	bool Check = false;
	// Update is called once per frame

	void Update () {
		if (Stop_Check == false) {
			Time_Check += Time.deltaTime;
			if (Time_Check > Down_Time) {
					 previous = this.transform.position.y;

				Stop_Check = true;
					float X = Random.Range (-floor.transform.localScale.x + this.transform.localScale.x, floor.transform.localScale.x - this.transform.localScale.x);
					float Y = Random.Range (-floor.transform.localScale.y + this.transform.localScale.y, floor.transform.localScale.y - this.transform.localScale.y);
					newVec = new Vector2 (X / 2, Y / 2);
			}
		} else {
				if (Check == false)
					DownPicker ();
				if (Check == true)
					ReturnPicker ();
		}
	}
		public	void DownPicker(){
			if (newVec.x != this.transform.position.x) {
				transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (newVec.x, transform.position.y), speed * Time.deltaTime);
			} else if (newVec.y != this.transform.position.y) {
				transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (newVec.x, newVec.y), speed * Time.deltaTime);
			} else {
				Check = true;
			}
		}
		public void ReturnPicker(){
			if (previous != this.transform.position.y) {
				transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), new Vector2 (transform.position.x, previous), speed * Time.deltaTime);
			} else {
				Time_Check = 0;
				Check = false;
				Stop_Check = false;
			}
		}
		void OnTriggerEnter2D(Collider2D col){
			Debug.Log ("! : " + col.tag);
		}
}
}
