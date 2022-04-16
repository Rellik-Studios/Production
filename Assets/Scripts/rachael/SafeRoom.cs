using Himanshu;
using rachael.SaveSystem;
using UnityEngine;

namespace rachael
{
    public class SafeRoom : MonoBehaviour
    {
        //temporary value until we fixed this on player
        bool m_isSafe = false;
    
        //when they enter safe room
        private void OnTriggerEnter(Collider _other)
        {
            if (_other.CompareTag("Player"))
            {
                if (!gameManager.Instance.m_bookTutorialPlayed ?? true)
                {
                    Tutorial.RunBookTutorial();
                }
                m_isSafe = true;
                if (_other.GetComponentInParent<RespawnManager>() != null)
                {
                    _other.GetComponentInParent<RespawnManager>().SetPosition(this.transform);
                    Debug.Log("You have entered the safe room");

                }
                if (_other.GetComponentInParent<PlayerSave>() != null)
                {
                    //saves the player data into the system
                    _other.GetComponentInParent<PlayerSave>().SavePlayer(transform);
                }
            }
        }
        //when they exit safe room
        private void OnTriggerExit(Collider _other)
        {
            if (_other.CompareTag("Player"))
            {
                m_isSafe = false;
                Debug.Log("You have exited the safe room");
            }
        }


        public void TeleportTo(float _rotationOffset)
        {
            var player = GameObject.FindObjectOfType<PlayerInteract>();
            var playerCam = FindObjectOfType<PlayerFollow>();
            if (player != null) {
                playerCam.m_mouseInput = false;
                playerCam.transform.rotation = Quaternion.Euler(playerCam.transform.rotation.eulerAngles.x, playerCam.transform.rotation.eulerAngles.y + _rotationOffset, playerCam.transform.rotation.eulerAngles.z);
                playerCam.ResetMouse();
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = this.transform.position;
                this.Invoke(()=> {
                    player.GetComponent<CharacterController>().enabled = true;
                    playerCam.m_mouseInput = true;
                }, 0.1f);
            }
        }
    }
}
