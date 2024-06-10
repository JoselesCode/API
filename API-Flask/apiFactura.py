from flask import Flask, request
from flask_restful import Api, Resource
from flask_cors import CORS
import requests

app = Flask(__name__)
api = Api(app)
CORS(app, resources={r"/api/*": {"origins": "*"}})

url_cliente = "http://localhost:5000/api/cliente"
url_producto = "http://localhost:5001/api/Productos"
url_logistica = "http://localhost:5001/api/StockProducto"

def obtener_cliente(id_cliente):
    cliente_response = requests.get(f"{url_cliente}/{id_cliente}")
    if cliente_response.status_code == 200:
        return cliente_response.json()
    else:
        raise Exception("Error al obtener datos del cliente")

def obtener_producto(producto_id):
    producto_response = requests.get(f"{url_producto}/{producto_id}")
    if producto_response.status_code == 200:
        return producto_response.json()
    else:
        raise Exception("Error al obtener datos del producto")

def verificar_stock(producto_id, cantidad):
    logistica_response = requests.get(f"{url_logistica}/{producto_id}")
    if logistica_response.status_code == 200:
        logistica_json = logistica_response.json()
        if logistica_json['stock'] >= cantidad:
            return logistica_json
        else:
            raise Exception("Stock insuficiente")
    else:
        raise Exception("Error al obtener datos de logística")

class Factura(Resource):
    def post(self):
        try:
            data = request.get_json()
            cliente = obtener_cliente(data["id_cliente"])
            producto = obtener_producto(data["producto_id"])
            logistica = verificar_stock(data["producto_id"], data["cantidad"])

            # Actualizar datos de la factura
            total_venta = producto["precio"] * data["cantidad"]
            factura = {
                "id_cliente": cliente["id"],
                "id_producto": data["producto_id"],
                "cantidad": data["cantidad"],
                "precio": producto["precio"],
                "total_venta": total_venta,
                "nombre_cliente": cliente["razonSocial"],
                "direccion_cliente": cliente["direccion"]
            }

            # Actualizar el stock en la API de Logística
            requests.put(f"{url_logistica}/{data['producto_id']}", json={'stock': logistica['stock'] - data['cantidad']})

            return factura

        except Exception as e:
            return {"error": str(e)}, 500

api.add_resource(Factura, "/api/boleta", methods=['POST'])

app.run(debug=True, port=5002)
