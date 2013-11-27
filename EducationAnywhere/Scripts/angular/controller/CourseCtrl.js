myApp.controller('CourseCtrl', function ($scope, $http) {

    var url = "/Course";    
    var customer = JSON.parse($.cookie("customer"));

    $scope.isRoleTeacher = 'hide';
    
    if (customer.Role === "1") {
        $scope.isRoleTeacher = '';
    }

    $scope.uploadTutorial = function() {
        window.location.href = '/Tutorial/Index';
    };


    $scope.getAllCourses = function () {

        $http.get('/Course/Details').then(fetchCourseDetailSuccess);
        
        function fetchCourseDetailSuccess(data, status, headers, config) {
            $scope.CourseDescriptionList = data.data;
        }
    };
    
    $scope.createCourse = function() {

        var courseInfo = $.param({
            Subject: $scope.course.name,
            Grade: $scope.course.grade
        });

        $http({
            method: 'POST',
            url: url + '/Create',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: courseInfo
        }).success(courseSuccess).error(courseFailed);

        function courseSuccess(data, status, headers, config) {
            window.location.href = '/Course/Index';
        }

        function courseFailed(data, status, headers, config) {

        }

    };
    
});