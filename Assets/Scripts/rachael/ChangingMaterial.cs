using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class ChangingMaterial : MonoBehaviour
    {
        [FormerlySerializedAs("ChangeEnvir")] public ChangeFurniture m_changeEnvir;
        [FormerlySerializedAs("mat")] public Material[] m_mat;
        Renderer m_rend;
        // Start is called before the first frame update
        void Start()
        {
            m_rend = GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if(m_changeEnvir.index < m_mat.Length)
                m_rend.sharedMaterial = m_mat[m_changeEnvir.index];
        }
    }
}
