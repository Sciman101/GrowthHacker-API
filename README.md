# Going Under API

This is an early attempt at a modding API for Going Under, built on BepInEx. Currently, the API...
- Allows access to the debug menu
- Disables steam achievements
- Allows you to add custom skills through SkillAPI
- Allows you to add custom costumes for Jackie

To install, start by [Installing BepInEx](https://bepinex.github.io/bepinex_docs/v5.0/articles/user_guide/installation.html), then drop `GUAPI.dll` into BepInEx/plugins.

There's an included [example skill](https://github.com/Sciman101/GUModding/blob/main/ExampleSkill/ExampleSkillPlugin.cs) to show how to add your own skills.

**NOTE: IT IS HIGHLY RECCOMENDED YOU MAKE BACKUPS OF YOUR SAVE FILE BEFORE INSTALLING.**

Save files can be found at `C:\Users\[USER]\AppData\LocalLow\Aggro Crab\Going Under` (Or just put run `%appdata%\..\LocalLow\Aggro Crab\Going Under`)

## Making mods
To set up a development environment for making your own skills, create a new project in Visual Studio. It will be a C# Class Library (.NET Standard)
Now, you will need to add the references for your project! Note; You must have installed BepInEx and run the game at least once.

Go to `Project` -> `Add Reference` -> select `Browse`

Navigate to the bepinex folder of your going under installation. Go through the `core` folder and add each item.

Then go to the plugins folder and add `GUAPI.dll`

Then, do the same for the main game assemblies: these are in your going under folder: `Going Under\Going Under_Data\Managed` In there, select 'UnityEngine', 'UnityEngine.CoreModule', 'UnityEngine.AssetBundleModule.dll' (for asset loading) and 'Assembly-CSharp', click "Add"

Finally, click "OK", this will apply your references (May take quite a while, be patient.)

The example mod outlines how everything works, but feel free to hmu in the [Aggro Crab Discord](https://discord.com/invite/aggrocrab) if you need more help (Sciman101#1100). I copy pasted most of this from the R2API modding guide, I'm real tired.

## Adding Skills
To add a skill, you need 3 things
- An icon for the skill in the HUD (set to 50 pixels per unit in Unity import settings).
- A GameObject/Prefab to represent the model for the skill in the world.
- A custom ModdedSkill class. You _must_ use ModdedSkill and not Skill, as ModdedSkill provides additional functionality for the API.

Afterwards, adding the skill just requires calling

    SkillAPI.createSkill<YourSkillClass>(
            "Name of Skill",
            "Skill Description",
            skillIcon,
            skillModel,
            skillRarity);

Where skillRarity is an enum provided by Going Under. Take a look at `ExampleSkill.cs` to see more on how skill classes work.

## Adding Costumes
To add a costume, you just need a model to work with. Currently, the example skill assetbundle comes with 3 models ripped from the game.
To create the costume, you just define an instance of ModdedCostume, providing the details you want. Then, add it with the provided function.

    ModdedCostume joblinCostume = new ModdedCostume
    {
        nameIndex = "JOBLIN_COSTUME",
        mesh = joblinMesh,
        unlockRequirement = Costume.UnlockRequirement.None,
        material = GlobalSettings.defaults.jackieCostumes[0].material
    };
    GoingUnderApiPlugin.AddCostume(joblinCostume);
    LocalizedText.mainTable.Add("JOBLIN_COSTUME", new List<string> { "Joblin Time :)" });

Of note are 2 things: Firstly, after or before adding the costume you need to add a localization key so the name of the costume will show up properly.  

Secondly, is the unlock requirement - There are 3 default options for this:
- `None`, meaning the costume is unlocked by default
- `GetRelic`, which requires beating one of the dungeons for the first time, specified by `relicRequired`
- `BeatOvertimeLevel`, which requires beating a level of Overtime, specified by `overtimeLevelRequired`

If you want your costume to have unique unlock paramaters, ModdedCostume has the property `checkUnlockedFunction`. By passing a function which takes no parameters and returns a bool, you can override the default unlock behaviour with whatever you want. The Jelly costume in `ExampleSkillPlugin.cs` uses this.
