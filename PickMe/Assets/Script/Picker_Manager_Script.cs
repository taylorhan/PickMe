using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class Picker_Manager_Script : MonoBehaviour
    {

		public GameObject sound;

        public enum ePickerState
        {
            Move_Horizontal = 0,
            Move_Down,
            Move_Up,
            Pick,
        }
        public ePickerState pickerState = ePickerState.Move_Horizontal;

        //Val for Movement
        private float H_Margin = 470;
        private float V_Init = 250;
        private float V_TopMargin = 200;
        private float V_BotMargin = -140;
        private int H_Dir = 1;

        //Val for State
        private float DownPeriod = 2;
	    private float elapsedTime = 0f;
        private bool IsPickUp = false;
        private float UpPeriod = 0.5f;

        [System.Serializable]
        public class PickerSetting
        {
            public float speed = 10;
            public float minDownPeriod = 2;
            public float maxDownPeriod = 5;
        }
        public PickerSetting pickerSetting;

        bool Check = false;

        //Val for Down
        float DestPosY = 0;

        void Start ()
        {
            //Init Vals
            SetPickerState(ePickerState.Move_Horizontal);

            SetRandomDownPeriod();
            DestPosY = V_BotMargin;

            //Init Pos
            InitPos();
        }

        void Update ()
        {
            //좌우로 계속 이동
            MoveHorizontal();

            //만약 내려갈 조건(시간 등)이 되었다면 좌우이동 멈추고 아래로 내려가기
            CheckTimeToDown();
            MoveDown();

            //픽업 상태로 변경 & 내려간 위치에서 콜라이더 Enable
            SetPickUp();

            //콜라이더에서 가장 먼저 접촉한 캐릭터 무적 설정 + Parent 바꿔서 들기
            CheckTimeToUp();

            //초기 높이로 올라오기
            MoveUp();

            //천장 축까지 올라오면 캐릭터 사라지고 점수 ++

            //집게 초기화


        }

        private void FixedUpdate()
        {
            //if (IsPickUp == true)
            //{
            //    Debug.Log("Picker::FixedUpdate - Off PickUp Flag");
            //    IsPickUp = false;
            //    SetPickerState(ePickerState.Move_Up);
            //}
        }

        void SetPickerState(ePickerState state)
        {
            pickerState = state;
        }
        
        void InitPos()
        {
            Vector3 curLocalPos = transform.localPosition;
            float ranPosX = Random.Range(-H_Margin, H_Margin);
            transform.localPosition = new Vector3(ranPosX, V_Init, curLocalPos.z);
            H_Dir = (ranPosX < 0) ? H_Dir : -H_Dir;
        }

        void MoveHorizontal()
        {
            if (pickerState != ePickerState.Move_Horizontal)
                return;

            Vector3 curLocalPos = transform.localPosition;

            float posX = curLocalPos.x + (H_Dir * pickerSetting.speed);

            if (-H_Margin <= curLocalPos.x && curLocalPos.x <= H_Margin)
            {
                
            }
            else if (curLocalPos.x > H_Margin && H_Dir == 1)
            {
                posX = H_Margin;
                H_Dir = -1;
            }
            else if (transform.localPosition.x < -H_Margin && H_Dir == -1)
            {
                posX = -H_Margin;
                H_Dir = 1;
            }
            transform.localPosition = new Vector3(posX, curLocalPos.y, curLocalPos.z);
        }

        void CheckTimeToDown()
        {
            if (pickerState != ePickerState.Move_Horizontal)
                return;

            elapsedTime += Time.deltaTime;
            if (elapsedTime > DownPeriod)
            {
                elapsedTime = 0;
                SetPickerState(ePickerState.Move_Down);
                DestPosY = Random.Range(V_BotMargin, V_TopMargin);
            }
        }

        void SetRandomDownPeriod()
        {
            DownPeriod = Random.Range(pickerSetting.minDownPeriod, pickerSetting.maxDownPeriod);
        }

        void MoveDown()
        {
            if (pickerState != ePickerState.Move_Down)
                return;

			//sound.GetComponent<SoundManager> ().pickup.Play ();
            Vector3 curLocalPos = transform.localPosition;

            float posY = curLocalPos.y + ((-1) * pickerSetting.speed);
            if (posY < DestPosY)
            {
                posY = DestPosY;
                SetPickerState(ePickerState.Pick);
                //SetPickerState(ePickerState.Move_Up);
            }
            transform.localPosition = new Vector3(curLocalPos.x, posY, curLocalPos.z);
        }

        void SetPickUp()
        {
            if (pickerState != ePickerState.Pick)
                return;

            IsPickUp = true;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (IsPickUp == false || GameManagerScript.Instance.inGameVars.isGameOver == true)
                return;

            Debug.Log("Picker::OnTriggerEnter2D - PickUp Flag (true)");

            if (col.gameObject.tag == "Doll")
            {
                Debug.Log("Picker::OnTriggerEnter2D - PickUp Flag (true)");
                col.gameObject.GetComponent<Doll>().SetDollState(Doll.eDollState.Pickup);
                col.transform.parent = transform;
            }
        }

        void CheckTimeToUp()
        {
            if (pickerState != ePickerState.Pick)
                return;

            elapsedTime += Time.deltaTime;
            if (elapsedTime > UpPeriod)
            {
                elapsedTime = 0;
                IsPickUp = false;
                SetPickerState(ePickerState.Move_Up);
            }
        }

        void MoveUp()
        {
            if (pickerState != ePickerState.Move_Up)
                return;

            Vector3 curLocalPos = transform.localPosition;

            float posY = curLocalPos.y + pickerSetting.speed;
            if (posY > V_Init)
            {
                posY = V_Init;
                ExchangeDollToPoint();
                SetPickerState(ePickerState.Move_Horizontal);
            }
            transform.localPosition = new Vector3(curLocalPos.x, posY, curLocalPos.z);
        }

        void ExchangeDollToPoint()
        {
            List<GameObject> childList = new List<GameObject>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform tChild = transform.GetChild(i);
                if (tChild.tag == "Doll")
                {
                    childList.Add(tChild.gameObject);
                }
            }

            foreach (GameObject childObj in childList)
            {
                int DollTypeIndex = childObj.GetComponent<Doll>().Type;

                int ranScore = Random.Range(100, 1000);
                bool isEnemy = (int)Object_Manager_Script.eDoll.EBear <= DollTypeIndex && DollTypeIndex <= (int)Object_Manager_Script.eDoll.EFox;
                if (isEnemy == true)
                {
                    GameManagerScript.Instance.SubScore(Mathf.FloorToInt(ranScore * 0.2f));
                }
                else
                {
                    GameManagerScript.Instance.AddScore(ranScore);
                }
                

                List<GameObject> list = InGameManager.Instance.objManagerScript.GetDollTypeList(DollTypeIndex);
                InGameManager.Instance.objManagerScript.Delete_Obj(list, childObj);
            }
        }
    }
}
