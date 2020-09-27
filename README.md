# CustomPNGToolKit
a custom image toolkit that i made because i needed one; in this version it can only resize image files (individual file) and image sequence (directory)

# in depth description
This tool aim at easing mass resize of image sequence by using a resize ratio. It is using the GDI+ library to load transform and save the files. This software can be multithreaded
speeding up the whole process, by default the thread count is 1 but it can be modified in the arguments. Later it would be able to perform image filters and watermarks; also
i wish to add a multiple ratio thingy to resize an image for icon creation.

# How to
The software comes in a cli form and can be called from any terminal. Actually the software is capable of running on Windows, mac and linux (including arm distributions). To use it
simply copy the executable in a folder and open the terminal. Navigate through the folders and locate the folder where you copied CustomPNGToolKit within your terminal/cmd. Then
type CustomPNGToolKit (or CustomPNGToolKit.exe on windows) and add your the arguments corresponding to your project. You can find a detailed list of available actions as following
or by simply launching the executable:
```
  (everything is case sensitive for the moment, i'll fix that later)
  - Resize <type> <input path> <output path> <resize ratio> [<thread count> <watermark activation>]
    the type is either a 'file' or a 'folder' (without ' symbole)
    the input path of the file/folder
    the ouput path of your file/folder
    the thread count or how many files will be treated at the same time (1 by default)
    the watermark activation or if the software display a watermark somewhere (false by default but it is not implemented yet)
    
    so an exemple: CustomPNGToolKit.exe Resize folder "input/" "output/" 0.1 4 false
    will resize every image file in the input folder by a ratio of 0.1 and will ouput result 4 image at a time in the output folder.
```

# Licensing

MIT classic license

# thanks
well i thank you for your time and you engagement, i hope it will serve you as well as possible.
