using rachael;
using UnityEngine;

namespace Himanshu
{
    /// <summary>
    /// Collectable: This class is for the collectable objects
    /// </summary>
    public class Collectable : MonoBehaviour, IInteract
    {
        public string objectName => m_collectableObject.m_objectName;
        public string m_pathName;
        public CollectableObject m_collectableObject;    
        
        
        /// <summary>
        /// Function overload from IInteract \n
        /// In this class it is used for adding the collectible object to the player
        /// </summary>
        /// <param name="_player">Reference to player's PlayerInteract script</param>
        public void Execute(PlayerInteract _player)
        {
            Debug.Log("Collect");
            GetComponent<AudioSource>()?.Play();
            _player.Collect(m_collectableObject);
            Tutorial.m_objectivePicked = true;
            //GetComponent<MeshCollider>().enabled = false;
            
            this.Invoke(()=> { Destroy(this.gameObject); }, 0.1f);
        }

        /// <summary>
        /// Function overload from IInteract \n
        /// In this class modifies the indicator to show the pickup UI
        /// </summary>
        /// <param name="_raycast">Reference to the Raycast script of main camera</param>
        public void CanExecute(Raycast _raycast)
        {
            if (_raycast.m_indication != null)
                _raycast.m_indication.sprite = Resources.Load<Sprite>("Pickup");
        }
    }
}