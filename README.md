# Search for Best Area to buy a new Residential property in Richardson using Multi-criteria based analysis:
This is a Python and ArcGIS integrated application, which retrieves the best fit area to buy a new house in Richardson city based on his parameters. For example, if a user wants to buy a house within a specific distance from a medical facilities or school or grocery stores, it retrieves the best area and the available houses for purchase in that specific area.

# Motivation:
Most of the real-estate websites provide a detailed information for purchasing the houses based on their price values and features. Adequate importance is not given user choices. So, we decided to give user the choice to purchase a house within a prescribed distance defined by him from the facilities available in the City.

# Getting Started:
You can download the soup.py file and Multi-criteria.py file and open up in a Pycharm.

# Pre-requisities:
ArcGIS Pro latest version which can be downloaded from (https://pro.arcgis.com/en/pro-app/get-started/install-and-sign-in-to-arcgis-pro.htm). A free trial version can be obtained.
Pycharm latest version which can be downloaded from (https://www.jetbrains.com/pycharm/). A free version can be obtained.

# Installation:
*Review the requirements for ArcGIS Pro from (pro.arcgis.com). Check for the system requirements and request permissions to your organization. Minimum of 4GB RAM is required, however for a faster processing and speedy results, 8GB RAM is recommended. Follow the instructions step by step so that you can install it without any problems. Also make sure your graphic drivers to be updated to the latest version, because ArcGIS Pro uses a high amount of graphics.

*Review the requirements for installing Pycharm (https://www.jetbrains.com/pycharm/).After installation properly set the path for the interpreter in settings. Copy the jdk file associated with ArcGIS Pro into the C:\xxx\xxxx\PyCharm2016.2\config\options directory so that ArcPy library will be available for usage in Pycharm.

# Packages used:
* Pyzillow package has been used for making the API calls to the URL of Zillow website which retrieves the Zillow id and some of the house features like house id, no. of bathrooms, bedrooms etc.
* Beautiful Soup package is used to for webscrapping the latitude and longitude coordinates from the real-estate website which helps in storing these values in a csv file. Also each feature of the house like Name, ID, Price Value can be extracted.

## API's reviewed for analysis:
(GetDeepSearchResults) API allows us to retrieve the ZillowID associated by providing the inputs address, zipcode and an API key which can be obtained by registering in Zillow for API call making.
(GetUpdatedPropertyDetails) API allows us to obtain the updated properties of that particular houses for sale with that ZillowID. Some of the properties like hometype, no. of bedrooms, no. of bathrooms, last sold price can be obtained.

# Tests:
Right click and run the soup.py file in Pycharm from which you can extract the house details like House ID, House Address, Zip Code, Price Value from the real estate URL (zillow.com) into a .csv file. Once the .csv file is ready make sure all the data types of variables are correctly obtained. If necessary convert or type cast them into approriate ones. Import this .csv file as XY event data in ArcGIS Pro and then export it to a shapefile. Download the opensource shapefiles of Medical Facilities, Schools, Grocery stores, City Limits from (opendata.richardson.opendata.arcgis.com) . Since zillow use Bing Maps which have a WGS 1984 Projection, make sure we project it to the appropriate projection of Richardson shape files which is NAD1983. 

Now run the MultiCriteria.py file in Pycharm. It then prompts you to input the distance parameters that you want to live within the schools, medical facilities or the grocery stores. It then prompts you to input the minimum and maximum budget range which you can afford for buying the house in Richardson. Now, you can observe the  the houses available for purchase in the best fit area obtained by the user provided parameters.

# Code functions:
Major arcpy functions are present in this code:
arcpy.Buffer_analysis --> Used for creating a Buffer around the input features.
arcpy.Intersect_analysis --> Used for intersecting the obtained Buffers.
arcpy.Select_analysis ---> Used for selecting the houses based on their price value.
arcpy.SelectLayerbyLocation_management --> Used for selecting the obtained houses that are within the best area obtained.
arcpy.Copy_management ---> Used for showing the houses as a separate layer which the user can finalize upon purchasing.


# Contributors:
1)Pyzillow API concept idea proposal and implementation by hanneshapke from GitHub.
2)Idea of using Beautiful Soup for this project - Dr.Bryan Chastain.
3)Python Webscraping and Implementation - Jeevan Patnaik.
4)Project idea, ArcGIS Pro implementation, Core Aspects - Rohit.

# Copying/License:
 * Copyright (C) 2016 Dr.Chastain, Zillow.com,HanneShapke,Rohit - All Rights Reserved
 * You may use, distribute and modify this code under the permission from either of them mentioned above. 



