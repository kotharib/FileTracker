# FileTracker

This project will return you the below statistics in a csv file.

Folder Name,
Total Size ( GB ),
Modified < 1 Year Ago (GB),
Modified < 3 Years Ago (GB),
Modified < 5 Years Ago (GB),
No of Inaccessible Folders,
No of Files,
No of Folders


To run the console application you need two parameters i.e. the folder path and the target csv file path

Syntax: 
GetFileStatistics <folder_path> <target_csv_file_path>

For e.g 
GetFileStatistics "E:\docs" "E:\Output.csv"
