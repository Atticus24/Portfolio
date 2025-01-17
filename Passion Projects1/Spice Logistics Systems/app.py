
from datetime import timedelta
from flask import Flask, flash, redirect, url_for, render_template, request, session
import cv2
from pyzbar.pyzbar import decode
from flask_sqlalchemy import SQLAlchemy 
from sqlalchemy import create_engine, ForeignKey

app=Flask(__name__)
app.secret_key = "hello"
app.config["SQLALCHEMY_TRACK_MODIFICATIONS"] = False
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///sls.sqlite3' 

app.permanent_session_lifetime = timedelta(minutes = 20)

camera=cv2.VideoCapture(0)

db = SQLAlchemy(app)

class Student(db.Model):
    student_nr = db.Column("Student_id", db.String, primary_key=True)
    fornavn = db.Column("Fornavn", db.String(30))
    etternavn = db.Column("Etternavn", db.String(30))
    telefon_nr = db.Column("telefon_nr", db.String(12))
    email = db.Column("Email", db.String(50))

    def __init__(self, student_nr, fornavn, etternavn, telefon_nr, email):
        self.student_nr = student_nr
        self.fornavn = fornavn
        self.etternavn = etternavn
        self.telefon_nr = telefon_nr
        self.email = email

class Inventory(db.Model):
    product_id = db.Column("Product id", db.String(), primary_key=True)
    product_name = db.Column("Product name", db.String(30))
    brand = db.Column("Brand", db.String(30))
    location = db.Column("Location", db.String(12))
    status = db.Column("Status", db.String())

    def __init__(self, product_id, product_name, brand, location, status):
        self.product_id = product_id
        self.product_name = product_name
        self.brand = brand
        self.location = location
        self.status = status

class student_inventory(db.Model):
    composite_key = db.Column(ForeignKey(Student.student_nr), ForeignKey(Inventory.product_id), primary_key=True)
    """ student_nr = db.Column("Student", db.String, ForeignKey(Student.student_nr))
    product_id = db.Column("Product", db.String, ForeignKey(Inventory.product_id)) """

  
def generate_frames():
    while True:         
        ## read the camera frame
        success,frame=camera.read()
        if not success:
            print("camera fail")
            break
        else:        
            for code in decode(frame):
                print(code.data)

            ret,buffer=cv2.imencode('.jpg',frame)
            frame=buffer.tobytes()
            
        yield(b'--frame\r\n'
            b'Content-Type: image/jpeg\r\n\r\n' + frame + b'\r\n')

@app.route("/")
def home():
    return render_template("index.html")

@app.route("/login", methods=["POST", "GET"])
def login():

    return render_template("login.html")
    if request.method == "POST":
        session.permanent = True
        student = request.form["student_nr"]
        session["active_user"] = student
        
        if request.form.get("submit") == "submit" :
            return redirect(url_for("user"))

        else:
            if "active_student" in session:
                return redirect(url_for("user"))

           

  found_student = student.query.filter_by(student_nr = student).first()
        if found_student:
            session["student_nr"] = found_student.student_nr
        else:
            student = Student(student_nr, None, None, None, None)
            db.session.add(student)
            db.session.commit()
            flash("Student has been added to the database!")
        flash("Login successful!")

      
        
        if request.form.get("Scan") == "Scan" :
            return render_template("camera.html") """
  


@app.route('/video')
def video():
    return Response(generate_frames(), mimetype='multipart/x-mixed-replace; boundary=frame') """

#===
@app.route("/user", methods=["GET", "POST"])
def user():
    if "active_user" in session:
        student = session["active_user"]
        return f"<h1>{student}</h1>"
    else:
        return redirect(url_for("login"))
    

"""  if request.method == 'POST':
        if    request.form.get('Laane') == 'Laane':
            pass # åpner camera og ber brukeren scanne utstyr
        elif  request.form.get('Levere') == 'Levere':
            pass # vise side med lån hvor man kan velge hva man vil levere tilbake
        elif  request.form.get('Se lån') == 'Se lån':
            return render_template("show_loans.html") # vise side med brukerens lån
            
    elif request.method == 'GET':
        return render_template('user.html', usr=usr)
    
    return render_template("index.html") """
#===

@app.route("/logout")
def logout():
    session.pop("user", None)
    flash("You have been logged out", "info")
    return redirect(url_for("login"))


if __name__=="__main__":
    db.create_all()
    app.run(debug=True)