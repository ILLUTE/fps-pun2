using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using System;

public static class SaveDetails
{
    public static void SaveData(PlayerDetails details)
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, "playerDetails.json");

            string playerDetails = JsonConvert.SerializeObject(details);

            File.WriteAllText(path, playerDetails);
        }
        catch (Exception e)
        {

        }
    }

    public static PlayerDetails LoadData()
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, "playerDetails.json");

            if (File.Exists(path))
            {
                string playerDetails = File.ReadAllText(path);

                return JsonConvert.DeserializeObject<PlayerDetails>(playerDetails);
            }
        }
        catch (Exception e)
        {

        }

        return new PlayerDetails(0);
    }
}
