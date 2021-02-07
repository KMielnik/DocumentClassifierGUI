# About

This program has been created as a helper for creating training data for: https://github.com/KMielnik/DocumentSectionClassifier, which is used to automatically mark common document elements as: 
* Printed text,
* Images/stamps,
* Written text/signatures.

Marking document relies on a neural network model, which needs training data marked by hand. This GUI let's user draw on loaded documents to create the data needed in an easy manner.

## Example of a marked document

![Example of a marked document](https://i.imgur.com/p8FSZ2A.png)

## Application GUI

![Application GUI](https://i.imgur.com/BpVwRGZ.png)

## Instruction

 1. Place documents in ```documents``` directory.
 1. Open application
    * LPM click or draw - place new point
    * PPM - delete last drawn point/section
    * Scroll - zoom in/out
    * Hold middle mouse button - move zoomed in document
    * Enter - finish drawing actual mask
    * Tab - generate mask for actual document
 1. Retrieve masks from ```masks``` directory.