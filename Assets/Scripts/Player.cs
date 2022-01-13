using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health { get; private set; }

    private void Awake()
    {
        Health = 100;
    }
}
