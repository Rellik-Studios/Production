using System.Collections.Generic;
using rachael.FavorSystem;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class ResetTransform : MonoBehaviour
    {
        [SerializeField] private List<Animator> m_animators;
        private static ResetTransform instance;
        private void Start()
        {
            instance = this;
        }
        
        public static bool ResetT()
        {
            instance.m_animators.ForEach(animator => animator.SetTrigger("ResetT"));
            return true;
        }
        
    }
}
