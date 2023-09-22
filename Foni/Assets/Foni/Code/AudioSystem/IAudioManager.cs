using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foni.Code.AudioSystem
{
    public interface IAudioManager
    {
        IEnumerator PlayAudio(string channel, AudioClip audioClip, bool shouldOverride);
        void PlayAudioOneShot(string channel, AudioClip audioClip, bool shouldOverride);
        IEnumerator PlayMultipleQueued(string channel, List<AudioClip> audioClips);
        void Stop(string channel);
    }
}