using System.Collections.Generic;
using System.Linq;
using Himanshu;
using JetBrains.Annotations;
using UnityEngine.Serialization;

namespace rachael.SaveSystem
{
    [System.Serializable]
    public class PlayerData
    {
        //public Dictionary<CollectableObject, Wrapper<int>> m_inventory;
        public List<CollectableObjectWrapper> m_depositedToTheClock;
        public List<CollectableObjectWrapper> m_inventory;

        public List<string> m_oneShotPlayed;

        public bool? m_bookTutorialPlayed = null;
        public bool? m_objTutorialPlayed = null;
        
        
        [CanBeNull] public string m_userName;
        
        [FormerlySerializedAs("Index")] public int m_index; //the number which index for each time era (for change furniture)
        //public int Death;

        [FormerlySerializedAs("hasPiece")] public bool m_hasPiece;

        [FormerlySerializedAs("position")] public float[] m_position;

        [FormerlySerializedAs("rotation")] public float[] m_rotation;

        private bool? m_endTutorialPlayed;
        //public string[] Loopnames;

        public PlayerData(PlayerSave _player)
        {
            m_bookTutorialPlayed = gameManager.Instance.m_bookTutorialPlayed;
            m_endTutorialPlayed = gameManager.Instance.m_endTutorialPlayed;
            m_objTutorialPlayed = gameManager.Instance.m_objTutorialPlayed;
            //Death = player.Death;
            m_userName = NarratorScript.UserName;
            m_depositedToTheClock = _player.m_depositedToTheClock.Select(t=>t.m_wrapper).ToList();

            m_oneShotPlayed = OneTimeText.alreadyUsed;
            m_inventory = _player.m_inventory.Keys.Select(t => t.m_wrapper).ToList();
            m_index = _player.m_index;

            m_position = new float[3];
            m_rotation = new float[3];

            m_position[0] = _player.transform.position.x;
            m_position[1] = _player.transform.position.y;
            m_position[2] = _player.transform.position.z;


            m_rotation[0] = _player.transform.rotation.eulerAngles.x;
            m_rotation[1] = _player.transform.rotation.eulerAngles.y;
            m_rotation[2] = _player.transform.rotation.eulerAngles.z;
        }
    }
}