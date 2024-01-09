using UnityEngine;
using UnityEngine.UI;

public class OpenWebsite : MonoBehaviour
{
    public string url = "https://www.google.com/";

    public void OpenLink()
    {
        Application.OpenURL(url);
    }
}
