namespace Himanshu
{
    
    /// <summary>
    /// Wrapper class for collectable object
    /// Is used for saving system because of being serializable
    /// </summary>
    [System.Serializable]
    public class CollectableObjectWrapper
    {
        public string m_objectName;

        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_objectName">Name of the object</param>
        public CollectableObjectWrapper(string _objectName)
        {
            m_objectName = _objectName;
        }
    }
}