from django.shortcuts import render
from django.http import HttpResponse
from modules.api import *


def index(request):
    if request.method == 'POST' and request.FILES['uploadedGpx']:
        try:
            elevation = handle_uploaded_gpx(request.FILES['uploadedGpx'], str(request.FILES['uploadedGpx']))
        except Exception as e:
            print('The file uploaded has issues')

    return render(request, "personal/webpage.html", {})

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

def convertElevationToWaypoint():
    print('method to write')
    # take the elevations
    # translation to soundscape waypoints