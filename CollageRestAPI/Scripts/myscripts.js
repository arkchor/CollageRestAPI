"use strict";

var apiStudents = "http://localhost:5501/api/students";
var apiCourses = "http://localhost:5501/api/courses";

function Student(data) {
    this.Id = ko.observable(data.Id);
}

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

var ViewModel = function() {
    var self = this;

    self.students = ko.observableArray([]);
    self.courses = ko.observableArray([]);

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
    $.ajax({
        type: "GET",
        url: apiStudents,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
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
        },
        error: function (error) {
            alert(error.status + "<--and--> " + error.statusText);
        }
    });

    //$(function() {
        
    //});
}
ko.applyBindings(new ViewModel());

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
