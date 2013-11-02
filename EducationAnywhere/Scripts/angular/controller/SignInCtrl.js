myApp.controller('SignInCtrl', function SignInCtrl($scope, $http) {

    var url = "/Registration/SignIn";
    $scope.showError = 'hide';

    $scope.signOnUser = function () {

        alert('Works');
        var userInfo = $.param({
            EmailAddress: this.emailAddress,
            Password: this.password
        });

        $http({
            method: 'POST',
            url: url,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: userInfo
        }).success(loginSuccess).error(loginFailed);

    };

    function loginSuccess() {
        alert("Success");        
        window.location.href = '/Course/Index';
    }

    function loginFailed(data, status, headers, config) {
        alert("Error");
        alert(status);
        //$scope.showError = '';
        //$scope.registrationErrorMessage = "Failed to logon";
        //$scope.password = '';
    }
});