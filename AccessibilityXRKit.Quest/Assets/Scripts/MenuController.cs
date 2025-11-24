using Unity.XR.CoreUtils;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject simulationsContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        gameObject.SetActive(false);
    }
}
