myApp.controller('RegistrationCtrl', function RegistrationCtrl($scope, $http) {

    var url = "/Registration/Register";
    $scope.showError = 'hide';

    $scope.registerUser = function() {

        //alert('Works');
        var userInfo = $.param({
            Name: this.user.name,
            Password: this.user.password,
            EmailAddress: this.user.emailAddress,
            Grade: this.user.grade,
            Role: this.user.role
        });

        $http({
            method: 'POST',
            url: url,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: userInfo
        }).success(redirectToCourse).error(showErrorMessage);

    };

    function redirectToCourse() {
        //alert("Success");
        //$location.path('/Course/Index');
        window.location.href = '/Course/Index';
    }

    function showErrorMessage() {
        //alert("Error");
        $scope.showError = '';
        $scope.registrationErrorMessage = "Failed to register";
    }
});