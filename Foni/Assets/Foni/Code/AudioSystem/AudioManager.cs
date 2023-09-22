using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Foni.Code.AudioSystem
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        [SerializeField] private AudioSource channelPrefab;
        private Dictionary<string, AudioSource> _channelMap;
        private Dictionary<string, Coroutine> _channelQueues;

        private void Awake()
        {
            _channelMap = new Dictionary<string, AudioSource>();
            _channelQueues = new Dictionary<string, Coroutine>();
        }

        public IEnumerator PlayAudio(string channel, AudioClip audioClip, bool shouldOverride)
        {
            var source = GetOrAddSource(channel);

            if (source.isPlaying && !shouldOverride)
            {
                yield break;
            }

            source.Stop();

            source.PlayOneShot(audioClip);
            yield return new WaitWhile(() => source.isPlaying);
        }

        public void PlayAudioOneShot(string channel, AudioClip audioClip, bool shouldOverride)
        {
            StartCoroutine(PlayAudio(channel, audioClip, shouldOverride));
        }

        public IEnumerator PlayMultipleQueued(string channel, List<AudioClip> audioClips)
        {
            Stop(channel);
            var queue = StartCoroutine(DoPlayMultiple(GetOrAddSource(channel), audioClips));
            _channelQueues[channel] = queue;
            yield return queue;
        }

        private static IEnumerator DoPlayMultiple(AudioSource source, List<AudioClip> clips)
        {
            foreach (var clip in clips)
            {
                source.PlayOneShot(clip);
                yield return new WaitWhile(() => source.isPlaying);
            }
        }

        public void Stop(string channel)
        {
            var source = GetOrAddSource(channel);
            source.Stop();

            if (_channelQueues.TryGetValue(channel, out var queue))
            {
                StopCoroutine(queue);
            }
        }

        private AudioSource GetOrAddSource(string channel)
        {
            if (_channelMap.TryGetValue(channel, out var source))
            {
                return source;
            }

            var newChannel = Instantiate(channelPrefab, gameObject.transform);
            newChannel.name = $"_channel_{channel}";
            _channelMap.Add(channel, newChannel);
            return newChannel;
        }
    }
}