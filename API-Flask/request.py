import requests

url = "https://127.0.0.1:5000/"

post_response = requests.post(url, data= {"hola": "mundo", "id": 5}, headers={"Content-Type" : "application/json"})

print(post_response.status_code)

google_response = requests.get("https://www.google.cl")
print(google_response.content)