using System.Collections.Generic;
using System.Linq;
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
        public List<Collectable> m_secondaryPieces;
        
        
        /// <summary>
        /// Function overload from IInteract \n
        /// In this class it is used for adding the collectible object to the player
        /// </summary>
        /// <param name="_player">Reference to player's PlayerInteract script</param>
        public void Execute(PlayerInteract _player)
        {
            // // Debug.Log("Collect");
            GetComponent<AudioSource>()?.Play();
            if (m_secondaryPieces.Count == 0)
            {
                _player.Collect(m_collectableObject);
                Tutorial.m_objectivePicked = true;
            }
            else
            {
                foreach (var secondaryPiece in m_secondaryPieces)
                {
                    secondaryPiece.m_secondaryPieces.Remove(this);
                }
            }
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