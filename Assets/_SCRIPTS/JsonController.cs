using SimpleJSON;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class JsonController : MonoBehaviour
{
    private static string filePath = Application.dataPath + "/StreamingAssets/JsonChallenge.json";

    public static JsonData ReadJsonFile()
    {
        if (!File.Exists(filePath))
            return null;
        
        string jsonString = File.ReadAllText(filePath);

        //JsonRoot jsonData = JsonUtility.FromJson<JsonRoot>(jsonString);
        JSONNode json = JSON.Parse(jsonString);

        JsonData jsonRoot = new JsonData();
        jsonRoot.title = json["Title"];
        jsonRoot.columnHeaders = new List<string>();
        jsonRoot.dataList = new List<Dictionary<string, string>>();

        for (int x = 0; x < json["ColumnHeaders"].AsArray.Count; x++)
        {
            jsonRoot.columnHeaders.Add(json["ColumnHeaders"][x]);
        }

        for (int x = 0; x < json["Data"].AsArray.Count; x++)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            for (int y = 0; y < json["ColumnHeaders"].AsArray.Count; y++)
            {
                data.Add(jsonRoot.columnHeaders[y], json["Data"][x][jsonRoot.columnHeaders[y]]);
            }
            jsonRoot.dataList.Add(data);
        }

        return jsonRoot;
    }

}
