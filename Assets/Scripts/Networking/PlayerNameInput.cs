using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class PlayerNameInput : MonoBehaviour
{
    private const string playerNamePrefKey = "PlayerName";
    // Start is called before the first frame update
    void Start()
    {
        string defaultName = "";
        var inputField = GetComponent<TMP_InputField>();
        if (inputField)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player name is null or empty");
            return;
        }

        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}
