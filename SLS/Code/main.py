import json
from flask import Flask, render_template, jsonify
from flask_mongoengine import MongoEngine

app = Flask(__name__)
app.config['MONGODB_SETTINGS'] = {
    'db': 'mydatabase',
    'host': 'mongodb://localhost:27017/mydatabase'
}

db = MongoEngine()
db.init_app(app)

class Product(db.EmbeddedDocument):
    barcode = db.StringField()
    name = db.StringField()
    status = db.BooleanField(default=True)

    def to_json(self):
        return {"barcode": self.barcode, 
                "name": self.name,
                "status" : self.status
                }

class Loanee(db.Document):
    iD = db.StringField()
    personalInventory = db.ListField(db.EmbeddedDocumentField(Product))
    def to_json(self):
        return {"iD": self.iD, 
                "personalInventory": self.personalInventory}
    
product1 = Product(barcode="1", name="Arduino")
product2 = Product(barcode="2", name="Bok")

Loanee(iD="247649", personalInventory=[product1, product2]).save()
Loanee(iD="242424", personalInventory=[product1, product2]).save()
Loanee(iD="62622", personalInventory=[product1, product2]).save()
Loanee(iD="72266", personalInventory=[product1, product2]).save()


Loanee.objects(iD="247649").first()



@app.route('/')
def index():
    return render_template('index.html')


if __name__ == '__main__':
    app.run(debug=True, host='localhost', port=1025)




