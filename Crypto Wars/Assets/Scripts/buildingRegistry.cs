using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingRegistry
{
    // List of building types and grabs the card they produce from the card registry
    private static List<Building> buildingList = new List<Building>();

    // Initialized after cardRegistry since CreateBuilding relies on it
    public static void Load() 
    {
        // Create buildings that produces the cards
        CreateBuilding("Python Factory", "Python", 1, 3);
        CreateBuilding("Java Junction", "Java", 2, 5);
        CreateBuilding("C Workshop", "C", 1, 3);
    }

    // Creates the bulding types and adds them to buildingList
    private static void CreateBuilding(string name, string cardName, int numProduced, int turnsToProduce)
    {
        Building newBuild = new Building(name, numProduced, turnsToProduce);
        Card cardProduct = CardRegistry.GetCardByName(cardName);  
        newBuild.SetCard(cardProduct);
        buildingList.Add(newBuild);
    }

    // Grabs building from the list by name
    public static Building GetBuildingByName(string buildingName)
    {
        Building building = buildingList.Find(build => build.GetName() == buildingName);
        Building newBuilding = new Building(building.GetName(), building.GetAmount(), building.GetTimeToProduce());
        newBuilding.SetCard(building.GetCard());
        return newBuilding;
    }

    // Grabs building from the list by index
    public static Building GetBuildingByIndex(int index)
    {
        Building newBuilding = new Building(buildingList[index].GetName(), buildingList[index].GetAmount(), buildingList[index].GetTimeToProduce());
        newBuilding.SetCard(buildingList[index].GetCard());
        return newBuilding;
    }
}