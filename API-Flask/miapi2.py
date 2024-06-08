from flask import Flask, request
from flask_restful import Resource, Api

app = Flask(__name__)
api = Api(app)

clientes={}

contador = 10

class HoalMundo(Resource):
    def get(self, cliente_id):
        return clientes[cliente_id]
    
    def put (self, cliente_id):
        clientes[cliente_id] = request.get_json()

class HoalMundoSinId(Resource):
    def get(self):
        return clientes
    def post(self):
        json = request.get_json()
        clientes[json["id"]] = json 
        return json["id"]
        
    
api.add_resource(HoalMundo, '/<string:cliente_id>')
api.add_resource(HoalMundoSinId, '/')

if __name__ == '__main__':
    app.run(debug=True)

else:
    print("No se no se encuentra") 