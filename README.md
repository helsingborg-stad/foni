# Foni

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
