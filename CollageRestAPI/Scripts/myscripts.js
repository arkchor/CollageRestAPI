"use strict";

if (!Array.prototype.last) {
    Array.prototype.last = function () {
        return this[this.length - 1];
    };
};

var apiStudents = "http://localhost:5501/api/students";
var apiCourses = "http://localhost:5501/api/courses";

//ko.bindingHandlers.datePicker = {
//    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
//        // Register change callbacks to update the model
//        // if the control changes.       
//        ko.utils.registerEventHandler(element, "change", function () {
//            //var value = valueAccessor();
//            var value = ko.unwrap(valueAccessor());
//            value(new Date(element.value));
//        });
//    },
//    // Update the control whenever the view model changes
//    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
//        var value = ko.unwrap(valueAccessor());
//        element.value = value().toISOString();
//    }
//};

ko.bindingHandlers.datePicker = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel) {
        // Register change callbacks to update the model
        // if the control changes.
        ko.utils.registerEventHandler(element,
            "change",
            function() {
                var value = valueAccessor();
                value(moment(element.value).format());
            });
    },
    // Update the control whenever the view model changes
    update: function(element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        element.value = moment(value()).format("YYYY-MM-DD");
    }
};

var StudentModel = function () {
    var self = this;
    self.Id = "";
    self.FirstName = "";
    self.LastName = "";
    self.BornDate = "";
    //self.ToCreate = true;
}

var StudentsRequest = function () {
    var self = this;
    self.Id = ko.observable("");
    self.FirstName = ko.observable("");
    self.LastName = ko.observable("");
    self.BornDate = ko.observable("");
    self.PageSize = ko.observable("");
    self.PageNumber = ko.observable("");
}

var CourseModel = function () {
    var self = this;
    self.Id = "";
    self.CourseName = "";
    self.Tutor = "";
}

var CoursesRequest = function () {
    var self = this;
    self.Id = ko.observable("");
    self.CourseName = ko.observable("");
    self.Tutor = ko.observable("");
    self.PageSize = ko.observable("");
    self.PageNumber = ko.observable("");
}

var GradesRequest = function () {
    var self = this;
    self.Id = ko.observable("");
    self.Value = ko.observable("");
    self.IssueDateTime = ko.observable("");
    self.CourseName = ko.observable("");
}

//var CoursesRequest = function () {
//    var self = this;
//    self.Id = "";
//    self.CourseName = "";
//    self.Tutor = "";
//    self.PageSize = "";
//    self.PageNumber = "";
//}

var GradeModel = function () {
    var self = this;
    self.Id = "";
    self.Value = "";
    self.IssueDateTime = "";
    self.CourseName = "";
}

//var StudentModel = function () {
//    var self = this;
//    self.Id = ko.observable();
//    self.FirstName = ko.observable();
//    self.LastName = ko.observable();
//    self.BornDate = ko.observable();
//}

