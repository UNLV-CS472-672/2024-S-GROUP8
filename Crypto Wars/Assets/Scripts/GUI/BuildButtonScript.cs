using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.EventSystems;

public class BuildButtonScript : MonoBehaviour
{
    // public GameObject buildOptionsMenu;
    [SerializeField]
    private GameObject buildButton;
    [SerializeField]
    private GameObject buildingPrefab;
    [SerializeField]
    private Button choiceOne;
    [SerializeField]
    private Button choiceTwo;
    [SerializeField]
    private Button choiceThree;
    [SerializeField]
    public GameObject buildMenu;
    [SerializeField]
    private GameObject destroyButton;

    // public GameObject BuildMenu
    // {
    //     get => buildMenu;
    //     private set => buildMenu = value;
    // }

    // Public properties to expose the private fields
    public GameObject BuildButton
    {
        get => buildButton;
        private set => buildButton = value;
    }

    public GameObject DestroyButton
    {
        get => destroyButton;
        private set => destroyButton = value;
    }

    private static List<GameObject> buildingPrefabs;

    [SerializeField]
    private Material PurpleMaterial;
    [SerializeField]
    private Material YellowMaterial;
    [SerializeField]
    private Material GreenMaterial;

    // Start is called before the first frame update
    void Start()
    {
        buildingPrefabs = new List<GameObject>();
        DeactivateMain();
        DeactivateMenu();
        choiceOne.onClick.AddListener(CreateBuildingOptionOne);
        choiceTwo.onClick.AddListener(CreateBuildingOptionTwo);
        choiceThree.onClick.AddListener(CreateBuildingOptionThree);
        SetText(choiceOne.transform, 0);
        SetText(choiceTwo.transform, 1);
        SetText(choiceThree.transform, 2);
    }

    public void SetText(Transform form, int build)
    {
        form.Find("BuildingName").GetComponent<TextMeshProUGUI>().text = "Build " + BuildingRegistry.GetBuildingByIndex(build).GetName();
    }

    public void DeactivateMain()
    {
        buildButton.SetActive(false);
    }

    public void DeactivateMenu()
    {
        buildMenu.SetActive(false);
    }

    public void ActivateMenu()
    {
        buildMenu.SetActive(true);

        Vector2 pos = PlayerController.GetSelectedTile().GetTilePosition();
        buildMenu.transform.position = new Vector3(pos.x + 0.04f, 2.75f, pos.y);
        buildMenu.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
        buildMenu.transform.eulerAngles = new Vector3(90, 0, 0);
    }

    public void CreateBuildingOptionOne()
    {
        CreateBuildingOption(1);
    }
    public void CreateBuildingOptionTwo()
    {
        CreateBuildingOption(2);
    }
    public void CreateBuildingOptionThree()
    {
        CreateBuildingOption(3);
    }

    public void CreateBuildingOption(int option)
    {
        DeactivateMenu();
        Debug.Log(PlayerController.GetSelectedTile());
        InstantiatePrefab(PlayerController.GetSelectedTile(), option);
    }
    public Tile InstantiatePrefab(Tile tile, int option)
    {
        GameObject newBuilding = Instantiate(buildingPrefab, new Vector3(tile.transform.position.x - 0.2f, tile.transform.position.y + .5f, tile.transform.position.z + .1f), Quaternion.identity);
        newBuilding.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        if (option == 1)
        {
            newBuilding.GetComponent<Renderer>().material = YellowMaterial;
        }
        else if (option == 2)
        {
            newBuilding.GetComponent<Renderer>().material = GreenMaterial;
        }
        else if (option == 3)
        {
            newBuilding.GetComponent<Renderer>().material = PurpleMaterial;
        }

        buildingPrefabs.Add(newBuilding);

        Building building = BuildingRegistry.GetBuildingByIndex(option - 1);
        building.SetPosition(new Vector2(tile.GetTilePosition().x - 0.2f, tile.GetTilePosition().y + .1f));
        building.SetOwner(new Player(PlayerController.CurrentPlayer.GetName(), PlayerController.CurrentPlayer.GetColor()));
        tile.SetBuilding(building);

        Debug.Log("Creating a Building");
        return tile;
    }

    public void DeletePrefab(Tile tile)
    {
        Building toDelete = tile.GetBuilding();
        Vector2 pos = toDelete.GetPosition();
        GameObject prefab = FindBuildingPrefab(pos);

        if (prefab != null)
        {
            Destroy(prefab);
            buildingPrefabs.Remove(prefab);
            tile.SetBuilding(null);
        }

        destroyButton.SetActive(false);
        buildButton.SetActive(true);
    }

    public GameObject FindBuildingPrefab(Vector2 pos)
    {
        foreach (GameObject gameObject in buildingPrefabs)
        {
            if (pos.Equals(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z)))
            {
                return gameObject;
            }
        }
        return null;
    }

}