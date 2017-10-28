using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class BearDoll : Doll
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
			if (this.GetComponent<Doll> ().Check_Super) {
				this.GetComponent<Doll> ().MakeSuper (true, this.gameObject);
			} else {
				this.GetComponent<Doll> ().MakeSuper (false, this.gameObject);
			}
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            bool isDead = CheckIsDead(Object_Manager_Script.eDoll.ERabbit, objManagerScript.BearList, this.gameObject, col.gameObject);
        }
    }
}