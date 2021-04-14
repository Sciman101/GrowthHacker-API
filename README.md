# GUModding

This is an early attempt at a modding API for Going Under, built on BepInEx. Currently, the API...
- Allows access to the debug menu
- Disables steam achievements
- Allows you to add custom skills through SkillAPI

To install, start by [Installing BepInEx](https://bepinex.github.io/bepinex_docs/v5.0/articles/user_guide/installation.html), then drop `GUAPI.dll` into BepInEx/plugins.

There's an included [example skill](https://github.com/Sciman101/GUModding/blob/main/ExampleSkill/ExampleSkillPlugin.cs) to show how to add your own skills. 

## Making mods
To set up a development environment for making your own skills, create a new project in Visual Studio. It will be a C# Class Library (.NET Standard)
Now, you will need to add the references for your project! Note; You must have installed BepInEx and run the game at least once.

Go to `Project` -> `Add Reference` -> select `Browse`

Navigate to the bepinex folder of your going under installation. Go through the `core` folder and add each item.

Then go to the plugins folder and add `GUAPI.dll`

Then, do the same for the main game assemblies: these are in your going under folder: `Going Under\Going Under_Data\Managed` In there, select 'UnityEngine', 'UnityEngine.CoreModule', 'UnityEngine.AssetBundleModule.dll' (for asset loading) and 'Assembly-CSharp', click "Add"

Finally, click "OK", this will apply your references (May take quite a while, be patient.)

The example mod outlines how everything works, but feel free to hmu in the [Aggro Crab Discord](https://discord.com/invite/aggrocrab) if you need more help (Sciman101#1100). I copy pasted most of this from the R2API modding guide, I'm real tired.
