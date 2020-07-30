from django.shortcuts import render
from django.http import HttpResponse
from modules.api import *
import json

def index(request):
    processed_elevation_data = None
    if request.method == 'POST' and request.FILES['uploadedGpx']:
        try:
            # elevation_data_str is a string JSON objects with elevation added from API calls
            elevation_data_str = handle_uploaded_gpx(request.FILES['uploadedGpx'], str(request.FILES['uploadedGpx']))
            elevation_data = json.loads(elevation_data_str)
            # prepare data for Preview headers
            processed_elevation_data = processElevationPoints(elevation_data)
            
        except Exception:
            print('The file uploaded has issues')
    
    # renders the HTML page and passes json_data to it to render its elements dynamically using this variable
    return render(request, "personal/webpage.html", {'processed_elevation_data': processed_elevation_data})

def handle_uploaded_gpx(file, filename):
    print(filename)
    content = file.read()
    if content is None:
         raise Exception('There is no content in this file')
    positionData = callConversionApi(content)
    if positionData is None:
         raise Exception('No position data could be found')
    elevationSegments = callElevationApi(positionData)
    if elevationSegments is None:
         raise Exception('No elevation segments were generated')
    return elevationSegments

def processElevationPoints(elevation_data):
    print('processing points')
    downhills = 0
    uphills = 0
    for i, item in enumerate(elevation_data):
        if(item["isDownHill"]):
            downhills += 1
            item["title"] = "Downhill " + str(downhills)
        else:
            uphills += 1
            item["title"] = "Uphill " + str(uphills)
    return elevation_data