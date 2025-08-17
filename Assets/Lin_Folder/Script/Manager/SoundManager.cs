using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource effectSource;

    public AudioClip jamMenuMusic1;//0
    public AudioClip jamTitleScreenMusic;//0
    public AudioClip death1;
    public AudioClip death;
    public AudioClip menuUi;

    public AudioClip bugAttack;
    public AudioClip beetleTankAttack;
    public AudioClip mantisAttack;
    public AudioClip mothAttack;

    public AudioClip selectCard;//0

    public void EffectSound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }
}
