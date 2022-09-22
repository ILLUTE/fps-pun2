using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarHandler : MonoBehaviour
{
    [SerializeField]
    private Sprite[] avatars = new Sprite[4];

    [SerializeField]
    private Sprite currentSprite;

    [SerializeField]
    private Image currentImage;

    [SerializeField]
    private Image[] avatar_Options = new Image[3];

    private PlayerDetails playerDetails;

    private List<Sprite> m_UpdatedAvatars = new List<Sprite>(3);

    private void Awake()
    {
        ProfileHandler.OnLoadData += AvatarLoaded;
    }

    private void AvatarLoaded()
    {
        playerDetails = ProfileHandler.Instance.PlayerDetails;

        if (playerDetails.avatarID == -1)
        {
            playerDetails.avatarID = 0;
        }

        currentSprite = avatars[0];

        LoadCurrentAvatar();

        UpdateAvailableAvatars();
    }

    public void OnSelectFromChoices(Image selectedImage)
    {
        currentSprite = selectedImage.sprite;

        LoadCurrentAvatar();

        UpdateAvailableAvatars();
    }

    private void UpdateAvailableAvatars()
    {
        m_UpdatedAvatars.Clear();

        for (int i = 0; i < avatars.Length; i++)
        {
            if (avatars[i].name.Equals(currentSprite.name))
            {
                continue;
            }

            m_UpdatedAvatars.Add(avatars[i]);
        }

        for (int i = 0; i < m_UpdatedAvatars.Count; i++)
        {
            avatar_Options[i].sprite = m_UpdatedAvatars[i];
        }
    }

    public void LoadCurrentAvatar()
    {
        currentImage.sprite = currentSprite;

        string[] split = currentSprite.name.Split('_');

        int avatarId = -1;

        int.TryParse(split[1], out avatarId);

        if(avatarId != -1)
        {
            ProfileHandler.Instance.SaveAvatarInfo(avatarId);
        }
    }
}