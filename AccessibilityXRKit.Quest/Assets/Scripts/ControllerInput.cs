using UnityEngine;

public class ControllerInput : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private GameObject simulationsContainer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menu.activeInHierarchy)
        {
            return;
        }

        if (OVRInput.GetDown(OVRInput.Button.Any))
        {
            menu.SetActive(true);
            foreach (Transform child in simulationsContainer.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
