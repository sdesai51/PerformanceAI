from django.shortcuts import render
from django.http import HttpResponse
from modules.api import *
import json

def index(request):
    json_data = None
    if request.method == 'POST' and request.FILES['uploadedGpx']:
        try:
            # elevation is a string JSON objects with elevation added from API calls
            elevation = handle_uploaded_gpx(request.FILES['uploadedGpx'], str(request.FILES['uploadedGpx']))
            # json_data returns a list of the JSON objects
            json_data = json.loads(elevation)
           
            # populate Preview headers   
            downhills = 0
            uphills = 0
            for i, item in enumerate(json_data):
                if(item["isDownHill"]):
                    downhills += 1
                    item["title"] = "Downhill " + str(downhills)
                else:
                    uphills += 1
                    item["title"] = "Uphill " + str(uphills)
        except Exception as e:
            print('The file uploaded has issues')
    
    # renders the HTML page and passes json_data to it to render its elements dynamically using this variable
    return render(request, "personal/webpage.html", {'json_data': json_data})

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
