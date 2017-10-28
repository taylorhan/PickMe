using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class ERabbitDoll : Doll
    {

        // Use this for initialization
        void Start()
        {
            InitDoll();
        }

        // Update is called once per frame
        void Update()
        {
            CheckState();
			if (GetComponent<Doll>().Check_Super == true) {
				GetComponent<Doll> ().Timer += Time.deltaTime;
				if (GetComponent<Doll> ().Timer > GetComponent<Doll> ().Limit_Time) {
					GetComponent<Doll> ().invincible = false;
					GetComponent<Doll> ().Check_Super = false;
				}
			}
        }
        void OnTriggerEnter2D(Collider2D col)
        {
			if (GetComponent<Doll> ().invincible == false && col.GetComponent<Doll>().invincible == false) {
				bool isDead = CheckIsDead (Object_Manager_Script.eDoll.Fox, objManagerScript.ERabbitList, this.gameObject, col.gameObject);
			}
        }
    }
}