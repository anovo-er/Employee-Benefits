$(function () {

    var viewModel = function () {
        var self = this;
        self.employees = ko.observableArray();
        self.applicationDetails = function(employee) {
            console.log(employee.id);

            window.location.replace(`applicationdetails/${employee.id}`);
        };
    };

    var app = new viewModel();
    ko.applyBindings(app);


    fetch('/api/employee')
    .then(response => response.json())
    .then(data => {
        let apps = data;
        apps.forEach(e => app.employees.push(e));
    });
});