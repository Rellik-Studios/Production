using System.Collections;
using System.Collections.Generic;
using rachael.FavorSystem;
using UnityEngine;
namespace Himanshu.SmartObjective
{
    public class ResetTransform : MonoBehaviour
    {
        [SerializeField] private List<Animator> m_animators;
        private static ResetTransform instance;
        private bool m_hasReset;
        [SerializeField] private Objective m_objective;
        private FavorSystem m_favorSystem;
        private void Start()
        {
            m_favorSystem = FindObjectOfType<FavorSystem>();
            instance = this;
        }
        
        public static bool ResetT()
        {
            IEnumerator ResetCoroutine()
            {
                instance.m_favorSystem.consoleDisplay = ConsoleDisplay.customMenu;
                instance.m_favorSystem.m_commandText.text = gameManager.Instance.m_currentRoom == "Morden Bedroom"
                    ? instance.m_hasReset ? "No faulty transforms detected" : "Transforms Reset Complete"
                    : "No faulty transforms detected";
                yield return new WaitForSecondsRealtime(1f);

                instance.m_favorSystem.CloseCommandPrompt();
                yield return new WaitForSeconds(0.9f);
                if (gameManager.Instance.m_currentRoom == "Morden Bedroom" && !instance.m_hasReset)
                {
                    instance.m_animators.ForEach(animator => animator.SetTrigger("ResetT"));
                    instance.m_hasReset = true;
                    instance.m_objective.Execute(FindObjectOfType<PlayerInteract>());
                }
            }

            // if (gameManager.Instance.m_currentRoom != "Morden Bedroom" || instance.m_hasReset)
            //     return false;
            instance.StartCoroutine(ResetCoroutine());
            return true;
        }
    }
}
