myApp.controller('SignInCtrl', function SignInCtrl($scope, $http) {

    var url = "/Registration/SignIn";
    $scope.userNameVisible = 'hide';

    $.cookie.raw = true;
    
    var userId = $.cookie('userId123');
    
    if (userId !== undefined && userId !== null) {   
        var userName = $.cookie('userName');
        hideSignOn(userId.toString(), userName);                
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
        $http.delete(url, "").success(logoutSuccess);        
    };

    function logoutSuccess(data, status, headers, config) {
        $.removeCookie('userId123', { path: '/' });
        $.removeCookie('userName', { path: '/' });
        window.location.href = '/';
    }

    function hideSignOn(loggedUserId, loggedUserName) {
        $scope.userNameVisible = '';
        $scope.visibility = 'hide';
        $scope.userName = 'Hello ' + loggedUserName;
        
        $.cookie('userId123', loggedUserId, { expires: 7, path: '/' });
        $.cookie('userName', loggedUserName, { expires: 7, path: '/' });
    }


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