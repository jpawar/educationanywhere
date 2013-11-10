myApp.controller('SignInCtrl', function SignInCtrl($scope, $http) {

    var url = "/Registration";
    $scope.userNameVisible = 'hide';

    $.cookie.raw = true;
    
    var customer = $.cookie("customer");
    
    if (customer !== undefined && customer !== null) {   
        hideSignOn(JSON.parse(customer));
    }
    
    $scope.signOnUser = function () {
        
        var userInfo = $.param({
            EmailAddress: this.emailAddress,
            Password: this.password
        });

        url = url + "/SignIn";
        
        $http({
            method: 'POST',
            url: url,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: userInfo
        }).success(loginSuccess).error(loginFailed);

    };

    $scope.signOutUser = function () {
        url = url + "/SignOut";
        $http.delete(url, "").success(logoutSuccess);        
    };

    function logoutSuccess(data, status, headers, config) {    
        $.removeCookie('customer', { path: '/' });
        window.location.href = '/';
    }

    function hideSignOn(userData) {
        
        $scope.userNameVisible = '';
        $scope.visibility = 'hide';
        $scope.userName = 'Hello ' + userData.Name;
        
        userData.Password = '';
        $.cookie('customer', JSON.stringify(userData), { expires: 7, path: '/' });
    }


    function loginSuccess(data, status, headers, config) {
        hideSignOn(data);
        window.location.href = '/Course/Index';
    }

    function loginFailed(data, status, headers, config) {        
        $scope.password = '';
    }
});