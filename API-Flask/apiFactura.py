#Antes de las importaciones, hay que poner en el CMD "pip install Flask", luego "pip install Flask_RESTful"

from flask import Flask, request
from flask_restful import Api, Resource
from flask_cors import CORS
import requests

app = Flask(__name__)
api = Api(app)
CORS = CORS(app, resources={r"/api/*": {"origins": "*"}})

# POST localhost:port/api/boleta 
# > body: json > {id_producto, cantidad, }
# new Factura() -> #numerocualquiera --> Factura{}

url_cliente = "http://localhost:5000/api/cliente"

class Factura(Resource):
    def post(self):

        #define objeto para la respuesta de la boleta
        objRespuesta = {
            "id_producto" : 0,
            "precio": 0,
            "cantidad": 0,
            "total_venta" : 0,
            "id_cliente" : 0,
            "nombre_cliente": "",
            "direccion_cliente" : ""    
        }

        #capturas el formato json con los datos
        json = request.get_json()
 

        #imprimes el resultado en el terminal

        cliente_response = requests.get(url_cliente + "/" + str(json["id_cliente"]))
        print(cliente_response.status_code)
        if(cliente_response.status_code == 200):
            cliente_json = cliente_response.json()

            objRespuesta["id_cliente"] = cliente.json["id"]
            objRespuesta["id_producto"] = json["producto_id"]
            objRespuesta["cantidad"] = json["cantidad"]
            objRespuesta["precio"] = 320
            objRespuesta["total_venta"] = objRespuesta["precio"] * json["cantidad"]
            objRespuesta["nombre_cliente"] = json["razonSocial"] 
            objRespuesta["direccion_cliente"] = json["direccion"]



        #return { "id_producto" : 5, "cantidad": 10, "id_cliente": 2}
        return objRespuesta

api.add_resource(Factura, "/api/boleta", {"origins" : "*"})
app.run(debug=True, port= "5001")
