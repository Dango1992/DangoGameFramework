using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class Proto2CS : MonoBehaviour
{
    
    [MenuItem("Tools/生成ProtoBuffCS", false, 301)]
    public static void GenProtoBuff()
    {
        Process.Start(Application.dataPath+@"/../Tools/protogen/protogen.bat");
    }
}
