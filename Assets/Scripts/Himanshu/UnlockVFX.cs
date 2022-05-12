using System.Collections;
using UnityEngine;
namespace Himanshu
{
    public class UnlockVFX : MonoBehaviour
    {
        private IEnumerator OnEnable()
        {
            yield return new WaitForSeconds(4f);
            this.gameObject.SetActive(false);
        }
    }
}
