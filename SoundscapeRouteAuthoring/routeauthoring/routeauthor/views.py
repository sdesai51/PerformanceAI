from django.shortcuts import render

from django.http import HttpResponse


def index(request):
<<<<<<< HEAD
    return render(request, "personal/webpage.html", {})
    # return HttpResponse("Hello, world. You're at the index.")
=======
    # return HttpResponse("Hello, world. You're at the index.")
    return render(request, 'webpage.html', {})
>>>>>>> a5c087cedb1d1e8c016a8ef1444b8c162b76ee03

