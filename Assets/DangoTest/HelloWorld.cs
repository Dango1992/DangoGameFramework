using UnityEngine;
using System.Collections;
using System.IO;
using ILRuntime.Runtime.Enviorment;
 
public class HelloWorld : MonoBehaviour
{
    ILRuntime.Runtime.Enviorment.AppDomain appdomain;
    void Start()
    {
        StartCoroutine(LoadILRuntime());
    }

    IEnumerator LoadILRuntime()
    {
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
#if UNITY_ANDROID
    WWW www = new WWW(Application.streamingAssetsPath + "/HotfixProject.dll");
#else
        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotfixProject.dll");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            Debug.LogError(www.error);
        byte[] dll = www.bytes;
        www.Dispose();
#if UNITY_ANDROID
    www = new WWW(Application.streamingAssetsPath + "/HotfixProject.pdb");
#else
        www = new WWW("file:///" + Application.streamingAssetsPath + "/HotfixProject.pdb");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            Debug.LogError(www.error);
        byte[] pdb = www.bytes;
        System.IO.MemoryStream fs = new MemoryStream(dll);
        System.IO.MemoryStream p = new MemoryStream(pdb);
        appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());    
    
        OnILRuntimeInitialized();
    }

    void OnILRuntimeInitialized()
    {
        //appdomain.Invoke("HotfixProject.MyClass", "SayHello", null, null);
    }
}