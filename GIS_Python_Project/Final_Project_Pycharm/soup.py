'''GIS Final Project Source code
_author_=Rohit Venkat Gandhi Mendadhala <rxm160030@utdallas.edu>'''
## This code extracts the latitude and longitude values of the real estate houses for sale
## and also other parameters and then imported into ArcGIS Pro

import requests
from bs4 import BeautifulSoup
# Enter the URL for extracting real-estate information
url = "http://www.zillow.com/homes/for_sale/Richardson-TX/fsba,fsbo,new_lt/54121_rid/1-_beds/1-_baths/globalrelevanceex_sort/33.041335,-96.574287,32.886363,-96.808777_rect/11_zm/1_p/0_mmm/"
r = requests.get(url)
r.content
# Parse the content with HTML Parser
soup = BeautifulSoup(r.content,"html.parser")
mapresult = soup.find("meta",attrs={"name":"description"})['content'].split(" ")[2]
#Obtain the map result to check how many houses are for sale
print(mapresult)
# Find the number of pages showing the houses for sale
pages= int(mapresult)//26
print(pages)
# Create a list for storing the coordinates
coordinates = []
# Create an ID for storing the houses
id = 1
# Open a csv file in write mode to store the house details.
file = open("Zillow.csv",mode='w')
file.write("ID,Address,Zipcode,Latitude,Longitude,Price\n")

# Loop through the pages
for page in range(1,pages+2):
    url="http://www.zillow.com/homes/for_sale/Richardson-TX/fsba,fsbo,new_lt/54121_rid/1-_beds/1-_baths/globalrelevanceex_sort/33.041335,-96.574287,32.886363,-96.808777_rect/11_zm/"+str(page)+"_p/0_mmm/"
    r = requests.get(url)
    r.content
    soup = BeautifulSoup(r.content, "html.parser")
    # Find span item with attribute geo where Lat Lon values can be obtained
    coord_tags = soup.find_all("span", attrs={"itemprop":"geo"})
    for home in coord_tags:
        ps = home.previous_sibling
        children = ps.findChildren()
        streetaddress=children[0].text
        addresslocality = children[1].text
        addressRegion = children[2].text
        zip = children[3].text
        pricevalue = home.next_sibling.find("span",attrs={"class":"zsg-photo-card-price"})
        if pricevalue is None:
            price = ""
        else:
            price= "\""+pricevalue.text+"\""
        address = "\""+streetaddress+" "+addresslocality+", "+addressRegion+"\""
        children = home.findChildren()
        latitude = children[0]["content"]
        longitude = children[1]["content"]
        file.write(str(id)+","+address+","+zip+","+latitude+","+longitude+","+str(price)+","+"\n")
        id+=1
file.close()




