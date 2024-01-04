using UnityEngine;

public class Item : MonoBehaviour
{
    public virtual void DoSomething()
    {
        gameObject.SetActive(false);
    }
}
