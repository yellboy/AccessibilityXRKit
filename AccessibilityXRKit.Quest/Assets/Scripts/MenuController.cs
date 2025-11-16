using Unity.XR.CoreUtils;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject simulationsContainer;
    
    [Header("Menu Canvas (prevents being occluded by world-space UI)")]
    [SerializeField]
    private Canvas menuCanvas;                     // assign the menu Canvas in Inspector
    [SerializeField]
    private int menuSortingOrder = 1000;           // higher = renders on top
    [SerializeField]
    private bool forceScreenSpaceOverlay = false;  // optional: force Overlay mode (use with care in XR)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ensure the menu canvas renders on top of the blur / tunnel canvases.
        if (menuCanvas != null)
        {
            // For world-space canvases, enable override sorting and bump the sorting order
            menuCanvas.overrideSorting = true;
            menuCanvas.sortingOrder = menuSortingOrder;

            // If you want the menu to be independent of world-space geometry,
            // optionally switch to ScreenSpace-Overlay. Not always desired in XR,
            // so controlled by inspector flag.
            if (forceScreenSpaceOverlay)
            {
                menuCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SimulationChosen(GameObject simulation)
    {
        print("Simulation chosen: " + simulation.name);
        
        foreach (Transform child in simulationsContainer.transform)
        {
            child.gameObject.SetActive(false);
        }

        simulation.SetActive(true);
    }
}
