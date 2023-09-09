from flask import Flask, request, render_template,redirect,url_for
from pyzbar.pyzbar import decode
from PIL import Image


def decode_barcode_file(image_file):
    # Open the image file
    image = Image.open(image_file)

    # Decode the barcode
    barcodes = decode(image)

    # Get the barcode data
    for barcode in barcodes:
        data = barcode.data.decode("utf-8")
        return data


def format_barcode_string(data):
    # Format the string from the decoded barcode
    return ''.join(filter(str.isdigit, data))


app = Flask(__name__)

@app.route('/')
def index():
    if request.method == 'GET':
        return render_template('index.html')
    if request.method == 'POST':
        imageStudentCard = request.files['studentcard']

        return redirect(url_for('user', ))


@app.route('/<usr>', methods=['POST', 'GET'])
def user(usr):
    if request.method == 'POST':
        return redirect(url_for('index'))
    else:
        return render_template('user_page.html', usr=usr)



if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0', port=5010)
