﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image memoryImagePrefab;
    public Sprite memoryUsedSprite;
    private static UIManager instance;
    private List<Image> memoryImages;

    void Start()
    {
        memoryImages = new List<Image>();
        if (!instance)
        {
            instance = this;
            print("UI Manager Script");
        }
        else
        {
            Destroy(this.gameObject);
            print("Destroyed duplicate");
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    GameManager GetGameManager()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            return gameManager.GetComponent<GameManager>();
        }
        return null;
    }

    public void SetMemories(int memories)
    {
        for (int i = 0; i < memoryImages.Count; ++i)
        {
            Destroy(memoryImages[i].gameObject);
        }
        memoryImages.Clear();

        for (int i = 0; i < memories; ++i)
        {
            memoryImages.Add(Instantiate(memoryImagePrefab, new Vector3((i * 80) - (80/2 * (memories-1)), -60, 0), Quaternion.identity) as Image);
            memoryImages[i].transform.SetParent(gameObject.transform, false);
        }
    }

    public void SetActiveMemories(int _iActiveMemories)
    {
        for (int i = 0; i < _iActiveMemories; i++)
        {
            memoryImages[i].sprite = memoryImagePrefab.sprite;
        }
        for (int j = memoryImages.Count; j > _iActiveMemories; j--)
        {
            memoryImages[j - 1].sprite = memoryUsedSprite;
        }
    }
}