using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Himanshu
{
    public class LoseScreenGif : MonoBehaviour
    {
        [SerializeField] private List<Sprite> m_sprites;

        IEnumerator Start()
        {
            while (m_sprites.Count > 2)
            {
                GetComponent<Image>().sprite = m_sprites[0];
                m_sprites.RemoveAt(0);
                yield return new WaitForSeconds(0.25f);
            }
        }

        // Update is called once per frame
        void Update()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
