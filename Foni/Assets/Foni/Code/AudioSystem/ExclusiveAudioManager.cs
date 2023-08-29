using UnityEngine;

namespace Foni.Code.AudioSystem
{
    public class ExclusiveAudioManager
    {
        private readonly AudioSource _audioSource;

        public ExclusiveAudioManager(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void PlayIfCurrentlySilent(AudioClip clip)
        {
            if (_audioSource.isPlaying)
            {
                return;
            }

            _audioSource.PlayOneShot(clip);
        }

        public void PlayOverride(AudioClip clip)
        {
            _audioSource.Stop();
            _audioSource.PlayOneShot(clip);
        }
    }
}