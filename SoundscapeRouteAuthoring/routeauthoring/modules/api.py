import requests

def callConversionApi(gpxContent):
    r = requests.post("https://microsoftperformanceaiapi.azurewebsites.net/api/Conversion", data=gpxContent)
    print("Conversion API Call results")
    print(r.text)
    return r.text

def callElevationApi(positionData):
    r = requests.post("https://microsoftperformanceaiapi.azurewebsites.net/api/Elevation", data=positionData)
    print("Elevation API Call results")
    print(r.text)
    return r.text
