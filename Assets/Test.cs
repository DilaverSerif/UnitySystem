using _SYSTEMS_._Menu_System_;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(Mai_StartMenu.Berat.GetMenuItem<Button>().name);
        Debug.Log(Mai_StartMenu.StartButton.GetMenuItem<Button>().name);
        // Mai_StartMenu.GottenButton.GetMenuItem<Image>().sprite = null;
        // Mai_StartMenu.MucoButton.GetMenuItem<Button>().onClick.AddListener(()=> Debug.Log("Muco"));
        Mai_MenuContainers.StartMenu.GetMenu<Transform>().gameObject.SetActive(false);
    }
}
