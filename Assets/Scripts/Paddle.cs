using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float _lifespan = 5f;

    private void Start()
    {
        Destroy(gameObject, _lifespan);
    }

    void Update()
    {
        // Follow the mouse
        transform.position = Input.mousePosition;
    }
}
