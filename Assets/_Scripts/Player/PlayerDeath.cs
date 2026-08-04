using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject playerCorpes;

    [SerializeField] private AudioClip ouchSound;
    [SerializeField] private AudioChannelSO sfx;

    private GameObject corpse;

    public void Die()
    {
        if (corpse != null)
        {
            Destroy(corpse);
        }
        sfx.PlaySound(ouchSound);
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
