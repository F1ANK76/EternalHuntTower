using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public Collider2D colliderUp;
    public Collider2D colliderDown;
    public Collider2D colliderLeft;
    public Collider2D colliderRight;

    public void EnableHitbox(string direction)
    {
        DisableAll(); // 항상 먼저 모든 콜라이더 끔

        switch (direction)
        {
            case "Up": colliderUp.enabled = true; break;
            case "Down": colliderDown.enabled = true; break;
            case "Left": colliderLeft.enabled = true; break;
            case "Right": colliderRight.enabled = true; break;
        }
    }

    public void DisableAll()
    {
        colliderUp.enabled = false;
        colliderDown.enabled = false;
        colliderLeft.enabled = false;
        colliderRight.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("sword hit: " + other.name);
    }
}