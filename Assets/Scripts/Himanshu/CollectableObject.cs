using System;
using UnityEngine;

namespace Himanshu
{
   
    /// <summary>
    /// Class for scriptable object.
    /// Handles clock pieces like face, gong etc
    /// </summary>
    [CreateAssetMenu(menuName = "ScrictableObjects/Collectable", fileName = "CollectableObject")]
    public class CollectableObject : ScriptableObject
    {
        
        public string m_objectName;

        
        /// <summary>
        /// Unity overload event
        /// In this class initialises the wrapper for the collectable object
        /// </summary>
        private void OnEnable()
        {
            m_wrapper = new CollectableObjectWrapper(m_objectName);
        }

        public CollectableObjectWrapper m_wrapper;
    }
}