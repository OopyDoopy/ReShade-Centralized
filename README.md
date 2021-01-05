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
- Whittle away at potential errors and handle them

## Required Reading
- First off, any ReShade related issues encountered with this installer should not be reported to Crosire unless replicated with a clean install of the normal installer.  This installer installs the official DLLs straight from the reshade website, but generates the reshade.ini itself.  Future updates to reshade may require a new reshade.ini to be created/updated for full feature functionality.

- **This is pre-release software and it will have bugs.**  This software is heavily IO focused and creates/deletes folders and files regularly.  With each release, I do my best to ensure that only intended files/directories get deleted, but proceed with caution.
