# Foni

- [Foni](#foni)
  - [Git Setup](#git-setup)
    - [Unity Diff Tool](#unity-diff-tool)
  - [Unity Setup](#unity-setup)
  - [Third-Party Notices](#third-party-notices)
    - [Free Cartoon Trees Vectors](#free-cartoon-trees-vectors)
    - [Animal Avatars](#animal-avatars)
    - [Material Symbols by Google](#material-symbols-by-google)
    - [Inter Font by Rasmus Andersson](#inter-font-by-rasmus-andersson)
    - [Unity Asset Store - OpenWavParser](#unity-asset-store---openwavparser)

Foni is an educational app/game intended to help pre-school children learn
how to phonetically pronounce alphabetical letters by associating them with
simple objects, such fruit.

It is being developed as a test-pilot for a innovation project by Helsingborg
Stad in Sweden.

## Git Setup

This project uses [Git-LFS](https://git-lfs.com/) to track large binary files.
For MacOS you can instal it via brew using `brew install git-lfs`.

### Unity Diff Tool

A custom diff tool is specified in the `.gitconfig` file that is used to diff
Unity-specific files. This tool is located inside your Unity installation and
the file path may differ slightly if you're using a different Unity version.

Double-check that the `cmd` path for `unityyamlmerge` in the `.gitconfig` is
correct or alter the file for your specific Unity version to avoid issues.

For more info about this check out the
[Unity Smart Merge docs](https://docs.unity3d.com/Manual/SmartMerge.html).

## Unity Setup

This project is built using Unity and is intended to be compatible with the
latest Unity 2022 version. Unity editor versions are easily managed using
[Unity Hub](https://unity.com/unity-hub).

## Third-Party Notices

This project uses external/third-party assets.

### Free Cartoon Trees Vectors

- Usage: In-game tree image (modified)
- License: [Free Vector Standard License](https://www.freevector.com/standard-license)
- Link: https://www.freevector.com/free-cartoon-trees-vectors-18742

### Animal Avatars

- Usage: profile avatar images
- License: [Freepik](https://www.freepikcompany.com/legal#nav-freepik-license)
- Link: https://www.freepik.com/free-vector/animal-avatars_766618.htm, https://www.freepik.com/free-vector/variety-animal-avatars_766787.htm

### Material Symbols by Google

- Usage: Various in-game text-symbols
- License: [Apache 2.0](https://www.apache.org/licenses/LICENSE-2.0.html)
- Link: https://fonts.google.com/icons

### Inter Font by Rasmus Andersson

- Usage: main font
- License: [OFL](https://scripts.sil.org/cms/scripts/page.php?site_id=nrsi&id=OFL)
- Link: https://fonts.google.com/specimen/Inter/

### Unity Asset Store - OpenWavParser

- Usage: Parse .wav files into Unity Engine AudioClips
- Link: https://assetstore.unity.com/packages/tools/audio/open-wav-parser-90832
