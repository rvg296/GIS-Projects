##Use of pyzillow and API's to retrieve the zillow id and house features
## However, this was not completely used to its potential because we can
## only make API calls but not extract and store the all the house details at once

from pyzillow.pyzillow import ZillowWrapper, GetDeepSearchResults,GetUpdatedPropertyDetails
address = "912 Plaza Ln Richardson TX"
zipcode = '75080'
zillow_data = ZillowWrapper('X1-ZWz1fjckjdd8gb_a2eph')
deep_search_response = zillow_data.get_deep_search_results(address,zipcode)
result = GetDeepSearchResults(deep_search_response)
result.zillow_id # zillow id, needed for the GetUpdatedPropertyDetails
print(result)

zillow_id = 26626079
updated_property_details_response = zillow_data.get_updated_property_details(zillow_id)
result = GetUpdatedPropertyDetails(updated_property_details_response)
print(result)
