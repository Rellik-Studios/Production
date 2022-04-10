using rachael;
using rachael.FavorSystem;
using UnityEngine;
using Himanshu;
public class LockObject : MonoBehaviour, IInteract
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Execute(PlayerInteract _player)
    {
        return;
    }

    public void CanExecute(Raycast _raycast)
    {
        if(_raycast.m_indication != null)
            _raycast.m_indication.sprite = Resources.Load<Sprite>("locked");
    }
}
