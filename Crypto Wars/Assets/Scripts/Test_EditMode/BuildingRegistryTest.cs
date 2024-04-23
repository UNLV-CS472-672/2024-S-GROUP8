using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BuildingRegistryTest
{
    // Setup the CardRegistry and BuildingRegistry before each test
    [SetUp]
    public void Setup()
    {
        // Clear previous data and setup cards
        CardRegistry.Load();

        // Load buildings after setting up the CardRegistry
        BuildingRegistry.Load();
    }

    [Test]
    public void Load_BuildingsCorrectlyInitialized()
    {
        // Check if buildings are loaded correctly
        Building pythonFactory = BuildingRegistry.GetBuildingByName("Python Factory");
        Building javaJunction = BuildingRegistry.GetBuildingByName("Java Junction");
        Building cWorkshop = BuildingRegistry.GetBuildingByName("C Workshop");

        Assert.IsNotNull(pythonFactory);
        Assert.IsNotNull(javaJunction);
        Assert.IsNotNull(cWorkshop);
    }

    [Test]
    public void GetBuildingByName_ReturnsCorrectBuildingWithCorrectProperties()
    {
        // Verify the properties of the Python Factory
        Building building = BuildingRegistry.GetBuildingByName("Python Factory");
        Assert.AreEqual("Python Factory", building.GetName());
        Assert.AreEqual(1, building.GetAmount());
        Assert.AreEqual(3, building.GetTimeToProduce());

        // Ensure the associated card is correct and its properties
        Card card = building.GetCard();
        Assert.AreEqual("Python", card.GetName());
        Assert.AreEqual(5, card.getOffense());
        Assert.AreEqual(55, card.getDefense());
        Assert.AreEqual(20, card.getStaminaCost());
    }

    [Test]
    public void GetBuildingByIndex_ReturnsCorrectBuildingWithCorrectProperties()
    {
        // Verify the properties of the building retrieved by index, for instance, the first building
        Building building = BuildingRegistry.GetBuildingByIndex(0);
        Assert.AreEqual("Python Factory", building.GetName());
        Assert.AreEqual(1, building.GetAmount());
        Assert.AreEqual(3, building.GetTimeToProduce());

        // Check associated card properties
        Card card = building.GetCard();
        Assert.AreEqual("Python", card.GetName());
        Assert.AreEqual(5, card.getOffense());
        Assert.AreEqual(55, card.getDefense());
        Assert.AreEqual(20, card.getStaminaCost());
    }
}
