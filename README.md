# FileTracker

This project will return you the below statistics in a csv file.

1. Folder Name,
2. Total Size ( GB ),
3. Modified < 1 Year Ago (GB),
4. Modified < 3 Years Ago (GB),
5. Modified < 5 Years Ago (GB),
6. No of Inaccessible Folders,
7. No of Files,
8. No of Folders


To run the console application you need two parameters i.e. the folder path and the target csv file path

# Syntax: 
GetFileStats <folder_path> <target_csv_file_path>

# For e.g 
GetFolderStats  "C:\Test" "C:\Output.csv"
