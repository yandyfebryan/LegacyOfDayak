using UnityEngine;
using UnityEngine.UI;

public class UIDisabler : MonoBehaviour
{
    public GameObject toBeDisabled;

    public void DisableAllUI()
    {
        toBeDisabled.gameObject.SetActive(false);
    }
}
