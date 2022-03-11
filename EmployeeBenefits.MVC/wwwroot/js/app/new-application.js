$(function () {

    var employeeViewModel = function () {
        var self = this;
        self.EmployeeId = ko.observable();
        self.FirstName = ko.observable();
        self.LastName = ko.observable();
    }

    var dependantViewModel = function (employeeId) {
        var self = this;
        self.EmployeeId = employeeId;
        self.FirstName = ko.observable();
        self.LastName = ko.observable();
    }

    var applicationViewModel = function () {
        var self = this;
        self.EmployeeAdded = ko.observable(false);
        self.AddingDependant = ko.observable(false);

        self.Employee = ko.observable(new employeeViewModel());
        self.Dependant = ko.observable(new dependantViewModel());
        self.Dependants = ko.observableArray();
        self.Benefits = ko.observable();

        self.addEmployee = () => {
            postData('/api/employee', ko.toJSON(self.Employee))
                .then(data => {
                    let employeeId = data;
                    self.EmployeeAdded(true);
                    self.Employee().EmployeeId(employeeId);
                    self.loadBenefits(employeeId);
                });
        };

        self.addDependant = () => {
            let employeeId = self.Employee().EmployeeId();
            self.Dependant().EmployeeId = employeeId;
            postData('/api/dependant', ko.toJSON(self.Dependant))
                .then(data => {
                    self.EmployeeAdded(true);
                    self.Employee().EmployeeId(employeeId);
                    self.Dependant(new dependantViewModel());
                    self.AddingDependant(false);
                    self.loadBenefits(employeeId);
                });
        };

        self.addingDependant = () => {
            self.AddingDependant(true);
        };

        self.loadBenefits = (employeeId) => {
            fetch(`/api/benefits/${employeeId}`)
                .then(response => response.json())
                .then(data => {
                    self.Benefits(data);
                });
        };

        self.removeDependant = (dependant) => {
            let dependantId = dependant.id;
            let employeeId = self.Employee().EmployeeId();
            fetch(`/api/dependant/${dependantId}`, { method: 'Delete' })
                .then(response => { self.loadBenefits(employeeId) })
        };
    };
    ko.applyBindings(applicationViewModel);

    async function postData(url = '', data = {}) {
        const response = await fetch(url, {
            method: 'POST',
            mode: 'cors',
            cache: 'no-cache',
            credentials: 'same-origin',
            headers: {
                'Content-Type': 'application/json'
            },
            redirect: 'follow',
            referrerPolicy: 'no-referrer',
            body: data
        });
        return response.json();
    }
});