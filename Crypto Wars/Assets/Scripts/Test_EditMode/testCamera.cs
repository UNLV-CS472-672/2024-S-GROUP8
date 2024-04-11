using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class testCameraScript
{
    private CameraScript cameraScript;
    private GameObject cameraGameObject;

    // Create a new game object
    // also add the CameraScript to said object
    [SetUp]
    public void SetUp()
    {
        cameraGameObject = new GameObject();
        cameraScript = cameraGameObject.AddComponent<CameraScript>();

        /*******************************************************
         * Set cameraScript variables to match CameraScript.cs
         *******************************************************/
        cameraScript.cameraSpeed = 3f;
        cameraScript.zoomSpeed = 1.0f;
        cameraScript.minZoom = 5.5f;
        cameraScript.maxZoom = 20f;

        // Camera default position coordinates
        cameraScript.defaultX = 2.5f;
        cameraScript.defaultZ = 7.46f; // deals with zoom in/out
        cameraScript.defaultY = -4f;

        // Camera default zoom position
        cameraScript.zoomAmount = 0f;
        cameraScript.newYPos = 7.46f;
}

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(cameraGameObject);
    }

    /******************************************************
     *
     * TEST moveCamera up/down/left/right
     * 
     *******************************************************/

    [Test]
    public void testmoveCameraUp()
    {
        cameraScript.moveCamera(KeyCode.W, KeyCode.None, KeyCode.None, KeyCode.None);
        Vector3 expectedPosition = new Vector3(0, 0* cameraScript.cameraSpeed * Time.deltaTime, 0);
        Assert.IsTrue((cameraGameObject.transform.position - expectedPosition).magnitude < 0.0001f);
    }

    [Test]
    public void testmoveCameraDown()
    {
        cameraScript.moveCamera(KeyCode.None, KeyCode.S, KeyCode.None, KeyCode.None);
        Vector3 expectedPosition = new Vector3(0, 0* cameraScript.cameraSpeed * Time.deltaTime, 0);
        Assert.IsTrue((cameraGameObject.transform.position - expectedPosition).magnitude < 0.0001f);
    }

    [Test]
    public void testmoveCameraLeft()
    {
        cameraScript.moveCamera(KeyCode.None, KeyCode.None, KeyCode.A, KeyCode.None);
        Vector3 expectedPosition = new Vector3(0* cameraScript.cameraSpeed * Time.deltaTime, 0, 0);
        Assert.IsTrue((cameraGameObject.transform.position - expectedPosition).magnitude < 0.0001f);
    }

    [Test]
    public void testmoveCameraRight()
    {
        cameraScript.moveCamera(KeyCode.W, KeyCode.None, KeyCode.None, KeyCode.D);
        Vector3 expectedPosition = new Vector3(0* cameraScript.cameraSpeed * Time.deltaTime, 0, 0);
        Assert.IsTrue((cameraGameObject.transform.position - expectedPosition).magnitude < 0.0001f);
    }

    /******************************************************
    *
    * TEST resetCamera
    * 
    *******************************************************/

    [Test]
    public void testResetCamera()
    {
        cameraScript.resetCamera(KeyCode.Space);
        Assert.AreEqual(new Vector3(0, 0, 0), cameraGameObject.transform.position);
    }

    /******************************************************
     *
     * TEST zoomCamera in/out
     * 
     *******************************************************/

    [Test]
    public void testZoomCameraIn()
    {
        float scrollDelta = 1.0f;
        float newYPos = Mathf.Clamp(cameraGameObject.transform.position.y + -scrollDelta * cameraScript.zoomSpeed, cameraScript.minZoom, cameraScript.maxZoom);
        cameraScript.zoomCamera(scrollDelta);
        Assert.AreEqual(new Vector3(0, newYPos, 0), cameraGameObject.transform.position);
    }

    [Test]
    public void testZoomCameraOut()
    {
        float scrollDelta = -1.0f;
        float newYPos = Mathf.Clamp(cameraGameObject.transform.position.y + -scrollDelta * cameraScript.zoomSpeed, cameraScript.minZoom, cameraScript.maxZoom);
        cameraScript.zoomCamera(scrollDelta);
        Assert.AreEqual(new Vector3(0, newYPos, 0), cameraGameObject.transform.position);
    }

    /******************************************************
     *
     * TEST setCameraPosition using a dummy tile
     * 
     *******************************************************/
    // ai-gen start (ChatGPT-3.5, human intervention level 1):
    [Test]
    public void testSetCameraPosition()
    {
        // Arrange
        Tile tile = testTileDummy(new Vector2(10, 5)); // Create a mock tile
        Vector3 expectedPosition = new Vector3(0, cameraScript.defaultZ, 0); // Expected camera position after setting to tile position

        cameraScript.setCameraPosition(tile);

        Assert.AreEqual(expectedPosition, cameraGameObject.transform.position);
    }

    private Tile testTileDummy(Vector2 position)
    {
        // Create a GameObject and add Tile component to mock a tile
        GameObject tileGameObject = new GameObject("Tile");
        Tile tile = tileGameObject.AddComponent<Tile>();
        tile.BoardXPos = (int)position.x;
        tile.BoardYPos = (int)position.y;
        return tile;
    }
    // ai-gen end
}

