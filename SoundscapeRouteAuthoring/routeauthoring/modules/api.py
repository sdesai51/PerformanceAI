import requests

def callConversionApi(gpxContent):
    try:
        r = requests.post("https://microsoftperformanceaiapi.azurewebsites.net/api/Conversion", data=gpxContent)
        print("Conversion API Call results")
        print(r.text)
        return r.text
    except Exception as e:
        print('Error calling Conversion API')
        return ''

def callElevationApi(positionData):
    try:
        r = requests.post("https://microsoftperformanceaiapi.azurewebsites.net/api/Elevation", data=positionData)
        print("Elevation API Call results")
        print(r.text)
        return r.text
    except Exception as e:
        print('Error calling Elevation API')
        return ''

def callSoundscapeGpxApi(experienceData):
    #experienceData is assumed to be a json string at this point
    try:
        r = requests.post("https://microsoftperformanceaiapi.azurewebsites.net/api/SoundscapeGpx", data=experienceData)
        print("SoundscapeGpx API Call results")
        print(r.text)
        return r.text
    except Exception as e:
        print('Error calling SoundscapeGpx API')
        return ''