using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Texture2DStorage
{
    private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

    // Initialize the texture storage with the ContentManager
    public void LoadContent(ContentManager content)
    {
        //Example: Load your enemy textures
        _textures["DeadlyDaisy"] = content.Load<Texture2D>(@"EnemyTextures\DeadlyDaisySprite");
        _textures["MurderousMushroom"] = content.Load<Texture2D>(@"EnemyTextures\MurderousMushroomSprite");
        _textures["TerribleTulip"] = content.Load<Texture2D>(@"EnemyTextures\TerribleTulipSprite");
        _textures["ToothyTerror"] = content.Load<Texture2D>(@"EnemyTextures\ToothyTerrorSprite");
        _textures["BothersomeBlueberry"] = content.Load<Texture2D>(@"EnemyTextures\BothersomeBlueberrySprite");
        _textures["AggravatingAcorn"] = content.Load<Texture2D>(@"EnemyTextures\AggravatingAcornSprite");
        _textures["AcornMaker"] = content.Load<Texture2D>(@"EnemyTextures\AcornMakerSprite");
        //_textures["Seed"] = content.Load<Texture2D>(@"EnemyTextures\lobber_seed_0001");
        _textures["PurpleSpore"] = content.Load<Texture2D>(@"EnemyTextures\mushroom_poison_cloud_0001");
        _textures["PinkSpore"] = content.Load<Texture2D>(@"EnemyTextures\mushroom_poison_cloud_pink_0003");

        // Block/Obstacle Textures
        _textures["TreeStump"] = content.Load<Texture2D>(@"BlockTextures\ForestStumps");
        _textures["FallenLog"] = content.Load<Texture2D>(@"BlockTextures\ForestBackground-6");
        _textures["PlatformMd"] = content.Load<Texture2D>(@"BlockTextures\ForestBackground-2");
        _textures["PlatformLg"] = content.Load<Texture2D>(@"BlockTextures\ForestBackground-1");
        _textures["FloatingPlatformSm"] = content.Load<Texture2D>(@"BlockTextures\ForestBackground-5");
        _textures["FloatingPlatformLg"] = content.Load<Texture2D>(@"BlockTextures\ForestBackground-5");

        //PlayerAnimationTextures
        _textures["PlayerJump"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerJump");
        _textures["PlayerRunNormal"] = content.Load<Texture2D>(@"PlayerAnimationTextures\PlayerRunNormal");


        // Add more textures as needed
    }

    // Method to retrieve a texture
    public Texture2D GetTexture(string textureName)
    {
        if (_textures.ContainsKey(textureName))
            return _textures[textureName];

        return null; // Handle missing textures if necessary
    }
}
