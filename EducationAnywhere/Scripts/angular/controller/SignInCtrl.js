myApp.controller('SignInCtrl', function SignInCtrl($scope, $http, $cookieStore, $cookies) {

    var url = "/Registration/SignIn";
    $scope.userNameVisible = 'hide';
    
    var userId = $cookieStore.get('userId');
    
    if (userId != null) {
        hideSignOn(userId, $cookieStore.get('userName'));
    }
    
    
    $scope.signOnUser = function () {
        
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


    $scope.signOutUser = function () {
        delete $cookies['userId'];
        delete $cookies['userName'];
        window.location.href = '/';
        /*$cookieStore.put('userId', null);
        $cookieStore.put('userName', null);*/
        //restoreSignOn(); 
    };

    function hideSignOn(userId, userName) {
        $scope.userNameVisible = '';
        $scope.visibility = 'hide';
        $scope.userName = 'Hello ' + userName;
        $cookieStore.put('userId', userId);
        $cookieStore.put('userName', userName);
    }

    /*function restoreSignOn() {
        $scope.userNameVisible = 'hide';
        $scope.visibility = '';
        $scope.userName = '';        
    }*/


    function loginSuccess(data, status, headers, config) {
        hideSignOn(data.Id, data.Name);
        window.location.href = '/Course/Index';
    }

    function loginFailed(data, status, headers, config) {        
        //$scope.showError = '';
        //$scope.registrationErrorMessage = "Failed to logon";
        $scope.password = '';
    }
});