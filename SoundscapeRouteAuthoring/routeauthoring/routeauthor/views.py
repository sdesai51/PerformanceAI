from django.shortcuts import render
from django.http import HttpResponse
from modules.api import *


def index(request):
    if request.method == 'POST' and request.FILES['uploadedGpx']:
        handle_uploaded_gpx(request.FILES['uploadedGpx'], str(request.FILES['uploadedGpx']))
        
    return render(request, "personal/webpage.html", {})

# debug method
def handle_uploaded_gpx(file, filename):
    print(filename)
    content = file.read()
    positionData = callConversionApi(content)
    elevationSegments = callElevationApi(positionData)
