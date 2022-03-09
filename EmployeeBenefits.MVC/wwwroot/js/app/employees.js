$(function () {

    var viewModel = {
        employees: ko.observableArray(),
    };
    ko.applyBindings(viewModel);

    fetch('/api/employee')
        .then(response => response.json())
        .then(data => {
            let employees = data;
            employees.forEach(e => viewModel.employees.push(e));
        });
});