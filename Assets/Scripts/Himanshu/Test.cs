using System;
using System.Collections.Generic;
using UnityEngine;

namespace Himanshu
{
    
    public class Test : MonoBehaviour
    {
        private Dictionary<string, Wrapper<int>> m_dictionary;

        
        private void Start()
        {
            m_dictionary = new Dictionary<string, Wrapper<int>>();

            
            //val = 1;
            m_dictionary.Add("try", new Wrapper<int>(1));

            if (m_dictionary.TryGetValue("try", out Wrapper<int> _count))
            {
                _count.value++;
            }

            if (m_dictionary.TryGetValue("try", out Wrapper<int> _count2))
            {
                // Debug.Log(_count2.value);
            }

        }
    }
}