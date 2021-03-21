# ReShade-Centralized

<img align="center" src="https://imgur.com/tB6AB6L.jpg" alt="ReShade Centralized">

ReShade Centralized is an installer (made in C# and .NET) meant to replace the official installer for users who like using one reshade-shaders folder for all games, or otherwise strive to centralize their reshade files.  This means presets, screenshots, shader files, and reshade dlls are stored in one location, and the reshade.ini that gets generated points to these locations.  This installer also deploys the reshade dlls via symlinks, eliminating the need to update your reshade dlls for every single game when a new version drops.  Update once in this installer and you're good to go.  Time to basically say this in list form.

## Features:
- Install ReShade and deploy .dlls and config files as symlinks (this is why the program needs admin privileges)
- Supports installing official ReShade dll files or modified ones (any modified builds are self provided)
- Download/Update a central shader respository and create all necessary preset and screenshot folders
- Generate reshade.ini file based on the ReShade Centralized folder you set.
- Update existing ReShade Centralized installs with new paths
- Installs ReShade to Microsoft Store applications via the injector

## Planned Improvements:
- Expand shader download functionality to include updating only pre-existing shaders.
- Add more customization for the options in the reshade.ini file
- Add full vulkan support (currently requires the global install from the normal reshade installer)
- Any other feasible user requests

## About Microsoft Store Injection
Unlike the wrapper method that reshade normally uses, most games from the Microsoft Store will only work with reshade through memory injection at run time.  While this usually won't be a problem, this method is significantly more likely to cause problems with anti cheat.  It's highly advised to not use this method for multiplayer games.  Also, if a game is memory protected in some way, the injection is likely to fail.  Most Microsoft first party games use memory protection in some capacity, especially the games that predate their move to Steam.  Most third party releases on game pass should work, provided no anti cheat is in use.

## Required Reading
- First off, any ReShade related issues encountered with this installer should not be reported to Crosire unless replicated with a clean install of the normal installer.  This installer installs the official DLLs straight from the reshade website, but generates the reshade.ini itself.  Future updates to reshade may require a new reshade.ini to be created/updated for full feature functionality.

- **This is pre-release software and it will have bugs.**  While I have worked hard to prevent the possibility of deleting uninteded data, I will not be held responsible for any loss of data that could result from the use of this software, as incredibly unlikely as it may be.

## Donation Link
<a href="https://www.buymeacoffee.com/oopydoopy" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" style="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>
