from django.shortcuts import render
from django.http import HttpResponse
from modules.api import *
import json


def index(request):
    json_data = None
    if request.method == 'POST' and request.FILES['uploadedGpx']:
        try:
            elevation = handle_uploaded_gpx(request.FILES['uploadedGpx'], str(request.FILES['uploadedGpx']))
            json_data = json.loads(elevation)
            print("Python data loaded from JSON")
            print(json_data[0]["length"])
            # print(y["length"])
        except Exception as e:
            print('The file uploaded has issues')
    

    # convertElevationToWaypoint(elevation)
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
    # print(elevationSegments)
    return elevationSegments

# def convertElevationToWaypoint(elevation):
#     print('method to write')
#     # take the elevations
#     # translation to soundscape waypoints

#     y = json.loads(elevation)
#     print(y)