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

@app.route("/login", methods=["POST", "GET"])
def login():
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
        
        