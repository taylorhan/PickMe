using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class FoxDoll : Doll
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
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            bool isDead = CheckIsDead(Object_Manager_Script.eDoll.EBear, objManagerScript.FoxList, this.gameObject, col.gameObject);
        }
    }

}