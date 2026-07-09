using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject playerCorpes;
    private GameObject corpse;

    public void Die()
    {
        if (corpse != null)
        {
            Destroy(corpse);
        }
        corpse = Instantiate(playerCorpes, PlayerController.Instance.transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        if (corpse != null)
        {
            Destroy(corpse);
        }
    }
}
