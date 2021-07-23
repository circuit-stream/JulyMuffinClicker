using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float speed;
    public float fadeDuration;

    private float age;
    private TMP_Text text;
    private Color initialColour;

    private void Awake()
    {
        // Grab the text component
        text = GetComponent<TMP_Text>();
        initialColour = text.color;
    }

    void Update()
    {
        // Float the text upwards over time
        transform.Translate(0f, speed * Time.deltaTime, 0f);

        // Increase the age of the floating text
        age += Time.deltaTime;

        // Fade out the text over time
        text.color = Color.Lerp(initialColour, Color.clear, age / fadeDuration);

        // If the text is completely faded out, remove it
        if(age > fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}
