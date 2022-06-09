using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Himanshu
{
    public class Commands : MonoBehaviour
    {
        [SerializeField] private HidingSpot m_room1Table;
        [SerializeField] private GameObject m_room1Camera;
        [SerializeField] private HidingSpot m_room2Table;
        [SerializeField] private GameObject m_room2Camera;
        
        [SerializeField] private HidingSpot m_room3Table;
        public void Room1Command(EnemyController _enemy)
        {
            StartCoroutine(eRoom1Command(_enemy));
        }

        private IEnumerator eRoom1Command(EnemyController _enemy)
        {
            m_room1Camera.SetActive(true);
            FindObjectOfType<CharacterController>().enabled = false;
            yield return new WaitForSeconds(2f);
            var agent = _enemy.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = 2f;
            agent.SetDestination(m_room1Table.transform.position);
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(2f);
            while (agent.remainingDistance > agent.stoppingDistance)
                yield return null;
            
            // Debug.Log(agent.remainingDistance);
            //m_room1Table.Infect();
            yield return new WaitForSeconds(3.5f);
            
            m_room1Camera.SetActive(false);
            yield return new WaitForSeconds(2f);

            _enemy.m_commandFinished = true;
            FindObjectOfType<CharacterController>().enabled = true;
        }
        
        
        public void Room2Command(EnemyController _enemy)
        {
            StartCoroutine(eRoom2Command(_enemy));
        }

        private IEnumerator eRoom2Command(EnemyController _enemy)
        {
            m_room2Camera.SetActive(true);
            FindObjectOfType<CharacterController>().enabled = false;

            yield return new WaitForSeconds(2f);
            var position = _enemy.transform.position;
            var rotation = _enemy.transform.rotation;
            
            var agent = _enemy.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = 2f;
            agent.SetDestination(m_room2Table.transform.position);
            yield return new WaitForEndOfFrame();
            yield return new WaitWhile(() => agent.remainingDistance >= agent.stoppingDistance);
            //m_room2Table.Infect();
            yield return new WaitForSeconds(3.5f);
            
            agent.SetDestination(position);
            yield return new WaitForEndOfFrame();
            yield return new WaitWhile(() => agent.remainingDistance >= agent.stoppingDistance);
            agent.transform.rotation = rotation;
            _enemy.m_commandFinished = true;

            m_room2Camera.SetActive(false);

            yield return new WaitForSeconds(3.5f);
            FindObjectOfType<CharacterController>().enabled = true;
        }
        
        public void Room3Command(EnemyController _enemy)
        {
            StartCoroutine(eRoom3Command(_enemy));
        }

        private IEnumerator eRoom3Command(EnemyController _enemy)
        {

            m_room1Camera.SetActive(true);
            FindObjectOfType<CharacterController>().enabled = false;

            yield return new WaitForSeconds(2f);
            var position = _enemy.transform.position;
            var rotation = _enemy.transform.rotation;
            
            var agent = _enemy.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = 2f;
            agent.SetDestination(m_room3Table.transform.position);
            yield return new WaitForEndOfFrame();
            yield return new WaitWhile(() => agent.remainingDistance >= agent.stoppingDistance);
            //m_room3Table.Infect();
            yield return new WaitForSeconds(3.5f);
            
            agent.SetDestination(position);
            yield return new WaitForEndOfFrame();
            yield return new WaitWhile(() => agent.remainingDistance >= agent.stoppingDistance);
            agent.transform.rotation = rotation;
            _enemy.m_commandFinished = true;

            m_room1Camera.SetActive(false);

            yield return new WaitForSeconds(3.5f);
            FindObjectOfType<CharacterController>().enabled = true;

            yield return null;
        }
        
    }
}
