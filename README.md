# TEXT2PO
TEXT2PO - A simple converter for the text from the games Lord of Magna and Rune Factory 4 by Darkmet98.
## Functions
* Export the '.nxtxt' and '.eng' files in Po for the profesional CAT Tools.
* Import the .po translated to the original file.
* Import the translated files to po with the original english translation.
## Usage 
* TEXT2PO "mode" "file1" "file2"
### Mode for Rune Factory 4:
* -exportrune (export to po)
* -importrune (import po)
* -transrune(import the translation from another file)
* -exportrunefix(export and fix bad newlines from another programs)
### Mode for Lord Of Magna Maiden Heaven:
* -exportlord (export to po)
* -importlord (import po)
* -translord(import the translation from another file)
* -exportlordfix(export and fix bad newlines from another programs)
### Examples 
* Example 1: TEXT2PO.exe -exportlord msg.nxtxt
* Example 2: TEXT2PO.exe -importlord msg.nxtxt.po
* Example 3: TEXT2PO.exe -translord msg.nxtxt.po msgESP.nxtxt
* Example 4: TEXT2PO.exe -exportlordfix msgESP.nxtxt
## Changelog
### Pre-release
* Initial version
### 1.0
* Added the import option for Lord of Magna and Rune Factory 4.
* Bug fixes.
### 1.1
* Updated Yarhl libraries.
* Bug fixes.
### 1.2
* Fix bad Lord Of Magna import from po.
* Refactored the code and now import and export more fastest.
* New modes:
** New method for import old translations from another programs.
** New alternate export algorithm for fix bad newlines from another programs.
## Massive thanks to:
* Pleonex (For all and Yarhl libraries)
* Artuvazro
* Megaflan
