using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform rootPanel;

    [SerializeField] private GameObject titlePrefab;
    [SerializeField] private GameObject headerPrefab;
    [SerializeField] private GameObject dataPrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateTable();
    }

    public void CreateTable()
    {
        foreach(Transform child in rootPanel.transform)
        {
            Destroy(child.gameObject);
        }

        //Reading Json
        JsonData jsonData = JsonController.ReadJsonFile();

        //Setting Title
        Text title = Instantiate(titlePrefab, rootPanel.transform).GetComponent<Text>();
        
        if (jsonData == null)
        {
            title.text = "File is missing!\n Please add JsonChallenge.json to StreamingAssets folder";
            return;
        }

        title.text = jsonData.title;

        //Setting Header
        GameObject headerRoot = new GameObject("HeaderRoot", typeof(HorizontalLayoutGroup));
        headerRoot.transform.SetParent(rootPanel, false);

        for (int x = 0; x < jsonData.columnHeaders.Count; x++)
        {
            Text headerTitle = Instantiate(headerPrefab, headerRoot.transform).GetComponent<Text>();
            headerTitle.text = jsonData.columnHeaders[x];
        }

        //Setting Lines
        foreach (Dictionary<string, string> data in jsonData.dataList)
        {
            GameObject lineRoot = new GameObject("LineRoot", typeof(HorizontalLayoutGroup));
            lineRoot.transform.SetParent(rootPanel, false);

            foreach (KeyValuePair<string, string> line in data)
            {
                Text dataText = Instantiate(dataPrefab, lineRoot.transform).GetComponent<Text>();
                dataText.text = line.Value;
            }
        }
    }
}
