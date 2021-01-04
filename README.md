# ReShade-Centralized

ReShade Centralized is an installer (made in C# and .NET) meant to replace the official installer for users who like using one shader repository, or otherwise centralizing their reshade files.  This means presets, screenshots, shader files, and reshade dlls are stored in one location, and the reshade.ini that gets generated points to these locations.  This installer also deploys the reshade dlls via symlinks, eliminating the need to update your reshade dlls for every single game when a new version drops.  Update once in this installer and you're good to go.  Time to basically say this in list form.

## Features:
- Install ReShade and deploy .dlls as symlinks (this is why the program needs admin privileges)
- Download/Update a central shader respository.
- Generate reshade.ini file based on the ReShade Centralized folder you set.

## Planned Improvements:
- Add UWP injection functionality, similar to the script I made in [ReShade-Tools](https://github.com/OopyDoopy/ReShade-Tools).
- Expand shader download functionality to include updating only pre-existing shaders.
- Create a UI for further customizing paths (currently can be done by editing ReShadeCentralized.ini).
- Generally improve UI appearance.
- Add better error handling and reduce chances of extreme edge case disaster.

## Warning
First off, any ReShade related issues encountered with this installer should not be reported to Crosire unless replicated with a clean install of the normal installer.  This installer installs the official DLLs straight from the reshade website, but generates the reshade.ini itself.  Future updates to reshade may require a new reshade.ini to be created/updated for full feature functionality.

This is pre-release software and it will have bugs.  This software is heavily IO focused and creates/deletes folders and files regularly.  It's unlikely that you'll use it in such a way that you create a disastrous scenario, but it can't be guaranteed.  Use with caution, you are very much a tester currently.  **Be very careful about customizing your directories!**

To be transparent about how this software deletes non-specific files, here is some pseudocode of the most dangerous delete it uses:

Below is a recursive delete intended to delete an existing FXShaders folder for replacement.  As long as your shader path isn't set to the middle of a folder that also contains an FXShaders folder filled with deeply personal documents, this is safe.
```
Delete <Shaders location from reshadecentralized.ini>\FXShaders Recursively
```
