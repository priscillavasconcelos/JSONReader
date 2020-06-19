using System;
using System.Collections.Generic;

[Serializable]
public class JsonData
{
    public string title;
    public List<string> columnHeaders;
    public List<Dictionary<string, string>> dataList;
}
