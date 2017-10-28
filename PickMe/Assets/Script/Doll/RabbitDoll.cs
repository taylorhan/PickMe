using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class RabbitDoll : Doll
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CheckState();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            bool isDead = CheckIsDead(Object_Manager_Script.eDoll.EFox, objManagerScript.RabbitList, this.gameObject, col.gameObject);
        }
    }

}
