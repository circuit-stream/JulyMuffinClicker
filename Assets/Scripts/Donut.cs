using UnityEngine;

public class Donut : MonoBehaviour
{
    [SerializeField] private int _startingScale;
    [SerializeField] private float _lifespan;
    
    private float _age;
    private float _muffinCountdown;
    [SerializeField] private float _delayBetweenMuffins = 0.5f;
    private Game _game;
    [SerializeField] private int _muffinsPerDelayPerLevel;
    public int _level;

    private void Awake()
    {
        _game = FindObjectOfType<Game>();
    }

    private void Start()
    {
        // Start at the starting scale
        SetScale(_startingScale);
    }

    private void SetScale(float scale)
    {
        // Set the size of the image
        transform.localScale = Vector2.one * scale;
    }

    void Update()
    {
        _muffinCountdown -= Time.deltaTime;
        if (_muffinCountdown <= 0f)
        {
            _muffinCountdown = _delayBetweenMuffins;
            _game.AwardDonutMuffins(_muffinsPerDelayPerLevel * _level);
        }
        
        // Increase the age of the donut
        _age += Time.deltaTime;
        
        // Scale the size of the donut based on its age
        SetScale(Mathf.Lerp(_startingScale, 0, _age / _lifespan));
        
        // Destroy the donut after it's too old
        if (_age > _lifespan)
        {
            Destroy(gameObject);
        }
    }
}
