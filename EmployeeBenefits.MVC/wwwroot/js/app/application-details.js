

$(function () {

    var employeeId = $('#EmployeeId').val();

    var applicationViewModel = function () {
        var self = this;
        self.Benefits = ko.observable();
    };

    var app = new applicationViewModel();
    ko.applyBindings(app);

    fetch(`/api/benefits/${employeeId}`)
        .then(response => response.json())
        .then(data => {
            app.Benefits(data);
        });
});