var ViewModel = function() {
    var self = this;

    self.students = ko.observableArray([]);
    self.courses = ko.observableArray([]);
    self.grades = ko.observableArray([]);

    self.newStudentPreparation = ko.observable(false);
    self.newCoursePreparation = ko.observable(false);
    self.newGradePreparation = ko.observable(false);

    self.currentStudentForGradeView = ko.observable(new StudentModel());

    self.coursesRequest = ko.observable(new CoursesRequest());
    self.studentsRequest = ko.observable(new StudentsRequest());

    self.CourseName = ko.observable("");
    self.GradeValue = ko.observable("");

    //self.getStudents = function() {
    //    $.ajax({
    //        type: "GET",
    //        url: apiStudents,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (data) {
    //            console.log(data); //Put the response in ObservableArray
    //            self.students = ko.mapping.fromJS(data, ViewModel);
    //        },
    //        error: function (error) {
    //            alert(error.status + "<--and--> " + error.statusText);
    //        }
    //    });
    //}

    //$.ajax({
    //    type: "GET",
    //    url: apiStudents,
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (data) {
    //        console.log(data); //Put the response in ObservableArray
    //        console.log(self.students());
    //        self.students = ko.mapping.fromJS(data, ViewModel);
    //        console.log(self.students());
    //    },
    //    error: function (error) {
    //        alert(error.status + "<--and--> " + error.statusText);
    //    }
    //});

    //$.getJSON(apiStudents, function (data) {
    //    console.log(data); //Put the response in ObservableArray
    //    console.log(self.students());
    //    self.students = ko.mapping.fromJS(data, ViewModel);
    //    console.log(self.students());
    //});

    /*=======================================
      ============= STUDENTS ================
      =======================================*/

    self.getStudents = function () {

        var query = "?"
            + "id=" + self.studentsRequest().Id()
            + "&firstName=" + self.studentsRequest().FirstName()
            + "&lastName=" + self.studentsRequest().LastName()
            + "&bornDate=" + self.studentsRequest().BornDate();

        console.log(query);

        $.ajax({
            type: "GET",
            url: apiStudents + query,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
                console.log(data); //Put the response in ObservableArray
                console.log(self.students());

                //self.students = ko.mapping.fromJS(data, ViewModel);
                //self.students(ko.mapping.fromJS(data, ViewModel));

                //var mappedStudents = $.map(data, function (item) { return new Student(item) });
                var mappedStudents = ko.mapping.fromJS(data, ViewModel);
                console.log(mappedStudents());
                self.students(mappedStudents());

                console.log(self.students());               
                console.log(self.students()[0].BornDate());
                console.log(self);

                //console.log("#################");
                //console.log(self.students().length);
                //console.log(self.students()[self.students().length - 1]);
                //console.log(self.students().last());
                console.log(self.newStudentPreparation());
            },
            error: function(error) {
                alert(error.status + "<--and--> " + error.statusText);
            }
        });       
    }

    self.prepareStudent = function () {
        var mappedStudent = ko.mapping.fromJS(new StudentModel(), ViewModel);
        //console.log(mappedStudent);
        //self.students().push(mappedStudent);
        //self.students().push(new StudentModel());
        //self.students.push(new StudentModel());
        self.students.push(mappedStudent);
        console.log(self.students());
        self.newStudentPreparation(true);
    }

    self.cancelPrepareStudent = function () {
        self.students.pop();
        console.log(self.students());
        self.newStudentPreparation(false);
    }

    self.createStudent = function () {
        if (self.students().last().FirstName() === "" ||
            self.students().last().LastName() === "" ||
            self.students().last().BornDate() === "") {

            console.log(ko.toJSON(ko.mapping.toJS(self.students().last())));
            alert("Proszę wypełnić pola!");
        } else {
            $.ajax({
                type: "POST",
                url: apiStudents,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: ko.toJSON(ko.mapping.toJS(self.students().last())),
                success: function (data) {
                    //alert("Utworzono studenta!");
                    console.log(data);
                    self.newStudentPreparation(false);
                    self.getStudents();
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }       
    }

    self.updateStudent = function (student) {
        var confirmationValue = confirm("Czy na pewno chcesz zmienić dane studenta?");
        if (confirmationValue) {
            $.ajax({
                type: "PUT",
                url: apiStudents + "?id=" + student.Id(),
                contentType: "application/json; charset=utf-8",
                data: ko.toJSON(ko.mapping.toJS(student)),
                success: function (data) {
                    self.getStudents();
                    alert("Dane studenta zostały zaktualizowane!");
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }      
    }

    self.deleteStudent = function (student) {
        var confirmationValue = confirm("Czy na pewno chcesz usunąć studenta?");
        if (confirmationValue) {
            $.ajax({
                type: "DELETE",
                url: apiStudents + "?id=" + student.Id(),
                success: function(data) {
                    self.getStudents();
                },
                error: function(error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }
    }

    self.seeStudentGrades = function (student) {

        return true;
    }

    /*=======================================
      ============== COURSES ================
      =======================================*/

    self.getCourses = function () {
        //console.log(self.coursesRequest().Tutor());
        //var query = "";
        //if (self.coursesRequest().Tutor() !== "") {
            //query += "?tutor=" + self.coursesRequest().Tutor();
        //}
        var query = "?"
            + "id=" + self.coursesRequest().Id()
            + "&courseName=" + self.coursesRequest().CourseName()
            + "&tutor=" + self.coursesRequest().Tutor();

        $.ajax({
            type: "GET",
            url: apiCourses + query,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {              
                var mappedCourses = ko.mapping.fromJS(data, ViewModel);
                console.log(mappedCourses());
                self.courses(mappedCourses());
            },
            error: function (error) {
                alert(error.status + "<--and--> " + error.statusText);
            }
        });
    }

    self.prepareCourse = function () {
        var mappedCourse = ko.mapping.fromJS(new CourseModel(), ViewModel);
        self.courses.push(mappedCourse);
        self.newCoursePreparation(true);
    }

    self.cancelPrepareCourse = function () {
        self.courses.pop();
        self.newCoursePreparation(false);
    }

    self.createCourse = function () {
        if (self.courses().last().CourseName() === "" ||
            self.courses().last().Tutor() === "") {

            alert("Proszę wypełnić pola!");
        } else {
            $.ajax({
                type: "POST",
                url: apiCourses,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: ko.toJSON(ko.mapping.toJS(self.courses().last())),
                success: function (data) {
                    //alert("Utworzono studenta!");
                    console.log(data);
                    self.newCoursePreparation(false);
                    self.getCourses();
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }
    }

    self.updateCourse = function (course) {
        var confirmationValue = confirm("Czy na pewno chcesz zmienić dane kursu?");
        if (confirmationValue) {
            console.log(ko.mapping.toJS(course));
            console.log(ko.toJSON(ko.mapping.toJS(course)));
            $.ajax({
                type: "PUT",
                url: apiCourses + "?courseName=" + course.CourseName(),
                contentType: "application/json; charset=utf-8",
                data: ko.toJSON(ko.mapping.toJS(course)),
                success: function (data) {
                    self.getCourses();
                    alert("Dane kursu zostały zaktualizowane!");
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }
    }

    self.deleteCourse = function (course) {
        var confirmationValue = confirm("Czy na pewno chcesz usunąć kurs?");
        if (confirmationValue) {
            $.ajax({
                type: "DELETE",
                url: apiCourses + "?courseId=" + course.Id(),
                success: function (data) {
                    self.getCourses();
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });          
        }
    }

    self.seeCourseGrades = function (course) {
        //console.log(ko.toJSON(ko.mapping.toJS(student)));
    }

//    type: "GET",
//    url: apiStudents + "/grades?id=" + student.Id(),
//    contentType: "application/json; charset=utf-8",
//    dataType: "json",
//    success: function(data) {
//        data.forEach(function(grade) {
//            self.courses()
//                .forEach(function(course) {
//                    if (course().Id() === grade.CourseReference.Id) {
//                        grade.
//                    }
//                });
//        });
//    }
//});
    /*=======================================
      =============== GRADES ================
      =======================================*/

    self.getStudentGrades = function (student) {
        console.log(student);
        //var gradeValue = isNaN(self.GradeValue()) ? "0" : self.GradeValue();
        //var gradeValue = Number.isInteger(self.GradeValue()) ? 0 : self.GradeValue();
        var gradeValue;
        if (self.GradeValue() === "") {
            gradeValue = 0;
        } else {
            gradeValue = self.GradeValue();
        }
        console.log(gradeValue);
        
        $.ajax({
            type: "GET",
            url: apiStudents + "/grades?id=" + student.Id()
                + "&courseName=" + self.CourseName()
                + "&gradeValue=" + gradeValue,//self.GradeValue(),//(parseInt(self.GradeValue()) == NaN ? "" : parseInt(self.GradeValue())),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //console.log(data);
                data.forEach(function (grade) {
                    var courses = ko.mapping.toJS(self.courses);
                    //console.log(courses);                   
                    courses
                        .forEach(function (course) {
                            //console.log(course.Id);
                            //console.log(grade);
                            if (course.Id === grade.CourseReference.Id) {
                                grade.CourseName = course.CourseName;
                            }
                        });
                });
                var mappedGrades = ko.mapping.fromJS(data, ViewModel);
                //console.log(mappedGrades());
                self.grades(mappedGrades());
            },
            error: function (error) {
                alert(error.status + "<--and--> " + error.statusText);
            }
        });

        self.currentStudentForGradeView(student);
        //console.log(self.currentStudentForGradeView.FirstName());

        return true;
    }

    self.getStudentGradesFiltered = function() {
        self.getStudentGrades(self.currentStudentForGradeView());
    }

    self.prepareGrade = function () {
        var mappedGrade = ko.mapping.fromJS(new GradeModel(), ViewModel);
        self.grades.push(mappedGrade);
        self.newGradePreparation(true);
    }

    self.cancelPrepareGrade = function () {
        self.grades.pop();
        self.newGradePreparation(false);
    }

    self.createGrade = function () {
        if (self.grades().last().CourseName() === "" ||
            self.grades().last().Value() === "" ||
            self.grades().last().IssueDateTime() === "") {
            console.log(apiCourses + "?id=" + self.currentStudentForGradeView().Id() + "&courseName=" + self.grades().last().CourseName());
            alert("Proszę wypełnić pola!");
        } else {
            //console.log(apiCourses + "?id=" + self.currentStudentForGradeView().Id() + "&courseName=" + self.grades().last().CourseName());
            var query = "?id=" +
                self.currentStudentForGradeView().Id() +
                "&courseName=" +
                self.grades().last().CourseName();
            var gradeToCreate = ko.mapping.toJS(self.grades().last());
            //console.log(gradeToCreate);
            //delete gradeToCreate.CourseName;
            //console.log(gradeToCreate);
            $.ajax({
                type: "POST",
                url: apiCourses + "/grades" + query,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: ko.toJSON(gradeToCreate),
                success: function (data) {
                    //alert("Utworzono studenta!");
                    console.log(data);
                    self.newGradePreparation(false);
                    self.getStudentGrades(self.currentStudentForGradeView());
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }
    }

    self.updateGrade = function (grade) {
        var confirmationValue = confirm("Czy na pewno chcesz zmienić dane oceny?");
        if (confirmationValue) {           
            //var gradeToUpdate = ko.mapping.toJS(grade);
            var gradeToUpdate = grade;
            //var courseReference = gradeToUpdate.CourseReference;
            delete gradeToUpdate.CourseName;
            delete gradeToUpdate.CourseReference;
            //console.log(grade);
            //console.log(ko.mapping.toJS(grade));
            console.log(gradeToUpdate);
            console.log(ko.toJSON(gradeToUpdate));
            console.log(grade);
            $.ajax({
                type: "PUT",
                url: apiCourses + "/grades",
                contentType: "application/json; charset=utf-8",
                data: ko.toJSON(ko.mapping.toJS(gradeToUpdate)),
                success: function (data) {
                    //var gradeUpdated = ko.mapping.toJS(grade);
                    //gradeUpdated.CourseReference = courseReference;
                    //grade(gradeUpdated);
                    //var gradeUpdated = grade;
                    //gradeUpdated.CourseReference = courseReference;
                    //grade = gradeUpdated;
                    self.getStudentGrades(self.currentStudentForGradeView());
                    alert("Dane oceny zostały zaktualizowane!");
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }
    }

    self.deleteGrade = function (grade) {
        var confirmationValue = confirm("Czy na pewno chcesz usunąć ocenę?");
        if (confirmationValue) {
            console.log(apiCourses + "/grades" + "?gradeId=" + grade.Id());
            $.ajax({
                type: "DELETE",
                url: apiCourses + "/grades" + "?gradeId=" + grade.Id(),
                success: function (data) {
                    self.getStudentGrades(self.currentStudentForGradeView());
                },
                error: function (error) {
                    alert(error.status + "<--and--> " + error.statusText);
                }
            });
        }
    }   
}

var vm = new ViewModel();
//console.log(vm.query);
//console.log(vm.coursesRequest().Tutor);

vm.coursesRequest().Id.subscribe(vm.getCourses);
vm.coursesRequest().CourseName.subscribe(vm.getCourses);
vm.coursesRequest().Tutor.subscribe(vm.getCourses);

vm.studentsRequest().Id.subscribe(vm.getStudents);
vm.studentsRequest().FirstName.subscribe(vm.getStudents);
vm.studentsRequest().LastName.subscribe(vm.getStudents);
vm.studentsRequest().BornDate.subscribe(vm.getStudents);

vm.CourseName.subscribe(vm.getStudentGradesFiltered);
vm.GradeValue.subscribe(vm.getStudentGradesFiltered);

vm.getStudents();
vm.getCourses();
ko.applyBindings(vm);