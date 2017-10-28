﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class EBearDoll : Doll
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
			if (Check_Super == true) {
				Timer += Time.deltaTime;
				if (Timer > Limit_Time) {
					invincible = false;
					Check_Super = false;
				}
			}
        }

        void OnTriggerEnter2D(Collider2D col)
        {
			if (invincible == false && col.GetComponent<Doll>().invincible == false) {
				bool isDead = CheckIsDead (Object_Manager_Script.eDoll.Rabbit, objManagerScript.EBearList, this.gameObject, col.gameObject);
			}
        }
    }

}
