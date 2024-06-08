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
url_producto = "http://localhost:5001/api/Producto" #Producto
url_logistica = "http://localhost:5001/api/StockProducto" #Boleta

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

        # Solicitud a la API de Productos (para obtener el precio del producto)
            producto_response = requests.get(f"{url_producto}/{json_data['producto_id']}")
            if producto_response.status_code == 200:
                producto_json = producto_response.json()

        # Solicitud a la API de Logística (para verificar el stock)
                logistica_response = requests.get(f"{url_logistica}/{json_data['producto_id']}")
                if logistica_response.status_code == 200:
                    logistica_json = logistica_response.json()
        #Verificar Stock
                    if logistica_json['stock'] >= json_data['cantidad']:
        #Actualizar datos de boleta
                        objRespuesta["id_cliente"] = cliente_json["id"]
                        objRespuesta["id_producto"] = json["producto_id"]
                        objRespuesta["cantidad"] = json["cantidad"]
                        objRespuesta["precio"] = producto_json["precio"]
                        objRespuesta["total_venta"] = objRespuesta["precio"] * json["cantidad"]
                        objRespuesta["nombre_cliente"] = json["razonSocial"] 
                        objRespuesta["direccion_cliente"] = json["direccion"]

                        # Actualizar el stock en la API de Logística Stock
                        requests.put(f"{url_logistica}/{json_data['producto_id']}",
                        json={'stock': logistica_json['stock'] - json_data['cantidad']})
                        return objRespuesta
                    else:
                        return {"error" : "Stock insuficiente"}, 400
                else:
                    return {"error" : "API de Logistica no responde"}, 500
            else:
                return {"error" : "API de Productos no responde"}, 500
        else:
            return {"error" : "API de Clientes no responde"}, 500
                        

        #return { "id_producto" : 5, "cantidad": 10, "id_cliente": 2}
        return objRespuesta

api.add_resource(Factura, "/api/boleta", {"origins" : "*"})
app.run(debug=True, port= "5002")
