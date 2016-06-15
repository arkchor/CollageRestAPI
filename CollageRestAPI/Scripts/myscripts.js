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
    var student = new StudentModel();
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
    self.query = ko.observable("");

    //self.coursesRequest.subscribe(self.getC);

    self.coursesRequestChanged = function(newValue) {
        
    }

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
        $.ajax({
            type: "GET",
            url: apiStudents,
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

    self.getC = function (value) {
        console.log("### INSIDE ###");
        self.getCourses();
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
                url: apiCourses + "?courseName=" + course.CourseName(),
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
        $.ajax({
            type: "GET",
            url: apiStudents + "/grades?id=" + student.Id(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
                data.forEach(function (grade) {
                    var courses = ko.mapping.toJS(self.courses);
                    console.log(courses);                   
                    courses
                        .forEach(function (course) {
                            console.log(course.Id);
                            console.log(grade);
                            if (course.Id === grade.CourseReference.Id) {
                                grade.CourseName = course.CourseName;
                            }
                        });
                });
                var mappedGrades = ko.mapping.fromJS(data, ViewModel);
                console.log(mappedGrades());
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

    self.prepareGrade = function () {
        var mappedCourse = ko.mapping.fromJS(new CourseModel(), ViewModel);
        self.courses.push(mappedCourse);
        self.newCoursePreparation(true);
    }

    self.cancelPrepareGrade = function () {
        self.courses.pop();
        self.newCoursePreparation(false);
    }

    self.createGrade = function () {
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

    self.updateGrade = function (course) {
        var confirmationValue = confirm("Czy na pewno chcesz zmienić dane kursu?");
        if (confirmationValue) {
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

    self.deleteGrade = function (course) {
        var confirmationValue = confirm("Czy na pewno chcesz usunąć kurs?");
        if (confirmationValue) {
            $.ajax({
                type: "DELETE",
                url: apiCourses + "?courseName=" + course.CourseName(),
                success: function (data) {
                    self.getCourses();
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
vm.coursesRequest().Id.subscribe(vm.getC);
vm.coursesRequest().CourseName.subscribe(vm.getC);
vm.coursesRequest().Tutor.subscribe(vm.getC);
vm.query.subscribe(vm.getC);
vm.getStudents();
vm.getCourses();
ko.applyBindings(vm);

var EmpViewModel = function () {
    //Make the self as 'this' reference
    var self = this;
    //Declare observable which will be bind with UI 
    self.EmpNo = ko.observable("0");
    self.EmpName = ko.observable("");
    self.Salary = ko.observable("");
    self.DeptName = ko.observable("");
    self.Designation = ko.observable("");

    //The Object which stored data entered in the observables
    var EmpData = {
        EmpNo: self.EmpNo,
        EmpName: self.EmpName,
        Salary: self.Salary,
        DeptName: self.DeptName,
        Designation: self.Designation
    };

    //Declare an ObservableArray for Storing the JSON Response
    self.Employees = ko.observableArray([]);

    GetEmployees(); //Call the Function which gets all records using ajax call

    //Function to perform POST (insert Employee) operation
    self.save = function () {
        //Ajax call to Insert the Employee
        $.ajax({
            type: "POST",
            url: "http://localhost:50457/api/EmployeeInfoAPI",
            data: ko.toJSON(EmpData), //Convert the Observable Data into JSON
            contentType: "application/json",
            success: function (data) {
                alert("Record Added Successfully");
                self.EmpNo(data.EmpNo);
                alert("The New Employee Id :" + self.EmpNo());
                GetEmployees();
            },
            error: function () {
                alert("Failed");
            }
        });
        //Ends Here
    };

    self.update = function () {
        var url = "http://localhost:50457/api/EmployeeInfoAPI/" + self.EmpNo();
        alert(url);
        $.ajax({
            type: "PUT",
            url: url,
            data: ko.toJSON(EmpData),
            contentType: "application/json",
            success: function (data) {
                alert("Record Updated Successfully");
                GetEmployees();
            },
            error: function (error) {
                alert(error.status + "<!----!>" + error.statusText);
            }
        });
    };

    //Function to perform DELETE Operation
    self.deleterec = function (employee) {
        $.ajax({
            type: "DELETE",
            url: "http://localhost:50457/api/EmployeeInfoAPI/" + employee.EmpNo,
            success: function (data) {
                alert("Record Deleted Successfully");
                GetEmployees();//Refresh the Table
            },
            error: function (error) {
                alert(error.status + "<--and--> " + error.statusText);
            }
        });
        // alert("Clicked" + employee.EmpNo)
    };

    //Function to Read All Employees
    function GetEmployees() {
        //Ajax Call Get All Employee Records
        $.ajax({
            type: "GET",
            url: "http://localhost:50457/api/EmployeeInfoAPI",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                self.Employees(data); //Put the response in ObservableArray
            },
            error: function (error) {
                alert(error.status + "<--and--> " + error.statusText);
            }
        });
        //Ends Here
    }

    //Function to Display record to be updated. This will be

    //executed when record is selected from the table
    self.getselectedemployee = function (employee) {
        self.EmpNo(employee.EmpNo),
        self.EmpName(employee.EmpName),
        self.Salary(employee.Salary),
        self.DeptName(employee.DeptName),
        self.Designation(employee.Designation)
    };


};
//ko.applyBindings(new EmpViewModel());

//$(function() {
//    $.ajax({
//        type: "GET",
//        url: apiStudents,
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            console.log(data); //Put the response in ObservableArray
//        },
//        error: function (error) {
//            alert(error.status + "<--and--> " + error.statusText);
//        }
//    });
//    ViewModel.getStudents();
//});
