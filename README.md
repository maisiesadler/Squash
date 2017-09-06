# Squash

Squash parses a folder of cucumber files and produces an equivalent structure of html files.


Example usage:
```
var squashConfiguration = new SquashConfiguration(title: "Title",
                                    inputDirectory: "C:\tests",
                                    outputDirectory: "C:\outputDirectory");

squasher.Squash(squashConfiguration);
```
