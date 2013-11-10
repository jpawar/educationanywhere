myApp.controller('TutorialCtrl', function($scope, $http) {

    var url = "/Tutorial";


    $scope.getCourses = function () {                
        $http.get('/Tutorial/Course').then(getCourseSuccess);
    };
    
    function getCourseSuccess(data) {
        $scope.CourseList = data.data;
    }
    
});