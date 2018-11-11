using UnityEngine;
using UnityEngine.UI;

public class UIPlayerFacingDirection : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Text lblFacingDirection;

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        lblFacingDirection.text = (player.transform.localScale.x > 0.0f) ? "Facing Direction : Right" : "Facing Direction : Left";
    }
}
