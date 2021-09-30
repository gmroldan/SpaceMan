using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager INSTANCE;

    public List<LevelBlock> allLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();
    public Transform levelStartPosition;

    void Awake() 
    {
        if (INSTANCE == null)
            INSTANCE = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLevelBlock()
    {
        LevelBlock block;
        Vector3 spawnPosition = Vector3.zero;

        if (this.currentLevelBlocks.Count == 0)
        {
            block = Instantiate(this.allLevelBlocks[0]);
            spawnPosition = this.levelStartPosition.position;
        }
        else
        {
            var randomIdx = Random.Range(0, this.allLevelBlocks.Count);
            block = Instantiate(this.allLevelBlocks[randomIdx]);
            spawnPosition = this.currentLevelBlocks[this.currentLevelBlocks.Count - 1].exitPoint.position;

        }

        block.transform.SetParent(this.transform, false);

        Vector3 correctionVector = new Vector3(
            spawnPosition.x - block.startPoint.position.x,
            spawnPosition.y - block.startPoint.position.y,
            0
        );

        block.transform.position = correctionVector;

        this.currentLevelBlocks.Add(block);
    }

    public void RemoveLevelBlock()
    {
        var oldBlock = this.currentLevelBlocks[0];
        this.currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    public void RemoveAllBlocks()
    {
        while (this.currentLevelBlocks.Count > 0)
            this.RemoveLevelBlock();
    }

    public void GenerateInitialBlocks()
    {
        for (int i = 0; i < 2; i++)
        {
            AddLevelBlock();
        }
    }
}
