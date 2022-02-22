using Himanshu;
using rachael.SaveSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class BookEntry : MonoBehaviour, IInteract
    {
        [FormerlySerializedAs("eraChanging")] public ChangeFurniture m_eraChanging;
        //public PlayerSave m_playerFile;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void Execute(PlayerInteract _player)
        {
            Debug.Log("Save Entry Point completed");
            GameObject tempPlayer = _player.gameObject;
            if (tempPlayer.GetComponent<PlayerSave>() != null)
            {
                // saves the player data into the system
                tempPlayer.GetComponent<PlayerSave>().SavePlayer();
                //_player.SaveProcess.SetTrigger("Save");
                //Debug.Log(eraChanging.Rooms[eraChanging.Index].name);
                //
                //DefineRoom(m_eraChanging.m_rooms[m_eraChanging.index].name);
            }
        }

        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Save");        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log("Contact");
        //    if (other.CompareTag("Player"))
        //    {
        //        if (other.GetComponentInParent<PlayerSave>() != null)
        //        {
        //            //saves the player data into the system
        //            other.GetComponentInParent<PlayerSave>().SavePlayer();
        //            //Debug.Log(eraChanging.Rooms[eraChanging.Index].name);
        //            DefineRoom(eraChanging.Rooms[eraChanging.Index].name);

        //        }
        //    }
        //}


        public void DefineRoom(string _roomName)
        {
            switch (_roomName)
            {
                case "Loop_gear":
                    PlayerPrefs.SetInt("Loop_Gear", PlayerPrefs.GetInt("ClockIndex"));
                    break;
                case "Loop_face":
                    PlayerPrefs.SetInt("Loop_Face", PlayerPrefs.GetInt("ClockIndex"));
                    break;
                case "Loop_mouth":
                    PlayerPrefs.SetInt("Loop_Mouth", PlayerPrefs.GetInt("ClockIndex"));
                    break;
                case "Loop_hand":

                    PlayerPrefs.SetInt("Loop_Hand", PlayerPrefs.GetInt("ClockIndex"));
                    break;
            }
        }
    }
}
