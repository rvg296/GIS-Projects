"""children = mapresult.findChildren()
print(children)
for child in children:
    print(child)
pages= mapresult/26
for page in range(1,pages):
    url="http://www.zillow.com/homes/for_sale/Richardson-TX/fsba,fsbo,new_lt/54121_rid/1-_beds/1-_baths/globalrelevanceex_sort/33.041335,-96.574287,32.886363,-96.808777_rect/11_zm/"+str(page)+"_p/0_mmm/"



coord_tags = soup.find_all("span", attrs={"itemprop":"geo"})
# <span itemprop="geo" itemscope="" itemtype="http://schema.org/GeoCoordinates">
#        <meta content="32.978499" itemprop="latitude"/>
#        <meta content="-96.728306" itemprop="longitude"/>
# </span>

coordinates = [
       (
         c.find("meta", attrs={"itemprop":"latitude"}).attrs["content"],
         c.find("meta", attrs={"itemprop":"longitude"}).attrs["content"]
       )
       for c in coord_tags
]
# ('-96.728306', '32.978499')
print(coordinates)
print(len(coordinates))"""

for range in range(0,5):
    print(range)