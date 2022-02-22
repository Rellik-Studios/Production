using UnityEngine;

namespace Himanshu
{
    
    /// <summary>
    /// Enemy Animation Handler : Stores a reference to and provides easy to access properties for enemy animations
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationHandler : MonoBehaviour
    {
        private Animator m_animator;
        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }
    }
}