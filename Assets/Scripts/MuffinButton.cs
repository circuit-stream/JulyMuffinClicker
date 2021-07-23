using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class MuffinButton : MonoBehaviour
{
    public AudioClip[] muffinClickSounds;
    public float muffinAnimationDuration = 0.2f;

    private AudioClip lastRandomSound;
    private Tweener buttonTween;
    private AudioSource audioSource;
    private Game game;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        game = FindObjectOfType<Game>();
    }

    public void OnMuffinClicked()
    {
        // Let the game know a muffin was clicked
        game.OnMuffinsCollected();

        // Play a sound
        PlayMuffinSound();

        // Animate the muffin
        AnimateMuffin();
    }

    private void AnimateMuffin()
    {
        // Punch the scale of the muffin
        if(buttonTween != null)
        {
            buttonTween.Rewind();
        }
        buttonTween = transform.DOPunchScale(Vector3.one * 0.1f, muffinAnimationDuration);
    }

    private void PlayMuffinSound()
    {
        // Make sure there are enough muffin sounds
        if(muffinClickSounds.Length <= 1)
        {
            Debug.LogError("Not enough muffin click sounds were provided, there must be at least 2");
            return;
        }

        // Choose a random sound to play (making sure it's not the last sound we played
        AudioClip randomSound;
        do
        {
            randomSound = muffinClickSounds[Random.Range(0, muffinClickSounds.Length)];
        } while (randomSound == lastRandomSound);

        lastRandomSound = randomSound;

        // Play the random sound
        audioSource.PlayOneShot(randomSound);
    }
}
