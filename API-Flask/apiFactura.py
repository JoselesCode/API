
from flask import Flask, request
from flask_restful import Api, Resource
from flask_cors import CORS
import requests

app = Flask(__name__)
api = Api(app)
CORS(app, resources={r"/api/*": {"origins": "*"}})

url_cliente = "http://localhost:5000/api/Clientes"
url_producto = "http://localhost:5001/api/Productos"
url_logistica = "http://localhost:5003/api/StockProducto"


def obtener_cliente(id):
    try:
        cliente_response = requests.get(f"{url_cliente}/{id}")
        cliente_response.raise_for_status()  # Lanza una excepción si hay un error en la respuesta
        return cliente_response.json()
    except requests.exceptions.RequestException as e:
        raise Exception(f"Error al obtener datos del cliente: {str(e)}")

def obtener_producto(id):
    try:
        producto_response = requests.get(f"{url_producto}/{id}")
        producto_response.raise_for_status()
        return producto_response.json()
    except requests.exceptions.RequestException as e:
        raise Exception(f"Error al obtener datos del producto: {str(e)}")

def obtener_stock(productoid):
    try:
        logistica_response = requests.get(f"{url_logistica}/{productoid}")
        logistica_response.raise_for_status()
        return logistica_response.json()
    except requests.exceptions.RequestException as e:
        raise Exception(f"Error al obtener datos de logística: {str(e)}")


class Factura(Resource):
    def post(self):

        #define objeto para la respuesta de la boleta
        objRespuesta = {
            "id_cliente" : 0,
            "nombre_cliente": "",
            "email_cliente": "",
            "id_producto" : 0,
            "cantidad": 0,
            "precio": 0,
            "total_venta" : 0,  
        }

        #capturas el formato json con los datos
        json_data = request.get_json()
 
        if 'ProductoId' not in json_data or 'CantidadComprada' not in json_data or 'id_cliente' not in json_data:
            return {"error": "Datos incompletos"}, 400

        #imprimes el resultado en el terminal

        try:

            producto_response = requests.get(f"{url_producto}/{json_data['ProductoId']}")
            if producto_response.status_code == 200:
                producto_json = producto_response.json()
            else:
                return {"error": "API de Productos no responde"}, 500

            cliente_json = obtener_cliente(json_data["id_cliente"])
            producto_json = obtener_producto(json_data["ProductoId"])
            logistica_json = obtener_stock(json_data["CantidadComprada"])
            #Verificar Stock
            if logistica_json['CantidadComprada'] <= json_data['CantidadComprada']:
            #Actualizar datos de boleta
                objRespuesta["id_cliente"] = cliente_json["Id"]
                objRespuesta["nombre_cliente"] = cliente_json["RazonSocial"] 
                objRespuesta["email_cliente"] = cliente_json["Email"]
                objRespuesta["id_producto"] = producto_json["Id"]
                objRespuesta["cantidad"] = json_data["CantidadComprada"]
                objRespuesta["precio"] = producto_json["Precio"]
            #   objRespuesta["total_venta"] = logistica_json["CostoTotal"]
                objRespuesta["total_venta"] = producto_json["Precio"] * json_data["CantidadComprada"]
            
            else:
                return {"error" : "Stock insuficiente"}, 400
        
        except Exception as e:
            return {"error": str(e)}, 500

        return objRespuesta
    #   logistica = obtener_producto(data["cantidad"])



api.add_resource(Factura, "/api/Boleta", methods=['POST'])

if __name__ == "__main__":
    app.run(debug=True, port=5002)
