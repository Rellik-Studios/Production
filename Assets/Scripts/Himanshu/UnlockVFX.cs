using System.Collections;
using UnityEngine;
namespace Himanshu
{
    public class UnlockVFX : MonoBehaviour
    {
        private void OnEnable()
        {
            this.Invoke(()=>gameObject.SetActive(false), 4f);
        }
    }
}
