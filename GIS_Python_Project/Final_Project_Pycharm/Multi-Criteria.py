'''GIS Final Project Source code
_author_=Rohit Venkat Gandhi Mendadhala <rxm160030@utdallas.edu>'''
## The Multi-Criteria code starts from here

import arcpy
from arcpy import env
env.workspace = "H:\\Rohit_GISP_Final\\Final_Project_GIS"

#Create the buffer around the first requirement
choice1 = input("Enter the distance parameter from schools: ")
arcpy.Buffer_analysis("Schools.shp","School_Buffer.shp",choice1,dissolve_option="ALL")
print("Buffer created around the schools")

#Create the Buffer around the second requirement
choice2 = input("Enter the distance parameter from Medical Facilities: ")
arcpy.Buffer_analysis("Medicals_Shp.shp","Medical_Buffer.shp",choice2,dissolve_option="ALL")
print("Buffer created around the Medical Facilities")

#Create the Buffer around the third requirement
choice3 = input("Enter the distance parameter from Grocery stores:")
arcpy.Buffer_analysis("Grocery_stores.shp","Grocery_Buffer.shp",choice3,dissolve_option="ALL")
print("Buffer created around the grocery stores")

#Intersecting all the Buffers to find the best area
arcpy.Intersect_analysis(["School_Buffer.shp","Medical_Buffer.shp","Grocery_Buffer.shp"],"Best_Area.shp","ALL")
print("Best Area is found")

#Select the Price Range
Min = input("Enter your minimum budget : ")
Max = input("Enter your maximum budget : ")
field = "PriceValue"
where = """{0} > {1} AND {0} < {2}""".format(arcpy.AddFieldDelimiters("Zillow_Project.shp",field),Min,Max)
arcpy.Select_analysis("Zillow_Project.shp", "Budget.shp", where)
print("Houses with the given price range are selected")

#Select the features that lie inside the Resultant area
arcpy.MakeFeatureLayer_management("Budget.shp","Budget_lyr")
arcpy.SelectLayerByLocation_management("Budget_lyr","WITHIN","Best_Area.shp",selection_type="NEW_SELECTION")
#Copy the features into a separate shapefile.
arcpy.CopyFeatures_management("Budget_Lyr","FinalHouses.shp")
print("Houses satisfying user's multi-criteria are displayed")







