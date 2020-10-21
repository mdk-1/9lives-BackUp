using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//script to change sprites assigned to animator component of character prefab based on player selection in character selection panel of main menu

public class PlayerSpriteManager : MonoBehaviour
{
    // The name of the sprite sheet to use 
    public string useSpriteSheet;
    // The name of the currently loaded sprite sheet
    private string useLoadedSpriteSheet;
    // The dictionary containing all the sliced up sprites in the sprite sheet
    private Dictionary<string, Sprite> spriteSheet;
    // reference to sprite renderer component
    private SpriteRenderer spriteRenderer;

    //call the static variable holding players selected cat from main menu and pass it into local variable
    void Awake()
    {  
        useSpriteSheet = MenuHandler.catName; 
        Debug.Log(MenuHandler.catName); //testing purposes
    }
    // Get and cache the sprite renderer for the character prefab
    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        this.LoadSpriteSheet();
    }

    // LateUpdate runs after the animation is loaded
    private void LateUpdate()
    {
        // Check if the sprite sheet name has changed
        // Load the new sprite sheet
        if (this.useLoadedSpriteSheet != this.useSpriteSheet)
        {           
            this.LoadSpriteSheet();
        }

        // Swap out the sprite to be rendered by its name -- this means the name of the sprite must be the same (first run frame called run1 across ALL sprite sheets).
        this.spriteRenderer.sprite = this.spriteSheet[this.spriteRenderer.sprite.name];
    }

    // Loads the sprites from a sprite sheet
    private void LoadSpriteSheet()
    {
        // Load the sprites from a sprite sheet file - must be in 'resources' directory
        // var is like a wildcard in this case, it will determine the type of variable upon compilation 
        var sprites = Resources.LoadAll<Sprite>(this.useSpriteSheet);
        this.spriteSheet = sprites.ToDictionary(x => x.name, x => x);

        // Remember the name of the sprite sheet in case it is changed later on in the game
        this.useLoadedSpriteSheet = this.useSpriteSheet;
    }
}
