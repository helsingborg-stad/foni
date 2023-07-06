using System.Collections;
using System.Collections.Generic;
using Foni.Code.AsyncSystem;
using Foni.Code.Core;
using Foni.Code.ProfileSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Foni.Code.UI
{
    public class ProfileMenuUI : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private RectTransform avatarContentRoot;

        [SerializeField] private DeleteProfileModalUI deleteProfileModalUI;

        [SerializeField] private AvatarWidget avatarPrefab;
        [SerializeField] private Transform addAvatarButtonTransform;

        [SerializeField] private AddProfileUI addProfileUI;
        [SerializeField] private PlayModalUI playModalUI;

        private void Start()
        {
            StartCoroutine(StartSetupAllProfiles());
            addProfileUI.OnSubmitProfile += OnSubmitNewProfile;
            playModalUI.OnStartGame += StartGame;
            deleteProfileModalUI.OnDeleteActiveProfile += SetupAllProfiles;
        }

        private void SetupAllProfiles()
        {
            StartCoroutine(StartSetupAllProfiles());
        }

        private static void StartGame()
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }

        private void OnSubmitNewProfile(ProfileData newProfile)
        {
            StartCoroutine(StartSubmitNewProfile(newProfile));
        }

        private IEnumerator StartSubmitNewProfile(ProfileData newProfile)
        {
            yield return new WaitForTask(() => Globals.ServiceLocator.ProfileService.UpdateProfile(newProfile));
            yield return StartSetupAllProfiles();
        }

        public void GoBackToStartMenu()
        {
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        }

        public void ShowAddNewProfileUI()
        {
            addProfileUI.Show();
        }

        private void ClearProfiles()
        {
            for (var i = 0; i < avatarContentRoot.childCount; i++)
            {
                var child = avatarContentRoot.GetChild(i);
                if (child != addAvatarButtonTransform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        private IEnumerator StartSetupAllProfiles()
        {
            ClearProfiles();

            List<ProfileData> profiles = null;
            yield return new WaitForTask<List<ProfileData>>(Globals.ServiceLocator.ProfileService.GetAllProfiles,
                newProfiles => profiles = newProfiles);

            foreach (var profile in profiles)
            {
                AddProfile(profile);
            }
        }

        private void AddProfile(ProfileData inProfile)
        {
            var newAvatar = Instantiate(avatarPrefab, avatarContentRoot);
            newAvatar.SetName(inProfile.name);
            newAvatar.SetAvatarSprite(Globals.AvatarIconMapper.GetSprite(inProfile.icon));
            newAvatar.OnClick += () => OnClickProfile(inProfile);

            addAvatarButtonTransform.SetAsLastSibling();
        }

        private void OnClickProfile(ProfileData profile)
        {
            Globals.ServiceLocator.ProfileService.SetActiveProfile(profile.name);
            playModalUI.SetFromProfile(profile);
            playModalUI.Show();
        }
    }
}