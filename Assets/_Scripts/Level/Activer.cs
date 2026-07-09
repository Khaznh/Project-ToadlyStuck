using UnityEngine;
using UnityEngine.Events;

public class Activer : MonoBehaviour
{
    public UnityEvent<Collider2D, Activer> OnTriggerEventEnter;
    public UnityEvent<Collider2D, Activer> OnTriggerEventExit;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEventEnter.Invoke(collision, this);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerEventExit.Invoke(collision, this);
    }
}
