# Muto Mark! - Pre Beta

A Mark Down Viewer for Windows

## Features

* Live file monitoring allows you to use your favorite editor and view the rendered result immediately
* Support for multiple stylesheets including [Git Hub Flavored Markdown]("http://github.github.com/github-flavored-markdown/" "Git Hub Flavored Markdown")

## Installation

* Clone the repo and run the installer found in the `dist` folder
* Alternatively you can use the [direct link](https://github.com/davidmuto/muto-mark/blob/master/dist/MutoMarkInstaller.exe?raw=true "Download MutoMark!")

## Usage

* Open MutoMark (you will be presented with an open file dialog)
* Open the markdown file you want to view
* Using your favorite editor, make changes to the markdown
* Watch the results change in real time

![MutoMark Screenshot](https://github.com/davidmuto/muto-mark/blob/master/samples/screenshot.png?raw=true)


You can open multiple documents using the Tray Icon (typically bottom right) or from the toolbar of an open window

## Contributing

* Fork the repo and make a pull request!

### Notes for Contributors

* In order to generate the installer (optional) you will need [Inno Setup](http://www.jrsoftware.org/isinfo.php, "Inno Setup")
* Installer is recreated on every release build (via post-build event and custom scripts)
