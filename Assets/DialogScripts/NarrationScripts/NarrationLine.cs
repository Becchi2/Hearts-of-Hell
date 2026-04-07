using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NarrationLines", menuName = "NarrationLines")]
[System.Serializable]
public class NarrationLines : ScriptableObject
{
    public List<string> lines;// contains narration line
    public string line = "";
    
    public string SetLine(int num)
    {
        line = lines[num];
        return line;
    }

    public int LineAmmount()
    {
        return lines.Count;
    }


}
