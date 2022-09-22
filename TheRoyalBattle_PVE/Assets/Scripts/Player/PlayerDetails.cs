using System;
[Serializable]
public struct PlayerDetails
{
    public string userName;
    public string emailAddress;
    public string password;
    public int avatarID;

    public PlayerDetails(int def)
    {
        userName = string.Empty;
        emailAddress = string.Empty;
        password = string.Empty;
        avatarID = -1;
    }
}

