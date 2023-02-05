using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akari
{
    public class AudioManager : MonoBehaviour
    {
        #region singleton
        private static AudioManager instance;
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<AudioManager>();
                }
                return instance;
            }
        }
        #endregion

        [SerializeField] private AudioSource audioSourceBGM;
        [SerializeField] private AudioSource audioSourceSFX;

        [SerializeField] private AudioClip[] sfxClips;

        private void Awake()
        {
            #region singleton
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            #endregion
        }

        public void PlayBGM()
        {
            audioSourceBGM.Play();
        }

        public void StopBGM()
        {
            audioSourceBGM.Stop();
        }

        public void PlaySFX(string sfxName)
        {
            int index = -1;
            for (int i = 0; i < sfxClips.Length; i++)
            {
                if (sfxClips[i].name == sfxName)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                audioSourceSFX.PlayOneShot(sfxClips[index]);
            }
        }
    }
}
