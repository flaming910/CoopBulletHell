using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("No player prefab");
        }
        else
        {
            if (!PlayerControl.LocalPlayerInstance)
            {
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 1, 0), Quaternion.identity);
            }
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " has entered the room");

        if (PhotonNetwork.IsMasterClient)
        {
            LoadArena();
        }
    }

    private void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Trying to load but not master client");
        }
        PhotonNetwork.LoadLevel("PrototypeScene");
    }

}
